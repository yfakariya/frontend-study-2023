// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'credential.dart';

// **************************************************************************
// RiverpodGenerator
// **************************************************************************

String _$oAuthCredentialRepositoryHash() =>
    r'5d3d0f13cf270303fa177eee37370f4bf8f38cc8';

/// Repository to restore/store credentials
///
/// Copied from [OAuthCredentialRepository].
@ProviderFor(OAuthCredentialRepository)
final oAuthCredentialRepositoryProvider = AutoDisposeAsyncNotifierProvider<
    OAuthCredentialRepository, OAuthCredential>.internal(
  OAuthCredentialRepository.new,
  name: r'oAuthCredentialRepositoryProvider',
  debugGetCreateSourceHash: const bool.fromEnvironment('dart.vm.product')
      ? null
      : _$oAuthCredentialRepositoryHash,
  dependencies: null,
  allTransitiveDependencies: null,
);

typedef _$OAuthCredentialRepository = AutoDisposeAsyncNotifier<OAuthCredential>;
// ignore_for_file: unnecessary_raw_strings, subtype_of_sealed_class, invalid_use_of_internal_member, do_not_use_environment, prefer_const_constructors, public_member_api_docs, avoid_private_typedef_functions
