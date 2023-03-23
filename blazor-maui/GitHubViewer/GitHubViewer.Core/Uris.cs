// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer;

public static class Uris
{
	public const string UriScheme = "github.viewer.app";
	public const string CallbackUriString = $"{UriScheme}:/oauth2redirect";
	public static readonly Uri CallbackUri = new Uri(CallbackUriString);
	public static readonly Uri GitHubAuthorizationUri = new Uri("https://github.com/login/oauth/authorize");
}
