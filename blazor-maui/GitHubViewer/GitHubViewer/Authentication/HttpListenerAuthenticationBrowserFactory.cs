// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GitHubViewer.Authentication;

internal sealed class HttpListenerAuthenticationBrowserFactory : IOAuthHelperFactory
{
	private readonly IDefaultOSBrowser _defaultOSBrowser;
	private readonly IUriInterceptorFactory _interceptorFactory;
	private readonly IOptionsMonitor<GitHubOptions> _options;
	private readonly ILoggerFactory _loggerFactory;

	public HttpListenerAuthenticationBrowserFactory(
		IDefaultOSBrowser defaultOSBrowser,
		IUriInterceptorFactory uriInterceptorFactory,
		IOptionsMonitor<GitHubOptions> options,
		ILoggerFactory loggerFactory
	)
	{
		_defaultOSBrowser = defaultOSBrowser;
		_interceptorFactory = uriInterceptorFactory;
		_options = options;
		_loggerFactory = loggerFactory;
	}

	public IOAuthHelper CreateHelper(Uri redirectUri)
		=> new HttpListenerAuthenticationBrowser(
			redirectUri.AbsolutePath,
			_defaultOSBrowser,
			_interceptorFactory,
			_options,
			_loggerFactory.CreateLogger<HttpListenerAuthenticationBrowser>()
		);
}
