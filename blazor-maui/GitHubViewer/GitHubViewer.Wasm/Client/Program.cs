// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer;
using GitHubViewer.Authentication;
using GitHubViewer.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

// Based on AddOidcAuthentication.
// Register post configuration handler for our DefaultAdvancedOidcOptionsConfiguration.
builder.Services.TryAddEnumerable(
	ServiceDescriptor.Scoped<IPostConfigureOptions<RemoteAuthenticationOptions<AdvancedOidcProviderOptions>>, DefaultAdvancedOidcOptionsConfiguration>()
);

// Use AddRemoteAuthentication to specify our custom AdvancedOidcProviderOptions to use client_secret.
builder.Services.AddRemoteAuthentication<RemoteAuthenticationState, RemoteUserAccount, AdvancedOidcProviderOptions>(
	options =>
	{
		// For production, this settings should not be stored.
		// Use server API instead, or use OIDC with PKCE instead of OAuth.
		(var clientId, var clientSecret) = Settings.GetOAuthConfiguration();

		// For github
		options.ProviderOptions.ClientId = clientId ?? String.Empty;
		options.ProviderOptions.ClientSecret = clientSecret ?? String.Empty;

		options.AuthenticationPaths.LogInCallbackPath = Uris.ServerCallbackPath;

		// Clear OIDC scopes because GitHub is not OIDC compliant
		options.ProviderOptions.DefaultScopes.Clear();
		// Add required scopes
		options.ProviderOptions.DefaultScopes.Add("user:email"); // for ASP.NET Core Identity compatibility
		options.ProviderOptions.DefaultScopes.Add("public_repo");
	}
);

ApplicationSetUp.RegisterServices(
	builder.Services,
	builder.Configuration,
	Registration<IBrowserLauncher>.Singleton<NullBrowserLauncher>(),
	Registration<IWindowTitleAccessor>.Singleton<NullWindowTitleAccessor>(),
	Registration<ICredentialsProvider>.Scoped<WebAssemblyTokenCredentialsProvider>(),
	Registration<IGitHubAuthenticator>.Nothing,
	Registration<AuthenticationStateProvider>.Nothing,
	GenericRegistration<ISessionStorage<ValueTuple>>.Scoped<InMemorySessionStorage<ValueTuple>>()
);

await builder.Build().RunAsync().ConfigureAwait(true);

