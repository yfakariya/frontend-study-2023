// See LICENCE file in the root.

import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:window_size/window_size.dart';

import '../l10n/l10n.g.dart';
import '../routes.dart';
import 'platform.dart';

/// Base class of all example widgets.
///
/// This class provides basic structure, menu, and navigation.
abstract class Screen extends ConsumerWidget {
  final bool withForm;

  /// Constructor.
  const Screen({super.key, this.withForm = false});

  /// Gets a title of the page.
  String get title;

  Widget? buildAppBarLeading(BuildContext context, WidgetRef ref) => null;

  /// Builds page content.
  Widget buildPage(BuildContext context, WidgetRef ref);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final router = ref.watch(routerProvider);

    final appTitle = l10n.common.titleTemplate(screenName: title);

    if (isDesktop) {
      setWindowTitle(appTitle);
    }

    final leading = buildAppBarLeading(context, ref);

    return Scaffold(
      appBar: AppBar(
        title: Text(appTitle),
        leading: leading,
      ),
      drawer: leading != null
          ? null
          : SafeArea(
              child: Drawer(
                child: ListView(
                  children: [
                    ListTile(
                      title: Text(l10n.issues.title),
                      onTap: () => router.go(issuesRoute),
                    ),
                    ListTile(
                      title: Text(l10n.signIn.title),
                      onTap: () => router.go(loginRoute),
                    ),
                  ],
                ),
              ),
            ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(8),
          child: !withForm
              ? _Descendant(builder: buildPage)
              : FormBuilder(
                  child: _Descendant(
                    builder: buildPage,
                  ),
                ),
        ),
      ),
    );
  }
}

class _Descendant extends ConsumerWidget {
  final Widget Function(BuildContext, WidgetRef) builder;

  _Descendant({required this.builder});

  @override
  Widget build(BuildContext context, WidgetRef ref) => builder(context, ref);
}
