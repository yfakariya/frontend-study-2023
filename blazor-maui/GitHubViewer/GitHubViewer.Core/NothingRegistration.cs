// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.Extensions.DependencyInjection;

namespace GitHubViewer;

internal sealed class NothingRegistration<TService> : GenericRegistration<TService>
	where TService : class
{
	public static NothingRegistration<TService> Instance { get; }
		= new NothingRegistration<TService>();

	private NothingRegistration() { }

	public override void Register(IServiceCollection services) { }
}
