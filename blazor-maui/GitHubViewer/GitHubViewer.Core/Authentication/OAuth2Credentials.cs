// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

namespace GitHubViewer.Authentication;

// TODO: remove ClientId and ClientSecret and store separately

public record OAuth2Credentials(string ClientId, string ClientSecret, string AccessToken);
