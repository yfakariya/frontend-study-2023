// See LICENCE file in the root.

import 'dart:async';

import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:form_builder_companion_presenter/form_builder_companion_annotation.dart';
import 'package:form_builder_companion_presenter/form_builder_companion_presenter.dart';
import 'package:go_router/go_router.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';
import 'package:url_launcher/url_launcher_string.dart';

import '../components/chip_panel.dart';
import '../components/screen.dart';
import '../l10n/l10n.g.dart';
import '../models/auth.dart';
import '../models/credential.dart';
import '../routes.dart';
import 'sign_in.fcp.dart';

part 'sign_in.g.dart';

class SignInPage extends Screen {
  const SignInPage({super.key}) : super(withForm: true);

  @override
  String get title => l10n.signIn.title;

  @override
  Widget buildPage(BuildContext context, WidgetRef ref) {
    final credential = ref.watch(oAuthCredentialRepositoryProvider);
    final auth = ref.watch(authTokenNotifierProvider);
    final state = ref.watch(signInPresenterProvider);
    final gitHubUrl =
        'https://docs.github.com/${Localizations.localeOf(context).languageCode}/developers/apps/building-oauth-apps/creating-an-oauth-app';
    return Column(
      children: [
        if (credential.hasError)
          ChipPanel.error(
            plainText: credential.error?.toString() ?? l10n.common.error,
          ),
        if (auth.hasError)
          ChipPanel.error(
            plainText: auth.error?.toString() ?? l10n.common.error,
          ),
        ChipPanel.info(
          richTexts: [
            l10n.signIn.credentialDescription(
              link: TextSpan(
                text: gitHubUrl,
                style: Theme.of(context).textTheme.titleMedium!.copyWith(
                      color: Colors.blue[700],
                      decoration: TextDecoration.underline,
                    ),
                recognizer: TapGestureRecognizer()
                  ..onTap = () => unawaited(launchUrlString(gitHubUrl)),
              ),
            ),
          ],
        ),
        // Forms
        state.fields.clientId(context),
        state.fields.clientSecret(context),
        state.fields.doPersist(
          context,
          title: Text(l10n.formFields.doPersist.label),
        ),
        Padding(
          padding: EdgeInsets.all(8),
          child: FilledButton(
            onPressed: state.submit(context),
            child: Text(l10n.signIn.signInButtonLabel),
          ),
        ),
      ],
    );
  }
}

@formCompanion
@riverpod
class SignInPresenter extends _$SignInPresenter
    with CompanionPresenterMixin, FormBuilderCompanionMixin {
  // Store notifiers to field to avoid ref usage beyond async boundary.
  late GoRouter _router;
  late AuthTokenNotifier _authenticator;
  late OAuthCredentialRepository _credentialRepository;

  SignInPresenter() {
    initializeCompanionMixin(
      PropertyDescriptorsBuilder()
        ..string(
          name: 'clientId',
        )
        ..string(
          name: 'clientSecret',
          valueTraits: PropertyValueTraits.sensitive,
        )
        ..booleanWithField<FormBuilderCheckbox>(
          name: 'doPersist',
        ),
    );
  }

  @override
  $SignInPresenterFormProperties build() {
    _authenticator = ref.read(authTokenNotifierProvider.notifier);
    _credentialRepository =
        ref.read(oAuthCredentialRepositoryProvider.notifier);
    _router = ref.watch(routerProvider);
    final loginState = ref.watch(oAuthCredentialRepositoryProvider);
    if (loginState.hasValue) {
      final loginData = loginState.requireValue;
      return resetProperties(
        (properties.copyWith()
              ..clientId(loginData.clientId)
              ..clientSecret(loginData.clientSecret))
            .build(),
      );
    }

    return properties;
  }

  @override
  FutureOr<void> doSubmit() async {
    final credential = OAuthCredential(
      clientId: properties.values.clientId,
      clientSecret: properties.values.clientSecret,
    );
    await _authenticator.authenticate(credential);

    // This causes authentication in authTokenNotifier.
    if (await _credentialRepository.store(
      credential,
      doPersist: properties.values.doPersist,
    )) {
      _router.go(homeRoute);
    }
  }
}
