// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Android.App;
using Android.Content.PM;
using Android.OS;

namespace GitHubViewer;

[Activity(
	Theme = "@style/Maui.SplashTheme",
	MainLauncher = true,
	ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density
)]
public class MainActivity : MauiAppCompatActivity
{
	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		Xamarin.Essentials.Platform.Init(this, savedInstanceState);
	}

	public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
	{
		Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

		base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
	}
}
