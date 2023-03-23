// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

// Based on MSAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/MessageAndHttpCode.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

using System.Net;

#pragma warning disable CA1716
namespace Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;
#pragma warning restore CA1716

public record struct MessageAndHttpCode(HttpStatusCode HttpCode, string Message);
