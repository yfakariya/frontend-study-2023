// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Text.Json;

namespace GitHubViewer.Authentication;

public class MauiSecureStorageCredentialsRepository : CredentialsRepository
{
	protected override async ValueTask<OAuth2Credentials?> LoadCredentialsAsync(CancellationToken cancellationToken)
	{
		var json = await SecureStorage.Default.GetAsync(SecureStorageKeys.Credential).ConfigureAwait(false);
		if (String.IsNullOrEmpty(json))
		{
			return null;
		}

		var dic = JsonDocument.Parse(json);

		var clientId = dic.RootElement.GetProperty("clientId"u8).GetString();
		var clientSecret = dic.RootElement.GetProperty("clientSecret"u8).GetString();
		var accessToken = dic.RootElement.GetProperty("accessToken"u8).GetString();

		if (String.IsNullOrEmpty(clientId)
			|| String.IsNullOrEmpty(clientSecret)
			|| String.IsNullOrEmpty(accessToken))
		{
			return null;
		}
		else
		{
			return new OAuth2Credentials(clientId, clientSecret, accessToken);
		}
	}

	protected override async ValueTask SaveCredentialsAsync(OAuth2Credentials credentials, CancellationToken cancellationToken)
		=> await SecureStorage.Default.SetAsync(
			SecureStorageKeys.Credential,
			JsonSerializer.Serialize(
				new Dictionary<string, string>(capacity: 2)
				{
					["clientId"] = credentials.ClientId,
					["clientSecret"] = credentials.ClientSecret,
					["accessToken"] = credentials.AccessToken,
				}
			)
		).ConfigureAwait(false);

	protected override ValueTask ClearPersistedCredentialsAsync(CancellationToken cancellationToken)
	{
		SecureStorage.Default.Remove(SecureStorageKeys.Credential);
		return ValueTask.CompletedTask;
	}
}
