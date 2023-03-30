// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Net;
using Microsoft.Extensions.Logging;

namespace GitHubViewer.Authentication;

internal static partial class Log
{
	[LoggerMessage(
		EventId = 1001,
		Level = LogLevel.Information,
		Message = "Listening for authorization code on {urlToListenTo}"
	)]
	public static partial void StartListening(this ILogger logger, string urlToListenTo);

	[LoggerMessage(
		EventId = 1002,
		Level = LogLevel.Debug,
		Message = "HttpListner received a message on {urlToListenTo}"
	)]
	public static partial void ListenerReceivedMessage(this ILogger logger, string urlToListenTo);

	[LoggerMessage(
		EventId = 1003,
		Level = LogLevel.Information,
		Message = "HttpListenerException - cancellation requested?: {isCancellationRequested}"
	)]
	public static partial void ListenerThrewException(this ILogger logger, Exception exception, bool isCancellationRequested);

	[LoggerMessage(
		EventId = 1004,
		Level = LogLevel.Warning,
		Message = "Default OS Browser intercepted an Uri with an error: {error} {errorDescription}"
	)]
	public static partial void ErrorIsIntercepted(this ILogger logger, string error, string? errorDescription);

	[LoggerMessage(
		EventId = 1005,
		Level = LogLevel.Information,
		Message = "Processing a response message to the browser. HttpStatus: {statusCode}"
	)]
	public static partial void ProcessingResponseToBrowser(this ILogger logger, HttpStatusCode statusCode);

	[LoggerMessage(
		EventId = 1011,
		Level = LogLevel.Information,
		Message = "Access token is invalidated."
	)]
	public static partial void AccessTokenIsInvalidated(this ILogger logger, Exception exception);
}
