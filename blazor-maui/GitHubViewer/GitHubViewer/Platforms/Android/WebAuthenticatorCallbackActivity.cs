// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Android.App;
using Android.Content;
using Android.Content.PM;

namespace GitHubViewer;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(
	new[] { Intent.ActionView },
	Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
	DataScheme = Uris.UriScheme
)]
public class WebAuthenticationCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
{
}
