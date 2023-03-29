// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using GitHubViewer.Infrastructure;
using GitHubViewer.Issues;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Octokit;
using Octokit.Internal;

namespace GitHubViewer;

public static class ApplicationSetUp
{
	public static void RegisterServices(
		IServiceCollection services,
		IConfiguration configuration,
		Registration<IBrowserLauncher> browserLauncherRegistration,
		Registration<IWindowTitleAccessor> windowTitleAccessorRegistration,
		Registration<ICredentialsProvider> credentialsProviderRegistration,
		Registration<IGitHubAuthenticator> gitHubAuthenticatorRegistration,
		Registration<AuthenticationStateProvider> authenticationStateProviderRegistration,
		GenericRegistration<ISessionStorage<ValueTuple>> sessionStorageRegistration
	)
	{
		services.AddLocalization();
		services.AddLogging(logging =>
			{
				logging.AddDebug();
			}
		);
		services.AddOptions();
		services.AddOptions<AuthenticationOptions>()
			.Bind(configuration.GetSection("Authentication"))
			.ValidateDataAnnotations();
		services.AddOptions<GitHubOptions>()
			.Bind(configuration.GetSection("GitHub"))
			.ValidateDataAnnotations();
		services.AddAuthorizationCore();
		services.AddHttpClient();
		services.AddHttpClient(HttpClientNames.Octokit)
			.ConfigurePrimaryHttpMessageHandler(HttpMessageHandlerFactory.CreateDefault);

		services.AddSingleton(
			new ProductHeaderValue(
				"GitHubViewer",
				"0.1"
			)
		);

		services.AddScoped<BasicAuthenticationApiConnectionFactory>();
		services.AddScoped<OAuth2ApiConnectionFactory>();
		services.AddScoped<GitHubUserProfile>();
		services.AddScoped<GitHubTokenInformation>();

		services.AddScoped<IIssueRepository, IssueRepository>();

		browserLauncherRegistration.Register(services);
		windowTitleAccessorRegistration.Register(services);
		credentialsProviderRegistration.Register(services);
		gitHubAuthenticatorRegistration.Register(services);
		authenticationStateProviderRegistration.Register(services);
		sessionStorageRegistration.Register(services);

		services.AddMudServices();
	}
}
