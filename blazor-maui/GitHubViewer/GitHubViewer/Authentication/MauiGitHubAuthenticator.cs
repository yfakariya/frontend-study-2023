// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Security.Authentication;
using System.Security.Claims;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GitHubViewer.Authentication;

public sealed class MauiGitHubAuthenticator : IGitHubAuthenticator
{
	private static ProviderInformation GetGitHubProviderInformation(string issuerName)
		=> new ProviderInformation
		{
			AuthorizeEndpoint = "https://github.com/login/oauth/authorize",
			EndSessionEndpoint = String.Empty,
			TokenEndpoint = "https://github.com/login/oauth/access_token",
			UserInfoEndpoint = String.Empty,
			IssuerName = issuerName,
		};

	private readonly GitHubUserProfile _userProfile;
	private readonly GitHubTokenInformation _tokenInformation;
	private readonly CredentialsRepository _credentialStore;
	private readonly IdentityModel.OidcClient.Browser.IBrowser _browser;
	private readonly IOptions<GitHubOptions> _options;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILoggerFactory _loggerFactory;

	public ClaimsPrincipal User { get; private set; } = ClaimsPrincipals.Anonymous;

	public event Action<ClaimsPrincipal>? UserChanged;

	public MauiGitHubAuthenticator(
		GitHubUserProfile userProfile,
		GitHubTokenInformation tokenInformation,
		CredentialsRepository credentialStore,
		IdentityModel.OidcClient.Browser.IBrowser browser,
		IHttpClientFactory httpClientFactory,
		ILoggerFactory loggerFactory,
		IOptions<GitHubOptions> options
	)
	{
		_userProfile = userProfile;
		_tokenInformation = tokenInformation;
		_credentialStore = credentialStore;
		_browser = browser;
		_httpClientFactory = httpClientFactory;
		_loggerFactory = loggerFactory;
		_options = options;
	}

	public async ValueTask<ClaimsPrincipal> SignInAutomaticallyAsync(CancellationToken cancellationToken = default)
	{
		var credentials = await _credentialStore.GetCredentialsAsync(cancellationToken);
		ClaimsPrincipal user;
		if (credentials == null)
		{
			user = ClaimsPrincipals.Anonymous;
		}
		else
		{
			var scopes = await _tokenInformation.GetScopesAsync(credentials);
			user = await _userProfile.GetAuthenticatedUserAsync(scopes);
		}

		OnUserChanged(user);
		return user;
	}


	public async ValueTask<ClaimsPrincipal> SignInAsync(string clientId, string clientSecret, bool persists, CancellationToken cancellationToken = default)
	{
		var credentials = await _credentialStore.GetCredentialsAsync(cancellationToken);

		if (credentials == null || credentials.ClientId != clientId)
		{
			var authorityAndIssuer = _options.Value.BaseAddress.ToString();
			var oidcClient =
				new OidcClient(
					new OidcClientOptions
					{
						Authority = authorityAndIssuer,
						ClientId = clientId,
						ClientSecret = clientSecret,
						Scope = String.Empty,
						RedirectUri = Uris.CallbackUriString,
						Browser = _browser,
						HttpClientFactory = o => _httpClientFactory.CreateClient(o.Authority),
						LoggerFactory = _loggerFactory,
						ProviderInformation = GetGitHubProviderInformation(authorityAndIssuer),
						Policy =
						{
							Discovery =
							{
								RequireKeySet = false,
							},
						},
					}
				);

			var result = await oidcClient.LoginAsync(cancellationToken: cancellationToken);
			if (result.IsError)
			{
				ThrowAuthenticationException(result);
			}

			credentials = new OAuth2Credentials(clientId, result.AccessToken);
			await _credentialStore.SetCredentialsAsync(credentials, persists: persists, cancellationToken: cancellationToken);
		}

		var scopes = await _tokenInformation.GetScopesAsync(credentials);
		var user = await _userProfile.GetAuthenticatedUserAsync(scopes);
		OnUserChanged(user);
		return user;
	}

	public async ValueTask SignOutAsync(CancellationToken cancellationToken = default)
	{
		await _credentialStore.ClearCredentialsAsync(cancellationToken);
		OnUserChanged(ClaimsPrincipals.Anonymous);
	}

	private void OnUserChanged(ClaimsPrincipal user)
	{
		var previous = User;
		if (user != previous)
		{
			User = user;
			UserChanged?.Invoke(user);
		}
	}

	[DoesNotReturn]
	private static void ThrowAuthenticationException(Result result) =>
		throw new AuthenticationException(
			String.IsNullOrEmpty(result.ErrorDescription) ?
				result.Error :
				$"{result.Error}: {result.ErrorDescription}"
		);
}
