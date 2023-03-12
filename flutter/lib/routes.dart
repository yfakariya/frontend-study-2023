// See LICENCE file in the root.

import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

import 'models/auth.dart';
import 'screens/home.dart';
import 'screens/issue.dart';
import 'screens/issues.dart';
import 'screens/sign_in.dart';

part 'routes.g.dart';

const homeRoute = '/';
const _issues = 'issues';
const issuesRoute = '/$_issues';
const _login = 'login';
const loginRoute = '/$_login';

@riverpod
GoRouter router(RouterRef ref) {
  String? guardRoute(
    BuildContext context,
    GoRouterState state,
  ) {
    final authToken = ref.watch(authTokenNotifierProvider);
    // Go to login page if the user is not authenticated yet.
    if (!authToken.hasValue) {
      return loginRoute;
    }

    // Call `builder` otherwise.
    return null;
  }

  return GoRouter(
    restorationScopeId: 'route',
    routes: [
      GoRoute(
        path: homeRoute,
        name: 'home',
        redirect: guardRoute,
        builder: (context, state) => const HomePage(),
        routes: [
          GoRoute(
            path: _issues,
            name: 'issues',
            redirect: guardRoute,
            pageBuilder: (context, state) =>
                const MaterialPage<void>(child: IssuesPage()),
            routes: [
              GoRoute(
                path: ':owner/:repository/:issueNumber',
                redirect: guardRoute,
                pageBuilder: (context, state) {
                  debugPrint(
                    'Router: ${state.params['owner']}/'
                    '${state.params['repository']}/'
                    '${state.params['issueNumber']}?'
                    '${state.queryParametersAll.entries.expand((e) => e.value.map((x) => MapEntry(e.key, x))).map((e) => '${e.key}=${e.value}').join('&')}',
                  );
                  return MaterialPage<void>(
                    child: IssuePage(
                      key: state.pageKey,
                      owner: state.params['owner']!,
                      repository: state.params['repository']!,
                      issueNumber: state.params['issueNumber']!,
                      issueTitle: state.queryParams['title'],
                    ),
                  );
                },
              ),
            ],
          ),
          // TODO(yfaakariya): signout
          GoRoute(
            path: _login,
            name: 'login',
            pageBuilder: (context, state) =>
                const MaterialPage<void>(child: SignInPage()),
          ),
        ],
      ),
    ],
  );
}
