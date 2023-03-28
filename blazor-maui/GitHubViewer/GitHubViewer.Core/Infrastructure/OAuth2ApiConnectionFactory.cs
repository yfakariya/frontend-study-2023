// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using Microsoft.Extensions.Options;
using Octokit;

namespace GitHubViewer.Infrastructure;

public sealed class OAuth2ApiConnectionFactory : ApiConnectionFactory
{
	public OAuth2ApiConnectionFactory(
		ICredentialsProvider credentialsProvider,
		IHttpMessageHandlerFactory messageHandlerFactory,
		IOptionsMonitor<GitHubOptions> options
	) : base(credentialsProvider, messageHandlerFactory, options) { }

	protected override ValueTask<ICredentialStore> GetCredentialStoreAsync(
		ICredentialsProvider credentialsProvider,
		CancellationToken cancellationToken
	)
		=> credentialsProvider.GetOAuth2CredentialStore(cancellationToken);
}
