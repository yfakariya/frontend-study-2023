// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace GitHubViewer;

public abstract class GenericRegistration<TService> : Registration<TService>
	where TService : class
{
	private protected GenericRegistration() { }

#pragma warning disable CA1000 // Do not declare static members on generic types
	public static new GenericRegistration<TService> Transient<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
		where TImplementation : class, TService
		=> EffectiveGenericRegistration<TService, TImplementation>.Transient;

	public static new GenericRegistration<TService> Scoped<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
		where TImplementation : class, TService
		=> EffectiveGenericRegistration<TService, TImplementation>.Scoped;

	public static new GenericRegistration<TService> Singleton<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
		where TImplementation : class, TService
		=> EffectiveGenericRegistration<TService, TImplementation>.Singleton;

	public static new GenericRegistration<TService> Nothing
		=> NothingRegistration<TService>.Instance;
#pragma warning restore CA1000 // Do not declare static members on generic types
}
