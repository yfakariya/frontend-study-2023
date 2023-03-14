// See LICENCE file in the root.

import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:form_builder_validators/form_builder_validators.dart';

import 'l10n/l10n.g.dart';
import 'routes.dart';

/// This is required to work slang correctly.
class _App extends ConsumerWidget {
  const _App();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final router = ref.watch(routerProvider);
    return MaterialApp.router(
      theme: ThemeData.light(useMaterial3: true),
      darkTheme: ThemeData.dark(useMaterial3: true),
      localizationsDelegates: [
        ...GlobalMaterialLocalizations.delegates,
        FormBuilderLocalizations.delegate,
      ],
      locale: TranslationProvider.of(context).flutterLocale,
      supportedLocales: AppLocaleUtils.supportedLocales,
      routerConfig: router,
    );
  }
}

/// Application.
class App extends StatelessWidget {
  /// Constructor.
  const App();

  @override
  Widget build(BuildContext context) => ProviderScope(
        child: TranslationProvider(
          child: const _App(),
        ),
      );
}
