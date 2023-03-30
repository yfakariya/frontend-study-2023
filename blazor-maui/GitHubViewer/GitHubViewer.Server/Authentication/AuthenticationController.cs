// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GitHubViewer.Authentication;

public class AuthenticationController : Controller
{
	[HttpGet("~/SignIn")]
	[HttpPost("~/SignIn")]
	public IActionResult SignIn()
		=> Challenge(new AuthenticationProperties { RedirectUri = "/" }, GitHubAuthenticationDefaults.AuthenticationScheme);

	[HttpGet("~/SignOut")]
	[HttpPost("~/SignOut")]
	public IActionResult SignOutCurrentUser()
		=> SignOut(new AuthenticationProperties { RedirectUri = "/" },
			// Clear session cookie
			IdentityConstants.ExternalScheme
		);
}
