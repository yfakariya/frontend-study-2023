#pragma warning disable IDE0073
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Based on MDAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/IUriInterceptor.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace GitHubViewer.Authentication;

internal interface IUriInterceptorFactory
{
	/// <summary>
	/// Creates a new <see cref="IUriInterceptor"/> and let start to listen to http://127.0.0.1:{port}.
	/// Then push back a response such as a display message or a redirect.
	/// </summary>
	/// <remarks>Cancellation is very important as this is typically a long running unmonitored operation</remarks>
	/// <param name="minimumPortInclusive">The minimum port to listen to, inclsuive.</param>
	/// <param name="maximumPortInclusive">The maximum port to listen to, inclsuive.</param>
	/// <param name="path">The path to listen in</param>
	/// <param name="responseProducer">The message to be displayed, or url to be redirected to will be created by this callback</param>
	/// <returns><see cref="IUriInterceptor"/>, which should be ready to listen.</returns>
	IUriInterceptor CreateInterceptor(
		int minimumPortInclusive,
		int maximumPortInclusive,
		string path,
		Func<Uri, MessageAndHttpCode> responseProducer
	);
}
