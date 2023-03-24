// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Infrastructure
{
	internal class MauiWindowTitleAccessor : IWindowTitleAccessor
	{
		public bool HasWindow => true;

		public string Title
		{
			get => App.Current?.MainPage?.Window.Title ?? String.Empty;
			set
			{
				var window = App.Current?.MainPage?.Window;
				if (window != null)
				{
					window.Title = value;
				}
			}
		}
	}
}
