// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Authentication;

public abstract class CredentialsRepository : IGitHubAccessTokenProvider
{
	private OAuth2Credentials? _inMemory;

	public async ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
		=> (await GetCredentialsAsync(cancellationToken).ConfigureAwait(false))?.AccessToken;

	public async ValueTask<OAuth2Credentials?> GetCredentialsAsync(CancellationToken cancellationToken = default)
	{
		var inMemory = _inMemory;
		if (inMemory != null)
		{
			return inMemory;
		}

		inMemory = await LoadCredentialsAsync(cancellationToken).ConfigureAwait(false);
		_inMemory = inMemory;
		return inMemory;
	}

	protected abstract ValueTask<OAuth2Credentials?> LoadCredentialsAsync(CancellationToken cancellationToken);

	public async ValueTask SetCredentialsAsync(OAuth2Credentials credentials, bool persists, CancellationToken cancellationToken = default)
	{
		if (persists)
		{
			await SaveCredentialsAsync(credentials, cancellationToken).ConfigureAwait(false);
		}

		_inMemory = credentials;
	}

	protected abstract ValueTask SaveCredentialsAsync(OAuth2Credentials credentials, CancellationToken cancellationToken);

	public async ValueTask ClearCredentialsAsync(CancellationToken cancellationToken = default)
	{
		await ClearPersistedCredentialsAsync(cancellationToken).ConfigureAwait(false);
		_inMemory = null;
	}

	protected abstract ValueTask ClearPersistedCredentialsAsync(CancellationToken cancellationToken);
}
