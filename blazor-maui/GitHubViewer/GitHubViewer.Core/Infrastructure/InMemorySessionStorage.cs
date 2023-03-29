// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Infrastructure
{
	public sealed class InMemorySessionStorage<T> : ISessionStorage<T>
	{
		private readonly Dictionary<string, object?> _storage = new();

		public ValueTask<StorageResult<TValue>> GetAsync<TValue>(string key, CancellationToken cancellationToken = default)
		{
			if (!_storage.TryGetValue(key, out var rawValue) || rawValue is not TValue value)
			{
				return ValueTask.FromResult(new StorageResult<TValue>(Success: false, default!));
			}

			return ValueTask.FromResult(new StorageResult<TValue>(Success: true, value));
		}

		public ValueTask SetAsync<TValue>(string key, TValue value, CancellationToken cancellationToken = default)
		{
			_storage[key] = value;
			return ValueTask.CompletedTask;
		}

		public ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
		{
			_storage.Remove(key);
			return ValueTask.CompletedTask;
		}
	}
}
