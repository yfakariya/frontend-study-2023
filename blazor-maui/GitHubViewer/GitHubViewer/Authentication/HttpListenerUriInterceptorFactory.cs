#pragma warning disable IDE0073
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Based on MDAL.NET https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/Platforms/Features/DefaultOSBrowser/IUriInterceptor.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

namespace GitHubViewer.Authentication;

internal sealed class HttpListenerUriInterceptorFactory : IUriInterceptorFactory
{
	private readonly ILoggerFactory _loggerFactory;

	public HttpListenerUriInterceptorFactory(ILoggerFactory loggerFactory)
	{
		_loggerFactory = loggerFactory;
	}

	public IUriInterceptor CreateInterceptor(
		int minimumPortInclusive,
		int maximumPortInclusive,
		string path,
		Func<Uri, MessageAndHttpCode> responseProducer
	)
	{
		return
			new HttpListenerUriInterceptor(
				minimumPortInclusive,
				maximumPortInclusive,
				path,
				responseProducer,
				_loggerFactory.CreateLogger<HttpListenerUriInterceptor>()
			);
	}
}
