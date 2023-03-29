// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace GitHubViewer.Authentication;

internal sealed class WebAssemblyTokenCredentialsProvider : ICredentialsProvider
{
	private readonly IAccessTokenProvider _accessTokenProvider;

	public WebAssemblyTokenCredentialsProvider(IAccessTokenProvider accessTokenProvider)
	{
		_accessTokenProvider = accessTokenProvider;
	}

	public async ValueTask<OAuth2Credentials?> GetCredentialsAsync(CancellationToken cancellationToken = default)
	{
		var result = await _accessTokenProvider.RequestAccessToken().ConfigureAwait(false);
		return result.TryGetToken(out var token) ? new OAuth2Credentials(String.Empty, String.Empty, token.Value) : null;
	}
}
