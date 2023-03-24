// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

// Based on MSAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/HttpListenerInterceptor.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

using System.Net;
using System.Security.Authentication;
using System.Text;
using GitHubViewer.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace Microsoft.Identity.Client.Platforms.Shared.DefaultOSBrowser;

internal sealed class HttpListenerUriInterceptor : IUriInterceptor
{
	private readonly HttpListener _listener;
	private readonly Func<Uri, MessageAndHttpCode> _responseProducer;
	private readonly ILogger _logger;
	private readonly TaskCompletionSource _ready;

	public string ListeningUrl { get; }

	public Task Ready => _ready.Task;

	public HttpListenerUriInterceptor(
		int minimumPortInclusive,
		int maximumPortInclusive,
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


		if (!path.EndsWith("/", StringComparison.Ordinal))
		{
			path += "/";
		}

		_responseProducer = responseProducer;
		_ready = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
		_listener = new HttpListener();
		ListeningUrl = SetupHttpListener(_listener, path, minimumPortInclusive, maximumPortInclusive);
		_logger = logger;
		logger.StartListening(ListeningUrl);
	}

	public void Dispose()
	{
		try
		{
			_listener.Abort();
		}
		catch { }
	}

	// Internal for testing
	internal static string SetupHttpListener(HttpListener listener, string path, int minimumPortInclusive, int maximumPortInclusive)
	{
		foreach (var port in Enumerable.Range(minimumPortInclusive, maximumPortInclusive - minimumPortInclusive + 1))
		{
			var prefix = $"http://127.0.0.1:{port}{path}";
			listener.Prefixes.Clear();
			listener.Prefixes.Add(prefix);

			try
			{
				listener.Start();
				return prefix;
			}
			catch { }
		}

		throw new HttpListenerException(
			unchecked((int)0xC00D158B), // NS_E_PORT_IN_USE_HTTP
			$"Failed to assign port to HttpListener between {minimumPortInclusive} and {maximumPortInclusive}, inclusive."
		);
	}

	public async Task<Uri> ListenToSingleRequestAndRespondAsync(CancellationToken cancellationToken)
	{
		try
		{
			var contextTask = _listener.GetContextAsync().ConfigureAwait(false);

			_ready.SetResult();

			cancellationToken.ThrowIfCancellationRequested();

			var context = await contextTask;
			await RespondAsync(context, cancellationToken).ConfigureAwait(false);
			_logger.ListenerReceivedMessage(ListeningUrl);

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
					$"An HttpListenerException occurred while listening on {ListeningUrl} for the system browser to complete the login. " +
					"Possible cause and mitigation: the app is unable to listen on the specified URL; " +
					"run 'netsh http add iplisten 127.0.0.1' from the Admin command prompt.",
					ex
				);
			}

			// if cancellation was not requested, propagate original ex
			throw;
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
