// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.Extensions.DependencyInjection;

namespace GitHubViewer;

internal sealed class NothingRegistration<TService, TImplementation> : Registration<TService>
	where TService : class
	where TImplementation : class, TService
{
	public static NothingRegistration<TService, TImplementation> Instance { get; }
		= new NothingRegistration<TService, TImplementation>();

	private NothingRegistration() { }

	public override void Register(IServiceCollection services) { }
}
