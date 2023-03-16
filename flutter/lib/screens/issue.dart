// See LICENCE file in the root.

import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:github/github.dart';
import 'package:intl/intl.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';
import 'package:url_launcher/url_launcher_string.dart';

import '../components/chip_panel.dart';
import '../components/screen.dart';
import '../components/web_view.dart';
import '../l10n/l10n.g.dart';
import '../models/auth.dart';
import '../models/issues.dart';
import '../routes.dart';

part 'issue.g.dart';

class IssuePage extends Screen {
  final String owner;
  final String repository;
  final String issueNumber;
  final String? issueTitle;
  const IssuePage({
    super.key,
    required this.owner,
    required this.repository,
    required this.issueNumber,
    this.issueTitle,
  });

  @override
  String get title => l10n.issue.title(
        issueNumber: issueNumber,
        issueTitle: issueTitle ?? '',
      );

  @override
  Widget? buildAppBarLeading(BuildContext context, WidgetRef ref) {
    final router = ref.watch(routerProvider);
    return IconButton(
      icon: const Icon(Icons.arrow_back),
      onPressed: () {
        if (router.canPop()) {
          router.pop();
        } else {
          router.go(issuesRoute);
        }
      },
    );
  }

  @override
  Widget buildPage(BuildContext context, WidgetRef ref) {
    final issueState = ref.watch(
      _issueProvider(
        owner: owner,
        repository: repository,
        issueNumber: issueNumber,
      ),
    );
    final issue = issueState.valueOrNull;
    final loadedWebView = ref.watch(
      _loadedWebViewProvider(
        theme: Theme.of(context),
        owner: owner,
        repository: repository,
        issueNumber: issueNumber,
      ),
    );

    return Column(
      children: [
        if (issueState.hasError)
          ChipPanel.error(
            plainText: issueState.error?.toString() ?? l10n.common.error,
          ),
        if (loadedWebView.hasError)
          ChipPanel.error(
            plainText: loadedWebView.error?.toString() ?? l10n.common.error,
          ),
        if (issueState.isLoading)
          const Center(child: CircularProgressIndicator()),
        if (!issueState.isLoading && issue != null) _issueView(context, issue),
        if (!issueState.isLoading && loadedWebView.isLoading)
          const Center(child: CircularProgressIndicator()),
        if (!loadedWebView.isLoading && loadedWebView.hasValue)
          Expanded(
            // Use container becuase DecoratedBox does not work here.
            // ignore: use_decorated_box
            child: Container(
              decoration: BoxDecoration(
                border: Border(
                  top: BorderSide(
                    color: Theme.of(context).colorScheme.onBackground,
                  ),
                ),
              ),
              child: loadedWebView.requireValue.build(context),
            ),
          ),
      ],
    );
  }

  Widget _issueView(BuildContext context, Issue issue) {
    final user = issue.user;
    final created = issue.createdAt;
    final locale = Localizations.localeOf(context).toLanguageTag();
    return Padding(
      padding: EdgeInsets.only(bottom: 8),
      child: Row(
        children: [
          if (user?.avatarUrl != null)
            MouseRegion(
              cursor: SystemMouseCursors.click,
              child: GestureDetector(
                onTap: () {
                  unawaited(
                    showDialog(
                      context: context,
                      builder: (context) => Dialog(
                        child: Image.network(user.avatarUrl!),
                      ),
                    ),
                  );
                },
                child: ClipOval(
                  child: Image.network(
                    user!.avatarUrl!,
                    height: 40,
                    width: 40,
                  ),
                ),
              ),
            ),
          const SizedBox(width: 8),
          user?.htmlUrl != null
              ? MouseRegion(
                  cursor: SystemMouseCursors.click,
                  child: GestureDetector(
                    onTap: () {
                      unawaited(launchUrlString(user!.htmlUrl!));
                    },
                    child: Text(
                      user?.name ?? user?.login ?? '',
                      style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                            decoration: TextDecoration.underline,
                          ),
                    ),
                  ),
                )
              : Text(user?.name ?? user?.login ?? ''),
          const SizedBox(width: 8),
          Text(
            created == null
                ? ''
                : DateFormat.yMd(locale).add_Hm().format(created),
          ),
          const SizedBox(width: 8),
          Container(
            padding: EdgeInsets.symmetric(horizontal: 4),
            decoration: BoxDecoration(
              borderRadius: BorderRadius.all(Radius.circular(4)),
              color: issue.state == 'open' ? Colors.green : Colors.purple,
            ),
            child: Text(
              issue.state,
              style: Theme.of(context).textTheme.labelLarge,
            ),
          ),
          const SizedBox(width: 8),
          Text('$owner/$repository'),
        ],
      ),
    );
  }
}

@riverpod
FutureOr<Issue?> _issue(
  _IssueRef ref, {
  required String owner,
  required String repository,
  required String issueNumber,
}) async {
  final authToken = await ref.watch(authTokenNotifierProvider.future);
  return authToken == null
      ? null
      : await getIssue(
          token: authToken,
          owner: owner,
          repository: repository,
          issueNumber: issueNumber,
        );
}

@Riverpod(keepAlive: true)
FutureOr<WebView> _initializedWebView(
  _InitializedWebViewRef _,
  ThemeData theme,
) =>
    WebView.create(theme);

@riverpod
FutureOr<WebView> _loadedWebView(
  _LoadedWebViewRef ref, {
  required ThemeData theme,
  required String owner,
  required String repository,
  required String issueNumber,
}) async {
  final webView = await ref.watch(_initializedWebViewProvider(theme).future);
  final authToken = await ref.watch(authTokenNotifierProvider.future);
  final issue = await ref.watch(
    _issueProvider(
      owner: owner,
      repository: repository,
      issueNumber: issueNumber,
    ).future,
  );

  final html = issue == null
      ? ''
      : await getIssueBodyHtml(
          token: authToken!,
          owner: owner,
          repository: repository,
          body: issue.body,
        );

  await webView.loadHtml(html);
  return webView;
}
