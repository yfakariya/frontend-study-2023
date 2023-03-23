// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Security.Claims;
using Octokit;

namespace GitHubViewer.Authentication;

public class GitHubUserProfile
{
	private readonly IUsersClient _usersClient;
	private readonly IApiConnection _connection;

	public GitHubUserProfile(IApiConnection connection)
	{
		_connection = connection;
		_usersClient = new UsersClient(connection);
	}

	public async Task<ClaimsPrincipal> GetAuthenticatedUserAsync(IReadOnlyCollection<string> scopes)
	{
		var currentUser = await _usersClient.Current();
		if (currentUser == null)
		{
			return ClaimsPrincipals.Anonymous;
		}

		var claims =
			new List<Claim>(capacity: scopes.Count + 3)
			{
				new Claim(ClaimTypes.NameIdentifier, currentUser.Login),
				new Claim(ClaimTypes.Name, currentUser.Name),
				new Claim(ClaimTypes.Email, currentUser.Email)
			};
		claims.AddRange(scopes.Select(s => new Claim(ClaimsPrincipals.ScopeType, s)));

		return
			new ClaimsPrincipal(
				new ClaimsIdentity(
					claims: claims,
					authenticationType: _connection.Connection.BaseAddress.ToString(),
					nameType: ClaimTypes.NameIdentifier,
					roleType: ClaimsPrincipals.ScopeType
				)
			);
	}
}
