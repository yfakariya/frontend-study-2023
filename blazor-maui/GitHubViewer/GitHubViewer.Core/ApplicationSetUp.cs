// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using GitHubViewer.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Octokit;

namespace GitHubViewer;

public static class ApplicationSetUp
{
	public static void RegisterServices(IServiceCollection services, bool useScoped)
	{
		services.AddLogging();
		services.AddOptions();
		services.AddAuthorizationCore();
		services.AddHttpClient();
		services.AddOptions<GitHubOptions>();

		if (useScoped)
		{
			services.AddScoped(GetApiConnection);
			services.AddScoped<GitHubUserProfile>();
			services.AddScoped<GitHubTokenInformation>();
		}
		else
		{
			services.AddSingleton(GetApiConnection);
			services.AddSingleton<GitHubUserProfile>();
			services.AddSingleton<GitHubTokenInformation>();
		}

		services.AddSingleton<WeatherForecastService>();


		services.AddMudServices();
	}

	private static IApiConnection GetApiConnection(IServiceProvider serviceProvider)
		=> new ApiConnection(
				new Connection(
					new ProductHeaderValue(
						"GitHubViewer",
						"0.1"
					),
					serviceProvider.GetRequiredService<IOptions<GitHubOptions>>().Value.BaseAddress,
					serviceProvider.GetRequiredService<ICredentialStore>()
				)
			);
}
