// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using Octokit;
using Octokit.Internal;

namespace GitHubViewer.Infrastructure;

public static class GitHubCredentialProviderExtensions
{
	public static async ValueTask<ICredentialStore> GetOAuth2CredentialStore(this IGitHubAccessTokenProvider source, CancellationToken cancellationToken = default)
	{
		if (source == null)
		{
			throw new ArgumentNullException(nameof(source));
		}

		var accessToken = await source.GetAccessTokenAsync(cancellationToken).ConfigureAwait(false);
		return
			new InMemoryCredentialStore(
				String.IsNullOrEmpty(accessToken)
				? Credentials.Anonymous
				// We must send as Authorization: Bearer
				: new Credentials(accessToken, AuthenticationType.Bearer)
			);
	}
}
