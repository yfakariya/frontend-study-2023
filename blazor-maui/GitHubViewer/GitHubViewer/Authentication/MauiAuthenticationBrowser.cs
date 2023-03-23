// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

// Based on IdentityModel sample(https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/77d875ed5945a4f417318a2c84c20a1c5da17999/Maui/MauiApp2/MauiApp2/MauiAuthenticationBrowser.cs)
// Apache 2 license (https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/77d875ed5945a4f417318a2c84c20a1c5da17999/LICENSE)

using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;

namespace GitHubViewer.Authentication;
internal sealed class MauiAuthenticationBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
#pragma warning disable IDE0060
	public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
#pragma warning restore IDE0060
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
				);

			var url =
				new RequestUrl(Uris.CallbackUriString)
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
