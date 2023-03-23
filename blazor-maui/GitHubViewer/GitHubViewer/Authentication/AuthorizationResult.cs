#pragma warning disable IDE0073
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Based on MSAL.NET:
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/src/client/Microsoft.Identity.Client/UI/AuthorizationResult.cs
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/01ecd12464007fc1988b6a127aa0b1b980bca1ed/src/client/Microsoft.Identity.Client/OAuth2/OAuthConstants.cs
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/08e65c731009642083284095149aad0262a15d98/src/client/Microsoft.Identity.Client/OAuth2/OAuth2ResponseBase.cs
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/f54e4cbf81206d6070c3ac9302905a4896611907/src/client/Microsoft.Identity.Client/OAuth2/MsalTokenResponse.cs
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/01ecd12464007fc1988b6a127aa0b1b980bca1ed/src/client/Microsoft.Identity.Client/MsalError.cs
// * https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/01ecd12464007fc1988b6a127aa0b1b980bca1ed/src/client/Microsoft.Identity.Client/Utils/CoreHelpers.cs
// MIT license (https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/blob/03fd41da88d9c23d15dd800ff57f63d2a2ffecea/LICENSE)

using GitHubViewer.Authentication;
using Microsoft.Extensions.Logging;

namespace Microsoft.Identity.Client.UI;

internal class AuthorizationResult
{
	public AuthorizationStatus Status { get; private set; }

	public string Code { get; set; }

	public string Error { get; set; }

	public string? ErrorDescription { get; set; }

	public string CloudInstanceHost { get; set; }

	public string ClientInfo { get; set; }

	/// <summary>
	/// A string that is added to each Authorization Request and is expected to be sent back along with the
	/// authorization code. MSAL is responsible for validating that the state sent is identical to the state received.
	/// </summary>
	/// <remarks>
	/// This is in addition to PKCE, which is validated by the server to ensure that the system redeeming the auth code
	/// is the same as the system who asked for it. It protects against XSRF https://openid.net/specs/openid-connect-core-1_0.html
	/// </remarks>
	public string State { get; set; }

	public static AuthorizationResult FromUri(string webAuthenticationResult, ILogger logger)
	{
		if (String.IsNullOrWhiteSpace(webAuthenticationResult))
		{
			return
				FromStatus(
					AuthorizationStatus.UnknownError,
					Errors.AuthenticationFailed,
					ErrorMessages.AuthorizationServerInvalidResponse
				);
		}

		var resultUri = new Uri(webAuthenticationResult);

		// NOTE: The Fragment property actually contains the leading '#' character and that must be dropped
		var resultData = resultUri.Query;

		if (String.IsNullOrWhiteSpace(resultData))
		{
			return FromStatus(
				AuthorizationStatus.UnknownError,
				Errors.AuthenticationFailed,
				ErrorMessages.AuthorizationServerInvalidResponse
			);
		}

		Dictionary<string, string> uriParams = ParseKeyValueList(resultData.Substring(1), '&', logger);

		return FromParsedValues(uriParams, webAuthenticationResult);
	}

	private static Dictionary<string, string> ParseKeyValueList(string input, char delimiter, ILogger logger)
	{
		var response = new Dictionary<string, string>();

		var queryPairs = SplitWithQuotes(input, delimiter);

		foreach (string queryPair in queryPairs)
		{
			var pair = SplitWithQuotes(queryPair, '=');

			if (pair.Count == 2 && !String.IsNullOrWhiteSpace(pair[0]) && !String.IsNullOrWhiteSpace(pair[1]))
			{
				var key = pair[0];
				var value = pair[1];

				// Url decoding is needed for parsing OAuth response, but not for parsing WWW-Authenticate header in 401 challenge
				key = UrlDecode(key).Trim().ToLowerInvariant();
				value = UrlDecode(value).Trim().Trim('\"').Trim();

				if (response.ContainsKey(key))
				{
					logger.RedundantKeysAreDetected(key);
				}

				response[key] = value;
			}
		}

		return response;
	}

	internal static IReadOnlyList<string> SplitWithQuotes(string input, char delimiter)
	{
		if (String.IsNullOrWhiteSpace(input))
		{
			return Array.Empty<string>();
		}

		var items = new List<string>();
		var startIndex = 0;
		var insideString = false;
		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == delimiter && !insideString)
			{
				var item = input.Substring(startIndex, i - startIndex);
				if (!String.IsNullOrWhiteSpace(item))
				{
					items.Add(item);
				}

				startIndex = i + 1;
			}
			else if (input[i] == '"')
			{
				insideString = !insideString;
			}
		}

		{
			var item = input.Substring(startIndex);
			if (!String.IsNullOrWhiteSpace(item))
			{
				items.Add(item);
			}
		}

		return items;
	}
	private static string UrlDecode(string message)
	{
		if (String.IsNullOrEmpty(message))
		{
			return message;
		}

		return Uri.UnescapeDataString(message.Replace("+", "%20"));
	}

	private static AuthorizationResult FromParsedValues(Dictionary<string, string> parameters, string? url = null)
	{
		if (parameters.TryGetValue(TokenResponseClaim.Error, out var error))
		{
			if (parameters.TryGetValue(TokenResponseClaim.ErrorSubcode, out var subcode))
			{
				if (TokenResponseClaim.ErrorSubcodeCancel.Equals(subcode, StringComparison.OrdinalIgnoreCase))
				{
					return FromStatus(AuthorizationStatus.UserCancel);
				}
			}

			return
				FromStatus(
					AuthorizationStatus.ProtocolError,
					error,
					parameters.TryGetValue(TokenResponseClaim.ErrorDescription, out var description) ? description : null
				);
		}

		var authResult = new AuthorizationResult
		{
			Status = AuthorizationStatus.Success
		};

		if (parameters.ContainsKey(OAuth2Parameter.State))
		{
			authResult.State = parameters[OAuth2Parameter.State];
		}

		if (parameters.ContainsKey(TokenResponseClaim.CloudInstanceHost))
		{
			authResult.CloudInstanceHost = parameters[TokenResponseClaim.CloudInstanceHost];
		}

		if (parameters.ContainsKey(TokenResponseClaim.ClientInfo))
		{
			authResult.ClientInfo = parameters[TokenResponseClaim.ClientInfo];
		}

		if (parameters.ContainsKey(TokenResponseClaim.Code))
		{
			authResult.Code = parameters[TokenResponseClaim.Code];
		}
		else if (!String.IsNullOrEmpty(url) && url.StartsWith("msauth://", StringComparison.OrdinalIgnoreCase))
		{
			authResult.Code = url;
		}
		else
		{
			return FromStatus(
				AuthorizationStatus.UnknownError,
				Errors.AuthenticationFailed,
				ErrorMessages.AuthorizationServerInvalidResponse
			);
		}

		return authResult;
	}

	internal static AuthorizationResult FromStatus(AuthorizationStatus status)
	{
		if (status == AuthorizationStatus.Success)
		{
			throw new InvalidOperationException("Use the FromUri builder");
		}

		var result = new AuthorizationResult() { Status = status };

		if (status == AuthorizationStatus.UserCancel)
		{
			result.Error = Errors.AuthenticationCanceledError;
			result.ErrorDescription = ErrorMessages.AuthenticationCanceled;
		}
		else if (status == AuthorizationStatus.UnknownError)
		{
			result.Error = Errors.UnknownError;
			result.ErrorDescription = ErrorMessages.Unknown;
		}

		return result;
	}

	public static AuthorizationResult FromStatus(AuthorizationStatus status, string error, string? errorDescription)
	{
		return
			new AuthorizationResult()
			{
				Status = status,
				Error = error,
				ErrorDescription = errorDescription,
			};
	}

	private static class OAuth2Parameter
	{
		//public const string ResponseType = "response_type";
		//public const string GrantType = "grant_type";
		//public const string ClientId = "client_id";
		//public const string ClientSecret = "client_secret";
		//public const string ClientAssertion = "client_assertion";
		//public const string ClientAssertionType = "client_assertion_type";
		//public const string RefreshToken = "refresh_token";
		//public const string RedirectUri = "redirect_uri";
		//public const string Resource = "resource";
		//public const string Code = "code";
		//public const string DeviceCode = "device_code";
		//public const string Scope = "scope";
		//public const string Assertion = "assertion";
		//public const string RequestedTokenUse = "requested_token_use";
		//public const string Username = "username";
		//public const string Password = "password";
		//public const string LoginHint = "login_hint"; // login_hint is not standard oauth2 parameter
		//public const string CorrelationId = OAuth2Header.CorrelationId;
		public const string State = "state";

		//public const string CodeChallengeMethod = "code_challenge_method";
		//public const string CodeChallenge = "code_challenge";
		//public const string PkceCodeVerifier = "code_verifier";
		//// correlation id is not standard oauth2 parameter
		//public const string LoginReq = "login_req";
		//public const string DomainReq = "domain_req";

		//public const string Prompt = "prompt"; // prompt is not standard oauth2 parameter
		//public const string ClientInfo = "client_info"; // restrict_to_hint is not standard oauth2 parameter

		//public const string Claims = "claims"; // claims is not a standard oauth2 parameter

		//public const string TokenType = "token_type"; // not a standard OAuth2 param
		//public const string RequestConfirmation = "req_cnf"; // not a standard OAuth2 param
		//public const string SpaCode = "return_spa_code"; // not a standard OAuth2 param
	}

	private static class TokenResponseClaim
	{
		//public const string Claims = "claims";
		public const string Error = "error";
		//public const string SubError = "suberror";
		public const string ErrorDescription = "error_description";
		//public const string ErrorCodes = "error_codes";
		//public const string CorrelationId = "correlation_id";

		public const string Code = "code";
		//public const string TokenType = "token_type";
		//public const string AccessToken = "access_token";
		//public const string RefreshToken = "refresh_token";
		//public const string IdToken = "id_token";
		//public const string Scope = "scope";
		public const string ClientInfo = "client_info";
		//public const string ExpiresIn = "expires_in";
		public const string CloudInstanceHost = "cloud_instance_host_name";
		//public const string CreatedOn = "created_on";
		//public const string ExtendedExpiresIn = "ext_expires_in";
		//public const string Authority = "authority";
		//public const string FamilyId = "foci";
		//public const string RefreshIn = "refresh_in";
		//public const string SpaCode = "spa_code";
		public const string ErrorSubcode = "error_subcode";
		public const string ErrorSubcodeCancel = "cancel";

		//public const string TenantId = "tenant_id";
		//public const string Upn = "username";
		//public const string LocalAccountId = "local_account_id";


	}

	private static class Errors
	{
		/// <summary>
		/// Authentication canceled.
		/// <para>What happens?</para>The user had canceled the authentication, for instance by closing the authentication dialog
		/// <para>Mitigation</para>None, you cannot get a token to call the protected API. You might want to inform the user
		/// </summary>
		public const string AuthenticationCanceledError = "authentication_canceled";

		/// <summary>
		/// Authentication failed.
		/// <para>What happens?</para>
		/// The authentication failed. For instance the user did not enter the right password
		/// <para>Mitigation</para>
		/// Inform the user to retry.
		/// </summary>
		public const string AuthenticationFailed = "authentication_failed";

		/// <summary>
		/// Unknown Error occurred.
		/// <para>Mitigation</para> None. You might want to inform the end user.
		/// </summary>
		public const string UnknownError = "unknown_error";
	}

	private static class ErrorMessages
	{
		public const string AuthorizationServerInvalidResponse = "The authorization server returned an invalid response. ";

		public const string AuthenticationCanceled = "User canceled authentication. ";

		public const string Unknown = "Unknown error";
	}
}
