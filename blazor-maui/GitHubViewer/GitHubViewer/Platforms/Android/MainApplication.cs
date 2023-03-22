// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Android.App;
using Android.Runtime;

namespace GitHubViewer;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership) { }

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
