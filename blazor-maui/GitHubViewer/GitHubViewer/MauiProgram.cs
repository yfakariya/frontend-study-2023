// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Components.WebView.Maui;
using MudBlazor.Services;
using GitHubViewer.Data;

namespace GitHubViewer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

		builder.Services.AddOptions();
		builder.Services.AddAuthorizationCore();

		builder.Services.AddSingleton<WeatherForecastService>();

		builder.Services.AddMudServices();

		return builder.Build();
	}
}
