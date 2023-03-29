// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AspNet.Security.OAuth.GitHub;

namespace GitHubViewer.Areas.Identity.Controllers;

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
			GitHubAuthenticationDefaults.AuthenticationScheme,
			// Instruct the cookies middleware to delete the local cookie created
			// when the user agent is redirected from the external identity provider
			// after a successful authentication flow.
			CookieAuthenticationDefaults.AuthenticationScheme
		);
}
