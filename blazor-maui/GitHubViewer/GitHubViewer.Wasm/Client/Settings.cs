// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer;

internal partial class Settings
{
	public static (string? ClientId, string? ClientSecret) GetOAuthConfiguration()
	{
		string? clientId = null;
		string? clientSecret = null;
		GetOAuthConfiguration(ref clientId, ref clientSecret);
		return (clientId, clientSecret);
	}

	static partial void GetOAuthConfiguration(ref string? clientId, ref string? clientSecret);
}
