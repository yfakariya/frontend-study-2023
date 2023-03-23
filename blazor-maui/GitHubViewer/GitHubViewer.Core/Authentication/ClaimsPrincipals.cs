// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Security.Claims;

namespace GitHubViewer.Authentication;

public static class ClaimsPrincipals
{
	public static string ScopeType => "scope";

	public static ClaimsPrincipal Anonymous { get; } =
		new ClaimsPrincipal(
			new ClaimsIdentity(
				authenticationType: null,
				nameType: ClaimTypes.NameIdentifier,
				roleType: ScopeType
			)
		);
}
