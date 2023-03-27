// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Octokit;

namespace GitHubViewer.Issues
{
	public interface IIssueRepository
	{
		Task<IReadOnlyList<Issue>> GetIssuesAsync(
			IssueSearchCondition condition, 
			int page = 1,
			CancellationToken cancellationToken = default
		);

		Task<Issue> GetIssueAsync(
			string owner,
			string repository,
			int number,
			CancellationToken cancellationToken = default
		);

		Task<string> RenderMarkdownAsync(
			string markdown,
			CancellationToken cancellationToken = default
		);
	}
}
