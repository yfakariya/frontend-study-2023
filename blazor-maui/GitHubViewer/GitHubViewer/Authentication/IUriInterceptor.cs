#pragma warning disable IDE0073
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Based on MDAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/IUriInterceptor.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

namespace GitHubViewer.Authentication;

/// <summary>
/// An abstraction over objects that are able to listen to localhost url (e.g. http://localhost:1234)
/// and to retrieve the whole url, including query params (e.g. http://localhost:1234?code=auth_code)
/// </summary>
internal interface IUriInterceptor : IDisposable
{
	string ListeningUrl { get; }

	/// <summary>
	/// Gets a pending <see cref="Task"/> to ready to handle incoming http request.
	/// </summary>
	Task Ready { get; }

	/// <summary>
	/// Gets a pending <see cref="Task{T}"/> to get the entire url, including query params.
	/// </summary>
	Task<Uri> ListenToSingleRequestAndRespondAsync(CancellationToken cancellationToken = default);
}
