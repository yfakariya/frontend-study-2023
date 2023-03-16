// See LICENCE file in the root.

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:oauth2_client/github_oauth2_client.dart';
import 'package:oauth2_client/interfaces.dart';
import 'package:oauth2_client/oauth2_helper.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

import '../components/platform.dart';
import 'credential.dart';

part 'auth.freezed.dart';
part 'auth.g.dart';

// TODO: Use OAuth2 client to support windows seemlessly.
final _customUriScheme =
    isDesktop ? 'http://127.0.0.1:3000' : 'github.viewer.app';
final _redirectUri = isDesktop
    ? '$_customUriScheme/github.viewer.app/oauth2redirect'
    : '$_customUriScheme:/oauth2redirect';

/// Tokens of OAuth.
@freezed
class AuthTokens with _$AuthTokens {
  factory AuthTokens({
    required String accessToken,
  }) = _AuthTokens;
}

// copied from FlutterSecureStorageWindowsPlugin::SanitizeDirString of flutter_secure_strorage
final _specialCharsSanitaization = RegExp('[<>:"/\\\\|?*]');
final _trimEndSanitaization = RegExp('[.]+\$');

String sanitizeDirString(String string) => string
    .replaceAll(_specialCharsSanitaization, '_')
    .trim()
    .replaceAll(_trimEndSanitaization, '');

// Use StreamProvider if dependent data can be stored if and only if the user
// input previously.

/// Notifies [AuthTokens] change.
@riverpod
class AuthTokenNotifier extends _$AuthTokenNotifier {
  @override
  Stream<AuthTokens?> build() async* {
    yield null;
    final credential =
        await ref.watch(oAuthCredentialRepositoryProvider.future);
    yield await _authenticate(credential);
  }

  Future<AuthTokens?> authenticate(OAuthCredential credential) =>
      update((_) => _authenticate(credential));

  Future<AuthTokens?> _authenticate(OAuthCredential credential) async {
    final client = GitHubOAuth2Client(
      customUriScheme: _customUriScheme,
      redirectUri: _redirectUri,
    );

    final helper = OAuth2Helper(
      client,
      clientId: credential.clientId,
      clientSecret: credential.clientSecret,
      // Avoid oauth2_client bug: *TODO*:issue
      scopes: ['public_repo'],
      // Avoid flutter_secure_storage bug: *TODO*:issue
      tokenStorage: TokenStorage(sanitizeDirString(client.tokenUrl)),
    );
    final token = await helper.getToken();

    if (!token!.isValid()) {
      throw Exception('Failed to login. $token');
    }

    final expiresInSeconds = token.expiresIn;
    if (expiresInSeconds != null) {
      Future.delayed(
        Duration(seconds: expiresInSeconds),
        () => ref.invalidateSelf(),
      );
    }

    return AuthTokens(accessToken: token.accessToken!);
  }
}
