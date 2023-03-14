// See LICENCE file in the root.

import 'package:flutter/material.dart';

import 'app.dart';
import 'l10n/l10n.g.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  LocaleSettings.useDeviceLocale();

  runApp(const App());
}
