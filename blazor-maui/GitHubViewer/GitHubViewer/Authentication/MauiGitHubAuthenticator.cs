// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Claims;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GitHubViewer.Authentication;

public sealed class MauiGitHubAuthenticator : IGitHubAuthenticator
{
	private static ProviderInformation GetGitHubProviderInformation(string issuerName)
		=> new()
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
	private readonly IOptionsMonitor<GitHubOptions> _options;
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
		IOptionsMonitor<GitHubOptions> options
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
		var credentials = await _credentialStore.GetCredentialsAsync(cancellationToken).ConfigureAwait(false);
		ClaimsPrincipal user;
		if (credentials == null)
		{
			user = ClaimsPrincipals.Anonymous;
		}
		else
		{
			var scopes = await _tokenInformation.GetScopesAsync(credentials, cancellationToken).ConfigureAwait(false);
			user = await _userProfile.GetAuthenticatedUserAsync(scopes, cancellationToken).ConfigureAwait(false);
		}

		OnUserChanged(user);
		return user;
	}


	public async ValueTask<ClaimsPrincipal> SignInAsync(string clientId, string clientSecret, bool persists, CancellationToken cancellationToken = default)
	{
		var credentials = await _credentialStore.GetCredentialsAsync(cancellationToken).ConfigureAwait(false);

		if (credentials == null || credentials.ClientId != clientId || credentials.ClientSecret != clientSecret)
		{
			var authorityAndIssuer = _options.CurrentValue.BaseAddress.ToString();
			var oidcClient =
				new OidcClient(
					new OidcClientOptions
					{
						Authority = authorityAndIssuer,
						ClientId = clientId,
						ClientSecret = clientSecret,
						Scope = String.Empty,
						RedirectUri =
							RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
							? String.Format(CultureInfo.InvariantCulture, Uris.WindowsCallbackUriStringTemplate, _options.CurrentValue.OAuthRedirectPort)
							: Uris.MobileCallbackUriString,
						Browser = _browser,
						HttpClientFactory = o => _httpClientFactory.CreateClient(o.Authority),
						LoggerFactory = _loggerFactory,
						ProviderInformation = GetGitHubProviderInformation(authorityAndIssuer),
						LoadProfile = false,
						Policy =
						{
							Discovery =
							{
								RequireKeySet = false,
							},
						},
					}
				);

			var result = await oidcClient.LoginAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
			if (result.IsError)
			{
				ThrowAuthenticationException(result);
			}

			credentials = new OAuth2Credentials(clientId, clientSecret, result.AccessToken);
			await _credentialStore.SetCredentialsAsync(credentials, persists: persists, cancellationToken: cancellationToken).ConfigureAwait(false);
		}

		var scopes = await _tokenInformation.GetScopesAsync(credentials, cancellationToken).ConfigureAwait(false);
		var user = await _userProfile.GetAuthenticatedUserAsync(scopes, cancellationToken).ConfigureAwait(false);
		OnUserChanged(user);
		return user;
	}

	public async ValueTask SignOutAsync(CancellationToken cancellationToken = default)
	{
		await _credentialStore.ClearCredentialsAsync(cancellationToken).ConfigureAwait(false);
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
