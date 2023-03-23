#pragma warning disable IDE0073
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Based on MSAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/HttpListenerInterceptor.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

using System.Diagnostics;
using System.Net;
using System.Security.Authentication;
using System.Text;
using GitHubViewer.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace Microsoft.Identity.Client.Platforms.Shared.DefaultOSBrowser;

internal class HttpListenerInterceptor : IUriInterceptor
{
	private readonly ILogger _logger;

#if DEBUG
	public Action? TestBeforeTopLevelCall { get; set; }
	public Action<string>? TestBeforeStart { get; set; }
	public Action? TestBeforeGetContext { get; set; }
#endif

	[Conditional("DEBUG")]
	private void OnBeforeTopLevelCall()
	{
#if DEBUG
		TestBeforeTopLevelCall?.Invoke();
#endif
	}

	[Conditional("DEBUG")]
	private void OnBeforeStartCall(string urlToListenTo)
	{
#if DEBUG
		TestBeforeStart?.Invoke(urlToListenTo);
#endif
	}

	[Conditional("DEBUG")]
	private void OnBeforeGetContext()
	{
#if DEBUG
		TestBeforeGetContext?.Invoke();
#endif
	}

	public HttpListenerInterceptor(ILogger<HttpListenerInterceptor> logger)
	{
		_logger = logger;
	}

	public async Task<Uri> ListenToSingleRequestAndRespondAsync(
		int port,
		string path,
		Func<Uri, MessageAndHttpCode> responseProducer,
		CancellationToken cancellationToken
	)
	{
		OnBeforeTopLevelCall();
		cancellationToken.ThrowIfCancellationRequested();

		if (String.IsNullOrEmpty(path))
		{
			path = "/";
		}
		else
		{
			path = path.StartsWith("/", StringComparison.Ordinal) ? path : "/" + path;
		}

		var urlToListenTo = "http://127.0.0.1:" + port + path;

		if (!urlToListenTo.EndsWith("/", StringComparison.Ordinal))
		{
			urlToListenTo += "/";
		}

		var httpListener = new HttpListener();
		try
		{
			httpListener.Prefixes.Add(urlToListenTo);

			OnBeforeStartCall(urlToListenTo);
			TestBeforeStart?.Invoke(urlToListenTo);

			httpListener.Start();
			_logger.StartListening(urlToListenTo);

			using (cancellationToken.Register(() =>
				{
					_logger.ListeningIsCancelded();
					TryStopListening(httpListener);
				})
			)
			{
				OnBeforeGetContext();

				var context = await httpListener.GetContextAsync().ConfigureAwait(false);

				cancellationToken.ThrowIfCancellationRequested();

				await RespondAsync(responseProducer, context, cancellationToken);
				_logger.ListenerReceivedMessage(urlToListenTo);

				// the request URL should now contain the auth code and pkce
				return context.Request.Url!;
			}
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
					$"An HttpListenerException occurred while listening on {urlToListenTo} for the system browser to complete the login. " +
					"Possible cause and mitigation: the app is unable to listen on the specified URL; " +
					"run 'netsh http add iplisten 127.0.0.1' from the Admin command prompt.",
					ex
				);
			}

			// if cancellation was not requested, propagate original ex
			throw;
		}
		finally
		{
			TryStopListening(httpListener);
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
		Func<Uri, MessageAndHttpCode> responseProducer,
		HttpListenerContext context,
		CancellationToken cancellationToken
	)
	{
		MessageAndHttpCode messageAndCode = responseProducer(context.Request.Url!);
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
					byte[] buffer = Encoding.UTF8.GetBytes(messageAndCode.Message);
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
