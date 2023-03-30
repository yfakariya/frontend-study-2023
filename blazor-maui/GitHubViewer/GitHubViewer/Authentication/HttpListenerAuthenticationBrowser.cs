// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Globalization;
using System.Net;
using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace GitHubViewer.Authentication;

// Based on MSAL.NET:
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/DefaultOsBrowserWebUi.cs
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/ApiConfig/SystemWebViewOptions.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

internal sealed class HttpListenerAuthenticationBrowser : IdentityModel.OidcClient.Browser.IBrowser, IOAuthHelper
{
	private readonly IDefaultOSBrowser _defaultOSBrowser;
	private readonly IUriInterceptor _interceptor;
	private readonly ILogger _logger;

	IdentityModel.OidcClient.Browser.IBrowser IOAuthHelper.Browser => this;

	public HttpListenerAuthenticationBrowser(
		string redirectUriPath,
		IDefaultOSBrowser defaultOSBrowser,
		IUriInterceptorFactory interceptorFactory,
		IOptionsMonitor<GitHubOptions> options,
		ILogger<HttpListenerAuthenticationBrowser> logger
	)
	{
		_defaultOSBrowser = defaultOSBrowser;
		_interceptor =
			interceptorFactory.CreateInterceptor(
				options.CurrentValue.OAuthRedirectMinimumPort,
				options.CurrentValue.OAuthRedirectMaximumPort,
				redirectUriPath,
				GetResponseMessage
			);
		_logger = logger;
	}

	public void Dispose()
		=> _interceptor.Dispose();

	public async ValueTask<AuthorizationCodeFlowInformation> PrepareAsync(CancellationToken cancellationToken = default)
		=> new AuthorizationCodeFlowInformation(await _interceptor.PrepareAsync(cancellationToken).ConfigureAwait(false));

	public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();

		try
		{
			var interception = _interceptor.ListenToSingleRequestAndRespondAsync(cancellationToken).ConfigureAwait(false);

			cancellationToken.ThrowIfCancellationRequested();

			await _defaultOSBrowser.OpenAsync(options.StartUrl).ConfigureAwait(false);

			var authCodeUri = await interception;

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
		var (error, errorDescription) = GetErrorFromUri(authCodeUri);

		if (!String.IsNullOrEmpty(error))
		{
			_logger.ErrorIsIntercepted(error, errorDescription);

			var errorMessage =
				String.Format(
					CultureInfo.CurrentCulture,
					AuthenticationResource.FailureHtml,
					error,
					errorDescription
				);

			return new MessageAndHttpCode(HttpStatusCode.OK, errorMessage);
		}

		return new MessageAndHttpCode(HttpStatusCode.OK, AuthenticationResource.SuccessHtml);
	}

	private static (string Error, string ErrorDescription) GetErrorFromUri(Uri resultUri)
	{
		if (String.IsNullOrWhiteSpace(resultUri.OriginalString))
		{
			return (Errors.AuthenticationFailed, ErrorMessages.AuthorizationServerInvalidResponse);
		}

		// NOTE: The Fragment property actually contains the leading '#' character and that must be dropped
		var resultData = resultUri.Query;

		if (String.IsNullOrWhiteSpace(resultData))
		{
			return (Errors.AuthenticationFailed, ErrorMessages.AuthorizationServerInvalidResponse);
		}

		return (String.Empty, String.Empty);
	}

	private static class Errors
	{
		/// <summary>
		/// Authentication failed.
		/// <para>What happens?</para>
		/// The authentication failed. For instance the user did not enter the right password
		/// <para>Mitigation</para>
		/// Inform the user to retry.
		/// </summary>
		public const string AuthenticationFailed = "authentication_failed";
	}

	private static class ErrorMessages
	{
		public const string AuthorizationServerInvalidResponse = "The authorization server returned an invalid response. ";
	}
}
