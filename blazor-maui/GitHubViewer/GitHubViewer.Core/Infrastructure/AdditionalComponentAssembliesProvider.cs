// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Reflection;

namespace GitHubViewer.Infrastructure;

public sealed class AdditionalComponentAssembliesProvider : IAdditionalComponentAssembliesProvider
{
	private readonly IReadOnlyList<Assembly> _assemblies;

	public AdditionalComponentAssembliesProvider(IEnumerable<Assembly> assemblies)
	{
		_assemblies = assemblies.ToArray();
	}

	public IEnumerable<Assembly> GetAssemblies() => _assemblies;
}
