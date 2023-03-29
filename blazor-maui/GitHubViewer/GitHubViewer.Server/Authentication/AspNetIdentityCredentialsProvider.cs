// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Authentication;

namespace GitHubViewer.Authentication;

public sealed class AspNetIdentityCredentialsProvider : ICredentialsProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AspNetIdentityCredentialsProvider(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public async ValueTask<OAuth2Credentials?> GetCredentialsAsync(CancellationToken cancellationToken = default)
	{
		var context = _httpContextAccessor.HttpContext;
		if (context != null)
		{
			var accessToken = await context.GetTokenAsync("access_token").ConfigureAwait(false);

			if (!String.IsNullOrEmpty(accessToken))
			{
				return new OAuth2Credentials(String.Empty, String.Empty, accessToken);
			}
		}

		return null;
	}
}
