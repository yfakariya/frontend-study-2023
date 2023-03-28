// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Text.Json;

namespace GitHubViewer.Infrastructure;

public sealed class AspNetSessionStorage<T> : ISessionStorage<T>
{
#pragma warning disable CA1000 // Do not declare static members on generic types
	public static string KeyPrefix { get; } = $"{typeof(T).FullName}.";
#pragma warning restore CA1000 // Do not declare static members on generic types

	private readonly ISession _session;

	public AspNetSessionStorage(ISession session)
	{
		_session = session;
	}

	private static string GetKey(string key) => $"{KeyPrefix}{key}";

	public ValueTask<StorageResult<TValue>> GetAsync<TValue>(string key, CancellationToken cancellationToken = default)
	{
		if (!_session.TryGetValue(GetKey(key), out var bytes))
		{
			return ValueTask.FromResult(new StorageResult<TValue>(Success: false, default!));
		}

		return ValueTask.FromResult(new StorageResult<TValue>(Success: true, JsonSerializer.Deserialize<TValue>(bytes)!));
	}

	public ValueTask SetAsync<TValue>(string key, TValue value, CancellationToken cancellationToken = default)
	{
		_session.Set(GetKey(key), JsonSerializer.SerializeToUtf8Bytes(value));
		return ValueTask.CompletedTask;
	}

	public ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
	{
		_session.Remove(GetKey(key));
		return ValueTask.CompletedTask;
	}
}
