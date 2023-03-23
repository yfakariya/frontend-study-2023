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
}
