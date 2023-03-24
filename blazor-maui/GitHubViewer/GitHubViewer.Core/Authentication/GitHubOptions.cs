// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.ComponentModel.DataAnnotations;
using GitHubViewer.Configuration;

namespace GitHubViewer.Authentication;

public class GitHubOptions
{
	[Required]
	[CustomValidation(typeof(HttpUriValidator), nameof(HttpUriValidator.Validate))]
	public Uri BaseAddress { get; set; } = new Uri("https://api.github.com");

	[Range(minimum: 1024, maximum: 65535)]
	public int OAuthRedirectMinimumPort { get; set; } = 49152;

	[Range(minimum: 1024, maximum: 65535)]
	public int OAuthRedirectMaximumPort { get; set; } = 65535;
}
