// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using GitHubViewer.Data;
using GitHubViewer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Octokit;
using Octokit.Internal;

namespace GitHubViewer;

public static class ApplicationSetUp
{
	public static void RegisterServices(IServiceCollection services, bool useScoped)
	{
		services.AddLocalization();
		services.AddLogging(logging =>
			{
				logging.AddDebug();
			}
		);
		services.AddOptions();
		services.AddOptions<GitHubOptions>();
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

		if (useScoped)
		{
			services.AddScoped<BasicAuthenticationApiConnectionFactory>();
			services.AddScoped<OAuth2ApiConnectionFactory>();
			services.AddScoped<GitHubUserProfile>();
			services.AddScoped<GitHubTokenInformation>();
		}
		else
		{
			services.AddScoped<BasicAuthenticationApiConnectionFactory>();
			services.AddScoped<OAuth2ApiConnectionFactory>();
			services.AddSingleton<GitHubUserProfile>();
			services.AddSingleton<GitHubTokenInformation>();
		}

		services.AddSingleton<WeatherForecastService>();

		services.AddMudServices();
	}
}
