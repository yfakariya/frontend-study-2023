// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Infrastructure;
using Octokit;

namespace GitHubViewer.Authentication;

public class GitHubTokenInformation
{
	private readonly BasicAuthenticationApiConnectionFactory _connectionFactory;

	public GitHubTokenInformation(BasicAuthenticationApiConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public async Task<IReadOnlyList<string>> GetScopesAsync(OAuth2Credentials credentials, CancellationToken cancellationToken = default)
	{
		var authorizationsClient =
			new AuthorizationsClient(
				await _connectionFactory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false)
			);

		return
			(await authorizationsClient.CheckApplicationAuthentication(
				credentials.ClientId,
				credentials.AccessToken
			).ConfigureAwait(false)).Scopes;
	}
}
