// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GitHubViewer.Identity.Data;

public class GitHubViewerUserContext : IdentityDbContext
{
	public GitHubViewerUserContext(DbContextOptions<GitHubViewerUserContext> options)
		: base(options) { }
}
