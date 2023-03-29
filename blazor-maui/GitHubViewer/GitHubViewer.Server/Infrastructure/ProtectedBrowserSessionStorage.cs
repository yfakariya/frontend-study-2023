// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GitHubViewer.Infrastructure;

public sealed class ProtectedBrowserSessionStorage<T> : ISessionStorage<T>
{
#pragma warning disable CA1000 // Do not declare static members on generic types
	public static string KeyPrefix { get; } = $"{typeof(T).FullName}.";
#pragma warning restore CA1000 // Do not declare static members on generic types

	private readonly ProtectedSessionStorage _sessionStorage;

	public ProtectedBrowserSessionStorage(ProtectedSessionStorage sessionStorage)
	{
		_sessionStorage = sessionStorage;
	}

	private static string GetKey(string key) => $"{KeyPrefix}{key}";

	public async ValueTask<StorageResult<TValue>> GetAsync<TValue>(string key, CancellationToken cancellationToken = default)
	{
		var result = await _sessionStorage.GetAsync<TValue>(GetKey(key)).ConfigureAwait(false);
		return new StorageResult<TValue>(Success: result.Success, result.Value!);
	}

	public async ValueTask SetAsync<TValue>(string key, TValue value, CancellationToken cancellationToken = default)
		=> await _sessionStorage.SetAsync(GetKey(key), value!).ConfigureAwait(false);

	public async ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
		=> await _sessionStorage.DeleteAsync(GetKey(key)).ConfigureAwait(false);
}
