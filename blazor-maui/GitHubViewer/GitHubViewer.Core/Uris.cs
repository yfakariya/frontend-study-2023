// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer;

// TODO: Configurable
public static class Uris
{
	public const string UriScheme = "github.viewer.app";

	private const string Path = "/oauth2redirect";

	public static readonly Uri MobileCallbackUri = new($"{UriScheme}:{Path}");

	public static readonly Uri WindowsCallbackUri = new($"http://127.0.0.1/{UriScheme}{Path}");

	public const string ServerCallbackPath = $"/{UriScheme}{Path}";
}
