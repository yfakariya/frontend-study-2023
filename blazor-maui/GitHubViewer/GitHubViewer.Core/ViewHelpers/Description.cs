// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace GitHubViewer.ViewHelpers;

public static class Description
{
	public static string FromDisplay<T>(Expression<Func<T>> expression)
		=> (expression.Body as MemberExpression)?.Member.GetCustomAttribute<DisplayAttribute>(inherit: true)?.GetDescription() ?? String.Empty;
}
