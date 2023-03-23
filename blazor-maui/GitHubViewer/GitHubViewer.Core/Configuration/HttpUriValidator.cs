// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace GitHubViewer.Configuration;

public static class HttpUriValidator
{
	public static ValidationResult? Validate(Uri? value, ValidationContext context)
	{
		if (value == null)
		{
			return ValidationResult.Success;
		}

		if (value.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) || value.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
		{
			return ValidationResult.Success;
		}

		return new ValidationResult("URL scheme must be 'http' or 'https'.");
	}
}
