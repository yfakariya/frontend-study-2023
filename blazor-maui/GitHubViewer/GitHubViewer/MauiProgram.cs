// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Runtime.InteropServices;
using GitHubViewer.Authentication;
using GitHubViewer.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Client.Platforms.Shared.DefaultOSBrowser;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;
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
		builder.Services.AddSingleton<IBrowserLauncher, MauiBrowserLauncher>();

		builder.Services.AddSingleton<ICredentialStore, MauiSecureStorageCredentialsRepository>();
		builder.Services.AddSingleton<CredentialsRepository, MauiSecureStorageCredentialsRepository>();
		builder.Services.AddSingleton<IGitHubAuthenticator, MauiGitHubAuthenticator>();
		builder.Services.AddSingleton<AuthenticationStateProvider, GitHubAuthenticationStateProvider>();

		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			// Due to WindowsAppSDK issue (https://github.com/microsoft/WindowsAppSDK/issues/441),
			// we cannot use WebAuthentication in Windows, so we use HttpListener based oauth2 authentication, borrowed from MSAL.NET.
			builder.Services.AddSingleton<IdentityModel.OidcClient.Browser.IBrowser, HttpListenerAuthenticationBrowser>();
		}
		else
		{
			builder.Services.AddSingleton<IdentityModel.OidcClient.Browser.IBrowser, MauiAuthenticationBrowser>();
		}

		builder.Services.AddSingleton<IUriInterceptor, HttpListenerInterceptor>();
		builder.Services.AddSingleton<IDefaultOSBrowser, WindowsDefaultOSBrowser>();

		return builder.Build();
	}
}
