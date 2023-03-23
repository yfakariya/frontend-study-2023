#pragma warning disable IDE0073
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Based on MDAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/IUriInterceptor.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

namespace Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

/// <summary>
/// An abstraction over objects that are able to listen to localhost url (e.g. http://localhost:1234)
/// and to retrieve the whole url, including query params (e.g. http://localhost:1234?code=auth_code_from_aad)
/// </summary>
internal interface IUriInterceptor
{
	/// <summary>
	/// Listens to http://localhost:{port} and retrieve the entire url, including query params. Then
	/// push back a response such as a display message or a redirect.
	/// </summary>
	/// <remarks>Cancellation is very important as this is typically a long running unmonitored operation</remarks>
	/// <param name="port">the port to listen to</param>
	/// <param name="path">the path to listen in</param>
	/// <param name="responseProducer">The message to be displayed, or url to be redirected to will be created by this callback</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Full redirect uri</returns>
	Task<Uri> ListenToSingleRequestAndRespondAsync(
		int port,
		string path,
		Func<Uri, MessageAndHttpCode> responseProducer,
		CancellationToken cancellationToken
	);
}
