// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Infrastructure
{
	internal sealed class MauiBrowserLauncher : IBrowserLauncher
	{
		public bool IsExternal => true;

		public async ValueTask OpenAsync(string uri, CancellationToken cancellationToken = default)
			=> await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred).ConfigureAwait(false);
	}
}
