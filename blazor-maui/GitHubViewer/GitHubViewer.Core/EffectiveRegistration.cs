// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.Extensions.DependencyInjection;

namespace GitHubViewer;

internal sealed class EffectiveRegistration<TService, TImplementation> : Registration<TService>
	where TService : class
	where TImplementation : class, TService
{
	public static EffectiveRegistration<TService, TImplementation> Transient { get; }
		= new EffectiveRegistration<TService, TImplementation>(ServiceLifetime.Transient);

	public static EffectiveRegistration<TService, TImplementation> Scoped { get; }
		= new EffectiveRegistration<TService, TImplementation>(ServiceLifetime.Scoped);

	public static EffectiveRegistration<TService, TImplementation> Singleton { get; }
		= new EffectiveRegistration<TService, TImplementation>(ServiceLifetime.Singleton);

	private readonly ServiceLifetime _lifeTime;

	private EffectiveRegistration(ServiceLifetime lifeTime)
	{
		_lifeTime = lifeTime;
	}

	public override void Register(IServiceCollection services)
	{
		switch (_lifeTime)
		{
			case ServiceLifetime.Transient:
			{
				services.AddTransient<TService, TImplementation>();
				break;
			}
			case ServiceLifetime.Scoped:
			{
				services.AddScoped<TService, TImplementation>();
				break;
			}
			default:
			{
				services.AddSingleton<TService, TImplementation>();
				break;
			}
		}
	}
}
