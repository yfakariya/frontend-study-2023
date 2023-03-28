// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using Octokit;
using Octokit.Internal;

namespace GitHubViewer.Infrastructure;

public static class GitHubCredentialProviderExtensions
{
	public static async ValueTask<ICredentialStore> GetBasicAuthenticationCredentialStore(this ICredentialsProvider source, CancellationToken cancellationToken = default)
	{
		if (source == null)
		{
			throw new ArgumentNullException(nameof(source));
		}

		var underlying = await source.GetCredentialsAsync(cancellationToken).ConfigureAwait(false);
		return
			new InMemoryCredentialStore(
				underlying == null
				? Credentials.Anonymous
				: new Credentials(underlying.ClientId, underlying.ClientSecret, AuthenticationType.Basic)
			);
	}
	public static async ValueTask<ICredentialStore> GetOAuth2CredentialStore(this ICredentialsProvider source, CancellationToken cancellationToken = default)
	{
		if (source == null)
		{
			throw new ArgumentNullException(nameof(source));
		}

		var underlying = await source.GetCredentialsAsync(cancellationToken).ConfigureAwait(false);
		return
			new InMemoryCredentialStore(
				underlying == null
				? Credentials.Anonymous
				// We must send as Authorization: Bearer
				: new Credentials(underlying.AccessToken, AuthenticationType.Bearer)
			);
	}
}
