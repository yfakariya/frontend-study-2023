// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Security.Claims;

namespace GitHubViewer.Authentication;

public interface IGitHubAuthenticator
{
	ClaimsPrincipal User { get; }

	event Action<ClaimsPrincipal>? UserChanged;

	ValueTask<ClaimsPrincipal> SignInAutomaticallyAsync(CancellationToken cancellationToken = default);

	// Although we know these parameters should be stored in configuration rather than input by user,
	// we take them becuase we want to build input form for study.

	ValueTask<ClaimsPrincipal> SignInAsync(string clientId, string clientSecret, bool persists, CancellationToken cancellationToken = default);

	ValueTask SignOutAsync(CancellationToken cancellationToken = default);
}
