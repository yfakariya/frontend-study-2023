// See LICENCE file in the root.

import 'dart:io';

/// A value whether this app is running on desktop enviroment.
final bool isDesktop =
    Platform.isWindows || Platform.isMacOS || Platform.isLinux;
