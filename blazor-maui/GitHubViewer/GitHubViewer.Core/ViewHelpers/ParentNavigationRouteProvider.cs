// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.ViewHelpers;

public class ParentNavigationRouteProvider : IParentNavigationRouteProvider
{
	public string? ParentRoute { get; private set; }

	public void Reset() => ParentRoute = null;

	public void SetParentRoute(string parentRoute) => ParentRoute = parentRoute;
}
