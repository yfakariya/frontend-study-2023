// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using GitHubViewer.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;

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
		ApplicationSetUp.RegisterServices(
			builder.Services,
			builder.Configuration,
			new[] { typeof(MauiProgram).Assembly },
			Registration<IBrowserLauncher>.Singleton<MauiBrowserLauncher>(),
			Registration<IWindowTitleAccessor>.Singleton<MauiWindowTitleAccessor>(),
			Registration<ICredentialsProvider>.Singleton<MauiSecureStorageCredentialsRepository>(),
			Registration<IGitHubAuthenticator>.Singleton<MauiGitHubAuthenticator>(),
			Registration<AuthenticationStateProvider>.Singleton<GitHubAuthenticationStateProvider>(),
			GenericRegistration<ISessionStorage<ValueTuple>>.Singleton<InMemorySessionStorage<ValueTuple>>()
		);

		builder.Services.AddSingleton<CredentialsRepository>(
			p => (MauiSecureStorageCredentialsRepository)p.GetRequiredService<ICredentialsProvider>()
		);

#if WINDOWS
		// Due to WindowsAppSDK issue (https://github.com/microsoft/WindowsAppSDK/issues/441),
		// we cannot use WebAuthentication in Windows, so we use HttpListener based oauth2 authentication, borrowed from MSAL.NET.
		builder.Services.AddSingleton<IOAuthHelperFactory, HttpListenerAuthenticationBrowserFactory>();
		builder.Services.AddSingleton<IUriInterceptorFactory, HttpListenerUriInterceptorFactory>();
		builder.Services.AddSingleton<IDefaultOSBrowser, WindowsDefaultOSBrowser>();
#else
		builder.Services.AddSingleton<IOAuthHelperFactory, MauiAuthenticationBrowserFactory>();
#endif // WINDOWS

		return builder.Build();
	}
}
