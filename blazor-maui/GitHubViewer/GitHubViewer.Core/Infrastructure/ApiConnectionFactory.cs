// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Authentication;
using Microsoft.Extensions.Options;
using Octokit;
using Octokit.Internal;

namespace GitHubViewer.Infrastructure;

public abstract class ApiConnectionFactory
{
	private static readonly ProductHeaderValue ProductInformation = new("GitHubViewer", "0.1");
	private static readonly IJsonSerializer JsonSerializer = new SimpleJsonSerializer();

	private readonly ICredentialsProvider _credentialsProvider;
	private readonly OctokitHttpClientFactory _httpClientFactory;
	private readonly IOptionsMonitor<GitHubOptions> _options;

	protected ApiConnectionFactory(
		ICredentialsProvider credentialsProvider,
		IHttpMessageHandlerFactory messageHandlerFactory,
		IOptionsMonitor<GitHubOptions> options
	)
	{
		_credentialsProvider = credentialsProvider;
		_httpClientFactory = new OctokitHttpClientFactory(messageHandlerFactory);
		_options = options;
	}

	public async ValueTask<IApiConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
		=> new ApiConnection(
			new Connection(
				ProductInformation,
				_options.CurrentValue.BaseAddress,
				await GetCredentialStoreAsync(_credentialsProvider, cancellationToken).ConfigureAwait(false),
				_httpClientFactory.CreateClient(),
				JsonSerializer
			)
		);

	protected abstract ValueTask<ICredentialStore> GetCredentialStoreAsync(
		ICredentialsProvider credentialsProvider,
		CancellationToken cancellationToken
	);

	private sealed class OctokitHttpClientFactory
	{
		private readonly IHttpMessageHandlerFactory _messageHandlerFactory;

		public OctokitHttpClientFactory(IHttpMessageHandlerFactory messageHandlerFactory)
		{
			_messageHandlerFactory = messageHandlerFactory;
		}

		public IHttpClient CreateClient()
			=> new HttpClientAdapter(
					() => new NonDisposeDelegatingHandler(
						_messageHandlerFactory.CreateHandler(HttpClientNames.Octokit)
					)
				);

		private sealed class NonDisposeDelegatingHandler : DelegatingHandler
		{
			public NonDisposeDelegatingHandler(HttpMessageHandler innerHandler)
				: base(innerHandler) { }
		}
	}
}
