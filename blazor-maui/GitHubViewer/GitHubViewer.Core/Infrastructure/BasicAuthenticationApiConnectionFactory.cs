// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using Microsoft.Extensions.Options;
using Octokit;

namespace GitHubViewer.Infrastructure;

public sealed class BasicAuthenticationApiConnectionFactory : ApiConnectionFactory
{
	public BasicAuthenticationApiConnectionFactory(
		ICredentialsProvider credentialsRepository,
		IHttpMessageHandlerFactory messageHandlerFactory,
		IOptionsMonitor<GitHubOptions> options
	) : base(credentialsRepository, messageHandlerFactory, options) { }

	protected override ValueTask<ICredentialStore> GetCredentialStoreAsync(
		ICredentialsProvider credentialsProvider,
		CancellationToken cancellationToken
	)
		=> credentialsProvider.GetBasicAuthenticationCredentialStore(cancellationToken);
}
