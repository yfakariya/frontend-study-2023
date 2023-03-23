// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Globalization;
using System.Net;
using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace GitHubViewer.Authentication;

// Based on MSAL.NET:
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/DefaultOsBrowserWebUi.cs
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/ApiConfig/SystemWebViewOptions.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

internal sealed class HttpListenerAuthenticationBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
	private const string DefaultSuccessHtml = @"""
<html>
  <head><title>Authentication Complete</title></head>
  <body>
    Authentication complete. You can return to the application. Feel free to close this browser tab.
  </body>
</html>""";

	private const string DefaultFailureHtml = @"""
<html>
  <head><title>Authentication Failed</title></head>
  <body>
    Authentication failed. You can return to the application. Feel free to close this browser tab.
</br></br></br></br>
    Error details: error {0} error_description: {1}
  </body>
</html>""";

	private readonly IDefaultOSBrowser _defaultOSBrowser;
	private readonly IUriInterceptor _uriInterceptor;
	private readonly ILogger _logger;

	public HttpListenerAuthenticationBrowser(
		IDefaultOSBrowser defaultOSBrowser,
		IUriInterceptor uriInterceptor,
		ILogger<HttpListenerAuthenticationBrowser> logger
	)
	{
		_defaultOSBrowser = defaultOSBrowser;
		_uriInterceptor = uriInterceptor;
		_logger = logger;
	}

	public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
	{
		var redirectUri = new Uri(options.EndUrl);
		cancellationToken.ThrowIfCancellationRequested();
		await _defaultOSBrowser.OpenAsync(options.StartUrl).ConfigureAwait(false);

		cancellationToken.ThrowIfCancellationRequested();
		try
		{
			var authCodeUri =
				await _uriInterceptor.ListenToSingleRequestAndRespondAsync(
					redirectUri.Port,
					redirectUri.AbsolutePath,
					GetResponseMessage,
					cancellationToken
				).ConfigureAwait(false);

			return
				new BrowserResult
				{
					Response = authCodeUri!.OriginalString,
					ResultType = BrowserResultType.Success,
				};
		}
		catch (OperationCanceledException)
		{
			return
			new BrowserResult
			{
				ResultType = BrowserResultType.UserCancel
			};
		}
	}

	internal /* internal for testing only */ MessageAndHttpCode GetResponseMessage(Uri authCodeUri)
	{
		// Parse the uri to understand if an error was returned. This is done just to show the user a nice error message in the browser.
		var authorizationResult = Microsoft.Identity.Client.UI.AuthorizationResult.FromUri(authCodeUri.OriginalString, _logger);

		if (!String.IsNullOrEmpty(authorizationResult.Error))
		{
			_logger.ErrorIsIntercepted(authorizationResult.Error, authorizationResult.ErrorDescription);

			var errorMessage =
				String.Format(
					CultureInfo.InvariantCulture,
					DefaultFailureHtml,
					authorizationResult.Error,
					authorizationResult.ErrorDescription
				);

			return new MessageAndHttpCode(HttpStatusCode.OK, errorMessage);
		}

		return new MessageAndHttpCode(HttpStatusCode.OK, DefaultSuccessHtml);
	}
}
