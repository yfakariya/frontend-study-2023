// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer.Infrastructure;
using Octokit;

namespace GitHubViewer.Issues
{
	public sealed class IssueRepository : IIssueRepository
	{
		private readonly OAuth2ApiConnectionFactory _connectionFactory;
		public IssueRepository(OAuth2ApiConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<IReadOnlyList<Issue>> GetIssuesAsync(
			IssueSearchCondition condition,
			int page = 1,
			CancellationToken cancellationToken = default
		)
		{
			var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
			var client = new IssuesClient(connection);

			var apiOptions =
				new ApiOptions
				{
					StartPage = page,
					PageSize = condition.IssuesPerPage,
					PageCount = 1,
				};

			Task<IReadOnlyList<Issue>> task;
			if (String.IsNullOrEmpty(condition.Repository) || String.IsNullOrEmpty(condition.Owner))
			{
				var issueRequest =
					new IssueRequest
					{
						Since = condition.Since,
						SortDirection = condition.SortDirection,
						SortProperty = condition.SortProperty,
						State = condition.State,
						Filter = IssueFilter.All,
					};

				foreach (var label in condition.Labels)
				{
					issueRequest.Labels.Add(label);
				}

				task = client.GetAllForCurrent(issueRequest, apiOptions);
			}
			else
			{
				var issueRequest =
					new RepositoryIssueRequest
					{
						Since = condition.Since,
						SortDirection = condition.SortDirection,
						SortProperty = condition.SortProperty,
						State = condition.State,
						Filter = IssueFilter.All,
					};

				foreach (var label in condition.Labels)
				{
					issueRequest.Labels.Add(label);
				}

				task = client.GetAllForRepository(condition.Owner!, condition.Repository!, issueRequest, apiOptions);
			}

			return await task.ConfigureAwait(false);
		}

		public async Task<Issue> GetIssueAsync(
			string owner,
			string repository,
			int number,
			CancellationToken cancellationToken = default
		)
		{
			var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
			var client = new IssuesClient(connection);
			return await client.Get(owner, repository, number).ConfigureAwait(false);
		}

		public async Task<string> RenderMarkdownAsync(
			string markdown,
			CancellationToken cancellationToken = default
		)
		{
			var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
			var client = new MarkdownClient(connection);
			return await client.RenderRawMarkdown(markdown).ConfigureAwait(false);
		}
	}
}
