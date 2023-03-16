// See LICENCE file in the root.

import 'dart:math';

import 'package:easy_localization/easy_localization.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:form_builder_companion_presenter/form_builder_companion_annotation.dart';
import 'package:form_builder_companion_presenter/form_builder_companion_presenter.dart';
import 'package:github/github.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

import '../components/chip_panel.dart';
import '../components/platform.dart';
import '../components/screen.dart';
import '../l10n/l10n.g.dart';
import '../models/auth.dart';
import '../models/issues.dart';
import '../routes.dart';
import 'issues.fcp.dart';

part 'issues.g.dart';

class IssuesPage extends Screen {
  const IssuesPage({super.key}) : super(withForm: true);

  @override
  String get title => l10n.issues.title;

  @override
  Widget buildPage(BuildContext context, WidgetRef ref) {
    final state = ref.watch(issuesPresenterProvider);

    return FormPropertiesRestorationScope(
      presenter: state.presenter,
      child: Column(
        children: [
          ExpansionTile(
            title: Text(
              l10n.issues.searchCondition,
              style: Theme.of(context).textTheme.titleMedium,
            ),
            childrenPadding: EdgeInsets.symmetric(horizontal: 16, vertical: 4),
            children: [
              // TODO(yfakariya): labels, milestone
              // TODO(yfakariya): searchable dropdown
              state.fields.repository(context),
              state.fields.issueState(context),
              // TODO(yfakaria): re-enable after nulalble support
              // state.fields.since(context),
              state.fields.sortKey(context),
              state.fields.direction(context),
              state.fields.issuesPerPages(context),
              Padding(
                padding: EdgeInsets.only(top: 8, bottom: 4),
                child: Align(
                  alignment:
                      isDesktop ? Alignment.centerRight : Alignment.center,
                  child: FilledButton(
                    onPressed: state.submit(context),
                    child: Text(l10n.issues.search),
                  ),
                ),
              ),
            ],
          ),
          // List
          Expanded(
            child: Padding(
              padding: EdgeInsets.only(left: 16),
              child: IssuesListPane(
                issuesPerPage: state.values.issuesPerPages,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

@formCompanion
@riverpod
class IssuesPresenter extends _$IssuesPresenter
    with CompanionPresenterMixin, FormBuilderCompanionMixin {
  IssuesPresenter() {
    initializeCompanionMixin(
      PropertyDescriptorsBuilder()
        ..string(name: 'repository')
        ..enumerated(
          name: 'sortKey',
          enumValues: IssueListSortKey.values,
          initialValue: IssueListSortKey.created,
        )
        ..enumerated(
          name: 'issueState',
          enumValues: IssueState.values,
          initialValue: IssueState.open,
        )
        ..enumerated(
          name: 'direction',
          enumValues: ListDirection.values,
          initialValue: ListDirection.desc,
        )
        // TODO(yfakaria): re-enable after nulalble support
        // ..dateTime(name: 'since')
        ..integerText(
          name: 'issuesPerPages',
          initialValue: 20,
        ),
    );
  }

  @override
  $IssuesPresenterFormProperties build() => properties;

  @override
  FutureOr<void> doSubmit() {
    ref.read(_issuesConditionProvider.notifier).state = IssuesSearchCondition(
      repository: properties.values.repository.isEmpty
          ? null
          : RepositorySlug.full(properties.values.repository),
      // milestoneNumber: ,
      // since: properties.values.since,
      sort: properties.values.sortKey,
      state: properties.values.issueState,
      direction: properties.values.direction,
      // labels: labels.value,
      issuesPerPage: properties.values.issuesPerPages,
    );
  }

  void Function()? nextPage(int page, int? itemsCount, int itemsPerPage) {
    if ((itemsCount ?? 0) < itemsPerPage) {
      return null;
    } else {
      return () {
        ref.read(_pageProvider.notifier).state = page + 1;
      };
    }
  }

  void Function()? previousPage(int page) {
    if (page == 1) {
      return null;
    } else {
      return () => ref.read(_pageProvider.notifier).state = page - 1;
    }
  }
}

class IssuesListPane extends ConsumerWidget {
  final int issuesPerPage;

  IssuesListPane({required this.issuesPerPage});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final presenter = ref.read(issuesPresenterProvider.notifier);
    final page = ref.watch(_pageProvider);
    final issues = ref.watch(_issuesProvider);
    final router = ref.watch(routerProvider);
    final items = issues.valueOrNull;

    return Column(
      children: [
        Align(
          alignment: isDesktop ? Alignment.centerLeft : Alignment.center,
          child: Text(
            l10n.issues.searchResult(count: items?.length ?? 0),
            style: Theme.of(context).textTheme.titleMedium,
          ),
        ),
        if (issues.hasError)
          ChipPanel.error(
            plainText: issues.error?.toString() ?? l10n.common.error,
          ),
        if (issues.isLoading) const Center(child: CircularProgressIndicator()),
        if (!issues.isLoading && (items?.isEmpty ?? true))
          // L10N
          Center(child: Text(l10n.issues.empty)),
        if (!issues.isLoading && (items?.isNotEmpty ?? false))
          Expanded(
            child: ListView.builder(
              padding: const EdgeInsets.all(8),
              itemCount: min(issuesPerPage, items!.length),
              itemBuilder: (context, index) {
                final item = items[index];
                final itemUri = Uri.parse(item.url);
                final owner = itemUri.pathSegments[1];
                final repository = itemUri.pathSegments[2];
                return Card(
                  child: ListTile(
                    title: Text(item.title),
                    subtitle: Text(
                      '#${item.number} at $owner/$repository, created: ${item.createdAt}, updated: ${item.updatedAt}',
                    ),
                    onTap: () {
                      router.push(
                        '$issuesRoute/$owner/$repository/${item.number}?title=${item.title}',
                      );
                    },
                  ),
                );
              },
            ),
          ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            // Previous
            TextButton(
              onPressed: presenter.previousPage(page),
              child: Text(l10n.issues.previous),
            ),
            Text(l10n.issues.page(page: page)),
            // Next
            TextButton(
              onPressed:
                  presenter.nextPage(page, issues.value?.length, issuesPerPage),
              child: Text(l10n.issues.next),
            ),
          ],
        ),
      ],
    );
  }
}

final _pageProvider = StateProvider<int>((_) => 1);
final _issuesConditionProvider =
    StateProvider<IssuesSearchCondition>((_) => const IssuesSearchCondition());

@riverpod
Future<List<Issue>> _issues(_IssuesRef ref) async {
  final authToken = await ref.watch(authTokenNotifierProvider.future);

  return authToken == null
      ? []
      : await getIssues(
          condition: ref.watch(_issuesConditionProvider),
          page: ref.watch(_pageProvider),
          token: authToken,
        );
}
