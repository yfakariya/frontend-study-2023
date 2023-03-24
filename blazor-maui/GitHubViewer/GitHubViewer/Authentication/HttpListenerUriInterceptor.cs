#pragma warning disable IDE0073
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Based on MSAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/HttpListenerInterceptor.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using GitHubViewer.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace Microsoft.Identity.Client.Platforms.Shared.DefaultOSBrowser;

internal sealed class HttpListenerUriInterceptor : IUriInterceptor
{
	private readonly HttpListener _listener;
	private readonly string _urlToListenTo;
	private readonly Func<Uri, MessageAndHttpCode> _responseProducer;
	private readonly ILogger _logger;
	private readonly TaskCompletionSource _ready;
	private readonly TaskCompletionSource<Uri> _intercepted;

	public Task Ready => _ready.Task;

	public Task<Uri> Intercepted => _intercepted.Task;

#if DEBUG
	public Action? TestBeforeGetContext { get; set; }
#endif


	[Conditional("DEBUG")]
	private void OnBeforeGetContext()
	{
#if DEBUG
		TestBeforeGetContext?.Invoke();
#endif
	}

	public HttpListenerUriInterceptor(
		int port,
		string path,
		Func<Uri, MessageAndHttpCode> responseProducer,
		ILogger<HttpListenerUriInterceptor> logger
	)
	{
		path =
			String.IsNullOrEmpty(path)
			? "/"
			: path.StartsWith("/", StringComparison.Ordinal)
			? path
			: "/" + path;

		var urlToListenTo = "http://127.0.0.1:" + port + path;

		if (!urlToListenTo.EndsWith("/", StringComparison.Ordinal))
		{
			urlToListenTo += "/";
		}

		_urlToListenTo = urlToListenTo;
		_responseProducer = responseProducer;
		_ready = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
		_intercepted = new TaskCompletionSource<Uri>(TaskCreationOptions.RunContinuationsAsynchronously);
		_listener = new HttpListener();
		_listener.Prefixes.Add(urlToListenTo);

		_logger = logger;
	}

	public void Dispose()
	{
		try
		{
			_listener.Abort();
		}
		catch { }
	}

	public async Task<Uri> ListenToSingleRequestAndRespondAsync(CancellationToken cancellationToken)
	{
		try
		{
			_listener.Start();
			OnBeforeGetContext();

			var contextTask = _listener.GetContextAsync().ConfigureAwait(false);

			_ready.SetResult();

			cancellationToken.ThrowIfCancellationRequested();

			var context = await contextTask;
			await RespondAsync(context, cancellationToken).ConfigureAwait(false);
			_logger.ListenerReceivedMessage(_urlToListenTo);

			// the request URL should now contain the auth code and pkce
			return context.Request.Url!;
		}
		// If cancellation is requested before GetContextAsync is called, then either 
		// an ObjectDisposedException or an HttpListenerException is thrown.
		// But this is just cancellation...
		catch (Exception ex) when (ex is HttpListenerException || ex is ObjectDisposedException)
		{
			_logger.ListenerThrewException(ex, cancellationToken.IsCancellationRequested);
			cancellationToken.ThrowIfCancellationRequested();

			if (ex is HttpListenerException)
			{
				throw new AuthenticationException(
					$"An HttpListenerException occurred while listening on {_urlToListenTo} for the system browser to complete the login. " +
					"Possible cause and mitigation: the app is unable to listen on the specified URL; " +
					"run 'netsh http add iplisten 127.0.0.1' from the Admin command prompt.",
					ex
				);
			}

			// if cancellation was not requested, propagate original ex
			throw;
		}
	}

	private static void TryStopListening(HttpListener httpListener)
	{
		try
		{
			httpListener.Abort();
		}
		catch
		{
		}
	}

	private async ValueTask RespondAsync(
		HttpListenerContext context,
		CancellationToken cancellationToken
	)
	{
		var messageAndCode = _responseProducer(context.Request.Url!);
		_logger.ProcessingResponseToBrowser(messageAndCode.HttpCode);

		try
		{
			switch (messageAndCode.HttpCode)
			{
				case HttpStatusCode.Found:
				{
					context.Response.StatusCode = (int)HttpStatusCode.Found;
					context.Response.RedirectLocation = messageAndCode.Message;
					break;
				}
				case HttpStatusCode.OK:
				{
					var buffer = Encoding.UTF8.GetBytes(messageAndCode.Message);
					context.Response.ContentLength64 = buffer.Length;
					await context.Response.OutputStream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
					break;
				}
				default:
				{
					throw new NotImplementedException("HttpCode not supported: " + messageAndCode.HttpCode);
				}
			}

		}
		finally
		{
			await context.Response.OutputStream.FlushAsync(cancellationToken).ConfigureAwait(false);
			await context.Response.OutputStream.DisposeAsync().ConfigureAwait(false);
		}
	}
}
