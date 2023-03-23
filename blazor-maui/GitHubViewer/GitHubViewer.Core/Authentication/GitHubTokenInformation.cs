// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Octokit;

namespace GitHubViewer.Authentication;

public class GitHubTokenInformation
{
	private readonly IAuthorizationsClient _authorizationsClient;

	public GitHubTokenInformation(IApiConnection connection)
	{
		_authorizationsClient = new AuthorizationsClient(connection);
	}

	public async Task<IReadOnlyList<string>> GetScopesAsync(OAuth2Credentials credentials)
		=> (await _authorizationsClient.CheckApplicationAuthentication(credentials.ClientId, credentials.AccessToken)).Scopes;
}
