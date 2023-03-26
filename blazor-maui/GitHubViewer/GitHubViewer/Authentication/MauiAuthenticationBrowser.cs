// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

// Based on IdentityModel sample(https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/77d875ed5945a4f417318a2c84c20a1c5da17999/Maui/MauiApp2/MauiApp2/MauiAuthenticationBrowser.cs)
// Apache 2 license (https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/77d875ed5945a4f417318a2c84c20a1c5da17999/LICENSE)

using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;

namespace GitHubViewer.Authentication;

internal sealed class MauiAuthenticationBrowser : IdentityModel.OidcClient.Browser.IBrowser, IOAuthHelper
{
	private readonly Uri _redirectUri;

	IdentityModel.OidcClient.Browser.IBrowser IOAuthHelper.Browser => this;

	public MauiAuthenticationBrowser(Uri redirectUri)
	{
		_redirectUri = redirectUri;
	}

	void IDisposable.Dispose() { }

	public ValueTask<AuthorizationCodeFlowInformation> PrepareAsync(CancellationToken cancellationToken = default)
		=> ValueTask.FromResult(new AuthorizationCodeFlowInformation(_redirectUri.OriginalString));

	public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
	{
		try
		{
			var result =
				await WebAuthenticator.Default.AuthenticateAsync(
					new WebAuthenticatorOptions
					{
						Url = new Uri(options.StartUrl),
						CallbackUrl = new Uri(options.EndUrl),
						PrefersEphemeralWebBrowserSession = true,
					}
				).ConfigureAwait(false);

			var url =
				new RequestUrl(options.EndUrl)
				.Create(new Parameters(result.Properties));

			return
				new BrowserResult
				{
					Response = url,
					ResultType = BrowserResultType.Success
				};
		}
		catch (TaskCanceledException)
		{
			return
				new BrowserResult
				{
					ResultType = BrowserResultType.UserCancel
				};
		}
	}
}
