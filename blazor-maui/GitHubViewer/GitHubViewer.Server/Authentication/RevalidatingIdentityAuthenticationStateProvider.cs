// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace GitHubViewer.Authentication
{
	public class RevalidatingIdentityAuthenticationStateProvider<TUser>
		: RevalidatingServerAuthenticationStateProvider
		where TUser : class
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly IdentityOptions _options;

		public RevalidatingIdentityAuthenticationStateProvider(
			ILoggerFactory loggerFactory,
			IServiceScopeFactory scopeFactory,
			IOptions<IdentityOptions> optionsAccessor)
			: base(loggerFactory)
		{
			_scopeFactory = scopeFactory;
			_options = optionsAccessor.Value;
		}

		protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

		protected override async Task<bool> ValidateAuthenticationStateAsync(
			AuthenticationState authenticationState, CancellationToken cancellationToken)
		{
			// Get the user manager from a new scope to ensure it fetches fresh data
			var scope = _scopeFactory.CreateScope();
			try
			{
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
				return await ValidateSecurityStampAsync(userManager, authenticationState.User).ConfigureAwait(false);
			}
			finally
			{
				if (scope is IAsyncDisposable asyncDisposable)
				{
					await asyncDisposable.DisposeAsync().ConfigureAwait(false);
				}
				else
				{
					scope.Dispose();
				}
			}
		}

		private async Task<bool> ValidateSecurityStampAsync(UserManager<TUser> userManager, ClaimsPrincipal principal)
		{
			var user = await userManager.GetUserAsync(principal).ConfigureAwait(false);
			if (user == null)
			{
				return false;
			}
			else if (!userManager.SupportsUserSecurityStamp)
			{
				return true;
			}
			else
			{
				var principalStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType);
				var userStamp = await userManager.GetSecurityStampAsync(user).ConfigureAwait(false);
				return principalStamp == userStamp;
			}
		}
	}
}
