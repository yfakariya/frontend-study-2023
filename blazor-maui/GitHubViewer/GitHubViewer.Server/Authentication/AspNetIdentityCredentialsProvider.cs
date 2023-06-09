// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Authentication;

namespace GitHubViewer.Authentication;

public sealed class AspNetIdentityCredentialsProvider : IGitHubAccessTokenProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AspNetIdentityCredentialsProvider(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public async ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
	{
		var context = _httpContextAccessor.HttpContext;
		return context == null ? null : await context.GetTokenAsync("access_token").ConfigureAwait(false);
	}
}
