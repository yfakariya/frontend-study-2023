// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

#if WINDOWS

using System.Diagnostics;

namespace GitHubViewer.Authentication;

// Based on MSAL>NET code (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/netdesktop/NetDesktopPlatformProxy.cs#L216)
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)
internal sealed class WindowsDefaultOSBrowser : IDefaultOSBrowser
{
	public ValueTask OpenAsync(string url)
	{
		try
		{
			var psi =
				new ProcessStartInfo
				{
					FileName = url,
					UseShellExecute = true
				};
			// Open and close remote process handle.
			Process.Start(psi)?.Dispose();
		}
		catch
		{
			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			url = url.Replace("&", "^&");
			// Open and close remote process handle.
			Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true })?.Dispose();
		}

		return ValueTask.CompletedTask;
	}
}
#endif // WINDOWS
