// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

// Based on ASP.NET Core code (https://github.com/dotnet/aspnetcore/blob/e56abc45c4f8adc518abfc11a59849d616431e2c/src/Components/WebAssembly/WebAssembly.Authentication/src/Options/DefaultOidcProviderOptionsConfiguration.cs)
// MIT License (https://github.com/dotnet/aspnetcore/blob/e56abc45c4f8adc518abfc11a59849d616431e2c/LICENSE.txt)

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace GitHubViewer.Authentication;

// AuthenticationService.js uses oidc-client.js, and the OidcProviderOptions is treated as OidcClientOptions in the JS,
// so client_secret should be recognized in the JS.
public sealed class AdvancedOidcProviderOptions : OidcProviderOptions
{
	[JsonPropertyName("client_secret")]
	public string? ClientSecret { get; set; }

	public OidcMetadata? Metadata { get; set; }

	public bool DisablesSilentSignIn { get; set; }

	public bool? SkipUserInfo { get; set; }
}

public sealed class OidcMetadata
{
	[JsonPropertyName("issuer")]
	public string IssuerName { get; set; } = null!;

	[JsonPropertyName("authorization_endpoint")]
	public string AuthorizeEndpoint { get; set; } = null!;

	[JsonPropertyName("token_endpoint")]
	public string TokenEndpoint { get; set; } = null!;

	[JsonPropertyName("check_session_iframe")]
	public string? CheckSessionIframe { get; set; } = null!;
}

