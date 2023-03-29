// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Authentication
{
	public sealed class AuthenticationOptions
	{
		public const string DefaultSignInRoute = "/SignIn";
		public const string DefaultSignOuteRoute = "/SignOut";

		public string SignInRoute { get; set; } = DefaultSignInRoute;

		public string SignOutRoute { get; set; } = DefaultSignOuteRoute;
	}
}
