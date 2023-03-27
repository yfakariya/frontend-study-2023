// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.ViewHelpers;

public class AppBarTitleAccessor : IAppBarTitleAccessor
{
	private readonly Action<string> _onSet;
	private string? _currentRoute;

	public AppBarTitleAccessor(Action<string> onSet)
	{
		_onSet = onSet;
	}

	public void Reset(string routePath)
	{
		_currentRoute = routePath;
	}

	public void SetTitle(string routePath, string title)
	{
		if (_currentRoute != routePath)
		{
			return;
		}

		_onSet(title);
	}
}
