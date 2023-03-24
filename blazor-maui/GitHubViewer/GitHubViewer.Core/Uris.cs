// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer;

public static class Uris
{
	public const string UriScheme = "github.viewer.app";
	public const string MobileCallbackUriString = $"{UriScheme}:/oauth2redirect";
	public static readonly Uri MobileCallbackUri = new(MobileCallbackUriString);
	public const string WindowsCallbackUriStringTemplate = $"http://127.0.0.1:{{0}}/{UriScheme}/oauth2redirect";
	public static readonly Uri GitHubAuthorizationUri = new("https://github.com/login/oauth/authorize");
}
