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
}
