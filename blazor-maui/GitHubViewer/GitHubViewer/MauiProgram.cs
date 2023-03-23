// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using GitHubViewer.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Octokit;

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
		ApplicationSetUp.RegisterServices(builder.Services, useScoped: false);
		builder.Services.AddSingleton<ICredentialStore, MauiSecureStorageCredentialsRepository>();
		builder.Services.AddSingleton<CredentialsRepository, MauiSecureStorageCredentialsRepository>();
		builder.Services.AddSingleton<IGitHubAuthenticator, MauiGitHubAuthenticator>();
		builder.Services.AddSingleton<AuthenticationStateProvider, GitHubAuthenticationStateProvider>();
		builder.Services.AddSingleton<IBrowserLauncher, MauiBrowserLauncher>();

		return builder.Build();
	}
}
