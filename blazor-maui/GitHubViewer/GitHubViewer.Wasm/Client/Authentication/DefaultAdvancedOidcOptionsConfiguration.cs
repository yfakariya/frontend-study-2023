// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

// Based on ASP.NET Core code (https://github.com/dotnet/aspnetcore/blob/e56abc45c4f8adc518abfc11a59849d616431e2c/src/Components/WebAssembly/WebAssembly.Authentication/src/Options/DefaultOidcProviderOptionsConfiguration.cs)
// MIT License (https://github.com/dotnet/aspnetcore/blob/e56abc45c4f8adc518abfc11a59849d616431e2c/LICENSE.txt)

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace GitHubViewer.Authentication;

internal sealed class DefaultAdvancedOidcOptionsConfiguration : IPostConfigureOptions<RemoteAuthenticationOptions<AdvancedOidcProviderOptions>>
{
	private readonly NavigationManager _navigationManager;

	public DefaultAdvancedOidcOptionsConfiguration(NavigationManager navigationManager)
	{
		_navigationManager = navigationManager;
	}

	public void Configure(RemoteAuthenticationOptions<AdvancedOidcProviderOptions> options)
	{
		options.UserOptions.AuthenticationType ??= options.ProviderOptions.ClientId;

		var redirectUri = options.ProviderOptions.RedirectUri;
		if (redirectUri == null || !Uri.TryCreate(redirectUri, UriKind.Absolute, out _))
		{
			redirectUri ??= "authentication/login-callback";
			options.ProviderOptions.RedirectUri = _navigationManager
				.ToAbsoluteUri(redirectUri).AbsoluteUri;
		}

		var logoutUri = options.ProviderOptions.PostLogoutRedirectUri;
		if (logoutUri == null || !Uri.TryCreate(logoutUri, UriKind.Absolute, out _))
		{
			logoutUri ??= "authentication/logout-callback";
			options.ProviderOptions.PostLogoutRedirectUri = _navigationManager
				.ToAbsoluteUri(logoutUri).AbsoluteUri;
		}
	}

	public void PostConfigure(string? name, RemoteAuthenticationOptions<AdvancedOidcProviderOptions> options)
	{
		if (name == Options.DefaultName)
		{
			Configure(options);
		}
	}
}
