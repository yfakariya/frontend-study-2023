// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Infrastructure;

public sealed class NullBrowserLauncher : IBrowserLauncher
{
	public bool IsExternal => false;

	public ValueTask OpenAsync(string uri, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
}
