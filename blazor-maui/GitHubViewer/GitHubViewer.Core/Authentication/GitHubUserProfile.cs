// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Security.Claims;
using GitHubViewer.Infrastructure;
using Octokit;

namespace GitHubViewer.Authentication;

public class GitHubUserProfile
{
	private readonly OAuth2ApiConnectionFactory _connectionFactory;

	public GitHubUserProfile(OAuth2ApiConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public async Task<ClaimsPrincipal> GetAuthenticatedUserAsync(IReadOnlyCollection<string> scopes, CancellationToken cancellationToken = default)
	{
		var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
		var usersClient = new UsersClient(connection);

		var currentUser = await usersClient.Current().ConfigureAwait(false);
		if (currentUser == null)
		{
			return ClaimsPrincipals.Anonymous;
		}

		var claims =
			new List<Claim>(capacity: 5)
			{
				new Claim(ClaimTypes.NameIdentifier, currentUser.Login?.Trim()!),
			};

		if (!String.IsNullOrEmpty(currentUser.Name))
		{
			claims.Add(new Claim("urn:github:name", currentUser.Name));
		}

		if (!String.IsNullOrEmpty(currentUser.Url))
		{
			claims.Add(new Claim("urn:github:url", currentUser.Url));
		}

		if (!String.IsNullOrEmpty(currentUser.Email))
		{
			claims.Add(new Claim(ClaimTypes.Email, currentUser.Email));
		}

		return
			new ClaimsPrincipal(
				new ClaimsIdentity(
					claims: claims,
					authenticationType: connection.Connection.BaseAddress.ToString(),
					nameType: ClaimTypes.NameIdentifier,
					roleType: ClaimsPrincipals.ScopeType
				)
			);
	}
}
