// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Security.Claims;
using GitHubViewer.Areas.Identity.Data;
using GitHubViewer.Authentication;
using Microsoft.AspNetCore.Identity;

namespace GitHubViewer.Infrastructure;

public sealed class AspNetIdentityCredentialsProvider : ICredentialsProvider
{
	private readonly AspNetUserManager<GitHubViewerUser> _userManager;

	public AspNetIdentityCredentialsProvider(AspNetUserManager<GitHubViewerUser> userManager)
	{
		_userManager = userManager;
	}

	public async ValueTask<OAuth2Credentials?> GetCredentialsAsync(CancellationToken cancellationToken = default)
	{
		var principal = ClaimsPrincipal.Current;
		if (principal == null)
		{
			return null;
		}

		var user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
		if (user == null)
		{
			return null;
		}

		var token = await _userManager.GetAuthenticationTokenAsync(user, "github", "access_token").ConfigureAwait(false);

		if (token == null)
		{
			return null;
		}

		return new OAuth2Credentials(String.Empty, String.Empty, token);
	}
}
