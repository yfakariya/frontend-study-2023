// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Infrastructure;

public interface ISessionStorage<T>
{
	ValueTask<StorageResult<TValue>> GetAsync<TValue>(string key, CancellationToken cancellationToken = default);

	ValueTask SetAsync<TValue>(string key, TValue value, CancellationToken cancellationToken = default);

	ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default);
}
