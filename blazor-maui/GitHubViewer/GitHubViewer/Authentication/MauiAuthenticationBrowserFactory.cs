// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

// Based on IdentityModel sample(https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/77d875ed5945a4f417318a2c84c20a1c5da17999/Maui/MauiApp2/MauiApp2/MauiAuthenticationBrowser.cs)
// Apache 2 license (https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/77d875ed5945a4f417318a2c84c20a1c5da17999/LICENSE)

namespace GitHubViewer.Authentication;

internal sealed class MauiAuthenticationBrowserFactory : IOAuthHelperFactory
{
	public IOAuthHelper CreateHelper(Uri redirectUri) => new MauiAuthenticationBrowser(redirectUri);
}
