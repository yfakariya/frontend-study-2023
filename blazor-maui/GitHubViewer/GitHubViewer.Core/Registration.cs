// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubViewer;

public abstract class Registration<TService>
	where TService : class
{
	private protected Registration() { }

	public abstract void Register(IServiceCollection services);

#pragma warning disable CA1000 // Do not declare static members on generic types
	public static Registration<TService> Transient<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
		where TImplementation : class, TService
		=> EffectiveRegistration<TService, TImplementation>.Transient;

	public static Registration<TService> Scoped<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
		where TImplementation : class, TService
		=> EffectiveRegistration<TService, TImplementation>.Scoped;

	public static Registration<TService> Singleton<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
		where TImplementation : class, TService
		=> EffectiveRegistration<TService, TImplementation>.Singleton;

	public static Registration<TService> Nothing
		=> NothingRegistration<TService>.Instance;
#pragma warning restore CA1000 // Do not declare static members on generic types
}
