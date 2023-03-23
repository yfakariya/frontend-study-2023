// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace GitHubViewer.Authentication;

public class GitHubAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
	private readonly IGitHubAuthenticator _authenticator;

	public AuthenticationState Current { get; private set; } = new AuthenticationState(ClaimsPrincipals.Anonymous);

	public GitHubAuthenticationStateProvider(IGitHubAuthenticator authenticator)
	{
		_authenticator = authenticator;
		authenticator.UserChanged += OnUserChanged;
	}

	public void Dispose()
	{
		_authenticator.UserChanged -= OnUserChanged;
	}

	private void OnUserChanged(ClaimsPrincipal user)
	{
		Current = new AuthenticationState(user);
		NotifyAuthenticationStateChanged(Task.FromResult(Current));
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		Current = new AuthenticationState(await _authenticator.SignInAutomaticallyAsync());
		return Current;
	}
}
