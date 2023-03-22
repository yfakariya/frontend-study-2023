// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace GitHubViewer;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	private static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
