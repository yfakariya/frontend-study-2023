// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.ViewHelpers;

public interface IParentNavigationRouteProvider
{
	void SetParentRoute(string parentRoute);
}
