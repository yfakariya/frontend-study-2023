// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubViewer;

public static class TypeParameterPlaceHolder { }

internal sealed class EffectiveGenericRegistration<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation> : GenericRegistration<TService>
	where TService : class
	where TImplementation : class, TService
{
	private static readonly Type? ServiceType;
	private static readonly Type? ImplementationType;
	private static readonly string? ErrorMessage;
	private static readonly string? ErrorParameterName;

	public static EffectiveGenericRegistration<TService, TImplementation> Transient { get; }
	= new EffectiveGenericRegistration<TService, TImplementation>(ServiceLifetime.Transient);

	public static EffectiveGenericRegistration<TService, TImplementation> Scoped { get; }
		= new EffectiveGenericRegistration<TService, TImplementation>(ServiceLifetime.Scoped);

	public static EffectiveGenericRegistration<TService, TImplementation> Singleton { get; }
		= new EffectiveGenericRegistration<TService, TImplementation>(ServiceLifetime.Singleton);

	static EffectiveGenericRegistration()
	{
		var serviceType = typeof(TService).GetGenericTypeDefinition();
		if (serviceType == null)
		{
			ErrorMessage = $"Cannot lead a generic type definition from service type {typeof(TService)}. If you want to use constructed generic type or non generic type, use Registration<TServcie> instead.";
			ErrorParameterName = nameof(TService);
			return;
		}

		var implementationType = typeof(TImplementation).GetGenericTypeDefinition();
		if (implementationType == null)
		{
			ErrorMessage = $"Cannot lead a generic type definition from implementation type {typeof(TImplementation)}.";
			ErrorParameterName = nameof(TImplementation);
			return;
		}

		var genericParametersInService = serviceType.GenericTypeArguments.Count(t => t.IsGenericTypeParameter);
		var genericParametersInImplementation = implementationType.GenericTypeArguments.Count(t => t.IsGenericTypeParameter);

		if (genericParametersInService != genericParametersInImplementation)
		{
			ErrorMessage = $"Service type {typeof(TService)} has {genericParametersInService} generic type parameters, but impelementation type {typeof(TImplementation)} has {genericParametersInImplementation} generic type parameters. These arity must match.";
			ErrorParameterName = nameof(TImplementation);
			return;
		}

		// OK
		ServiceType = serviceType;
		ImplementationType = implementationType;
	}

	private readonly ServiceLifetime _lifeTime;

	internal EffectiveGenericRegistration(ServiceLifetime lifeTime)
	{
		_lifeTime = lifeTime;
	}

	private static (string Message, string ParameterName)? CheckTypeError()
	{
		var genericParametersInService = typeof(TService).GetGenericTypeDefinition()?.GenericTypeArguments.Count(t => t.IsGenericTypeParameter);

		if (genericParametersInService == null)
		{
			return (
				Message: $"Cannot lead a generic type definition from service type {typeof(TService)}. If you want to use constructed generic type or non generic type, use Registration<TServcie> instead.",
				ParameterName: nameof(TService)
			);
		}

		var genericParametersInImplementation = typeof(TImplementation).GetGenericTypeDefinition()?.GenericTypeArguments.Count(t => t.IsGenericTypeParameter);

		if (genericParametersInService != genericParametersInImplementation)
		{
			return (
				Message: $"Service type {typeof(TService)} has {genericParametersInService ?? 0} generic type parameters, but impelementation type {typeof(TImplementation)} has {genericParametersInImplementation ?? 0} generic type parameters. These arity must match.",
				ParameterName: nameof(TImplementation)
			);
		}

		// OK
		return null;
	}

	private static void EnsureValid()
	{
		if (ErrorMessage != null)
		{
			Debug.Assert(ErrorParameterName != null);
			throw new ArgumentException(ErrorMessage, ErrorParameterName);
		}
	}

	public override void Register(IServiceCollection services)
	{
		EnsureValid();
		Debug.Assert(ServiceType != null);
		Debug.Assert(ImplementationType != null);

		switch (_lifeTime)
		{
			case ServiceLifetime.Transient:
			{
				services.AddTransient(ServiceType, ImplementationType);
				break;
			}
			case ServiceLifetime.Scoped:
			{
				services.AddScoped(ServiceType, ImplementationType);
				break;
			}
			default:
			{
				services.AddSingleton(ServiceType, ImplementationType);
				break;
			}
		}
	}
}
