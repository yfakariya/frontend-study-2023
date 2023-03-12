// See LICENCE file in the root.

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

import '../models/auth.dart';
import '../routes.dart';

class HomePage extends ConsumerWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final auth = ref.watch(authTokenNotifierProvider);
    final router = ref.watch(routerProvider);
    if (auth.hasValue) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        router.go(issuesRoute);
      });
    }

    return const Center(
      child: const CircularProgressIndicator(),
    );
  }
}
