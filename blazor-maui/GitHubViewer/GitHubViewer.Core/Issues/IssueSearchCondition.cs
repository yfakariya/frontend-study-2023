// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Octokit;

namespace GitHubViewer.Issues
{
	public sealed class IssueSearchCondition
	{
		public string? Owner { get; set; }

		public string? Repository { get; set; }

		public int? IssuesPerPage { get; set; }

		public ItemStateFilter State { get; set; }

		public SortDirection SortDirection { get; set; }

		public IssueSort SortProperty { get; set; }

		public DateTimeOffset? Since { get; set; }

		public IReadOnlyList<string> Labels { get; set; } = Array.Empty<string>();
	}
}
