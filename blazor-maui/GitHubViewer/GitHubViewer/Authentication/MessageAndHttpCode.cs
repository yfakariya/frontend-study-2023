// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using System.Net;

namespace Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;

public record struct MessageAndHttpCode(HttpStatusCode HttpCode, string Message);

