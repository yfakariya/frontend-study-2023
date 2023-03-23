// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Foundation;

namespace GitHubViewer;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	// NOTE: According to samples (https://github.com/dotnet/maui/blob/1cda76926b1316cf91554075ef6393e15c8b7f03/src/Essentials/samples/Samples/Platforms/MacCatalyst/AppDelegate.cs)
	//       overriding methods are not necessary even if the doc says required (https://learn.microsoft.com/xamarin/essentials/web-authenticator?context=xamarin%2Fxamarin-forms&tabs=ios).

}
