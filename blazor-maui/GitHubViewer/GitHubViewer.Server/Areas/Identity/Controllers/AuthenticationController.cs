// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GitHubViewer.Identity.Controllers;

public class AuthenticationController : Controller
{
	[HttpGet("~/Identity/SignIn")]
	[HttpPost("~/Identity/SignIn")]
	public IActionResult SignIn()
		=> Challenge(new AuthenticationProperties { RedirectUri = "/" }, GitHubAuthenticationDefaults.AuthenticationScheme);

	[HttpGet("~/Identity/SignOut")]
	[HttpPost("~/Identity/SignOut")]
	public IActionResult SignOutCurrentUser()
		=> SignOut(new AuthenticationProperties { RedirectUri = "/" },
			// Clear session cookie
			IdentityConstants.ExternalScheme
		);
}
