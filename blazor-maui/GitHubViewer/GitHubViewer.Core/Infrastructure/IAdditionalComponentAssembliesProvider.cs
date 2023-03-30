// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Reflection;

namespace GitHubViewer.Infrastructure;

public interface IAdditionalComponentAssembliesProvider
{
	IEnumerable<Assembly> GetAssemblies();
}
