// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'issue.dart';

// **************************************************************************
// RiverpodGenerator
// **************************************************************************

String _$issueHash() => r'db0522fc0acd56a1054b858ccaee425fcc0ff207';

/// Copied from Dart SDK
class _SystemHash {
  _SystemHash._();

  static int combine(int hash, int value) {
    // ignore: parameter_assignments
    hash = 0x1fffffff & (hash + value);
    // ignore: parameter_assignments
    hash = 0x1fffffff & (hash + ((0x0007ffff & hash) << 10));
    return hash ^ (hash >> 6);
  }

  static int finish(int hash) {
    // ignore: parameter_assignments
    hash = 0x1fffffff & (hash + ((0x03ffffff & hash) << 3));
    // ignore: parameter_assignments
    hash = hash ^ (hash >> 11);
    return 0x1fffffff & (hash + ((0x00003fff & hash) << 15));
  }
}

typedef _IssueRef = AutoDisposeFutureProviderRef<Issue?>;

/// See also [_issue].
@ProviderFor(_issue)
const _issueProvider = _IssueFamily();

/// See also [_issue].
class _IssueFamily extends Family<AsyncValue<Issue?>> {
  /// See also [_issue].
  const _IssueFamily();

  /// See also [_issue].
  _IssueProvider call({
    required String owner,
    required String repository,
    required String issueNumber,
  }) {
    return _IssueProvider(
      owner: owner,
      repository: repository,
      issueNumber: issueNumber,
    );
  }

  @override
  _IssueProvider getProviderOverride(
    covariant _IssueProvider provider,
  ) {
    return call(
      owner: provider.owner,
      repository: provider.repository,
      issueNumber: provider.issueNumber,
    );
  }

  static const Iterable<ProviderOrFamily>? _dependencies = null;

  @override
  Iterable<ProviderOrFamily>? get dependencies => _dependencies;

  static const Iterable<ProviderOrFamily>? _allTransitiveDependencies = null;

  @override
  Iterable<ProviderOrFamily>? get allTransitiveDependencies =>
      _allTransitiveDependencies;

  @override
  String? get name => r'_issueProvider';
}

/// See also [_issue].
class _IssueProvider extends AutoDisposeFutureProvider<Issue?> {
  /// See also [_issue].
  _IssueProvider({
    required this.owner,
    required this.repository,
    required this.issueNumber,
  }) : super.internal(
          (ref) => _issue(
            ref,
            owner: owner,
            repository: repository,
            issueNumber: issueNumber,
          ),
          from: _issueProvider,
          name: r'_issueProvider',
          debugGetCreateSourceHash:
              const bool.fromEnvironment('dart.vm.product')
                  ? null
                  : _$issueHash,
          dependencies: _IssueFamily._dependencies,
          allTransitiveDependencies: _IssueFamily._allTransitiveDependencies,
        );

  final String owner;
  final String repository;
  final String issueNumber;

  @override
  bool operator ==(Object other) {
    return other is _IssueProvider &&
        other.owner == owner &&
        other.repository == repository &&
        other.issueNumber == issueNumber;
  }

  @override
  int get hashCode {
    var hash = _SystemHash.combine(0, runtimeType.hashCode);
    hash = _SystemHash.combine(hash, owner.hashCode);
    hash = _SystemHash.combine(hash, repository.hashCode);
    hash = _SystemHash.combine(hash, issueNumber.hashCode);

    return _SystemHash.finish(hash);
  }
}

String _$initializedWebViewHash() =>
    r'764cd29eeb49dd5b3a7f61c0ade8c4ddf554f65c';
typedef _InitializedWebViewRef = FutureProviderRef<WebView>;

/// See also [_initializedWebView].
@ProviderFor(_initializedWebView)
const _initializedWebViewProvider = _InitializedWebViewFamily();

/// See also [_initializedWebView].
class _InitializedWebViewFamily extends Family<AsyncValue<WebView>> {
  /// See also [_initializedWebView].
  const _InitializedWebViewFamily();

  /// See also [_initializedWebView].
  _InitializedWebViewProvider call(
    ThemeData theme,
  ) {
    return _InitializedWebViewProvider(
      theme,
    );
  }

  @override
  _InitializedWebViewProvider getProviderOverride(
    covariant _InitializedWebViewProvider provider,
  ) {
    return call(
      provider.theme,
    );
  }

  static const Iterable<ProviderOrFamily>? _dependencies = null;

  @override
  Iterable<ProviderOrFamily>? get dependencies => _dependencies;

  static const Iterable<ProviderOrFamily>? _allTransitiveDependencies = null;

  @override
  Iterable<ProviderOrFamily>? get allTransitiveDependencies =>
      _allTransitiveDependencies;

  @override
  String? get name => r'_initializedWebViewProvider';
}

/// See also [_initializedWebView].
class _InitializedWebViewProvider extends FutureProvider<WebView> {
  /// See also [_initializedWebView].
  _InitializedWebViewProvider(
    this.theme,
  ) : super.internal(
          (ref) => _initializedWebView(
            ref,
            theme,
          ),
          from: _initializedWebViewProvider,
          name: r'_initializedWebViewProvider',
          debugGetCreateSourceHash:
              const bool.fromEnvironment('dart.vm.product')
                  ? null
                  : _$initializedWebViewHash,
          dependencies: _InitializedWebViewFamily._dependencies,
          allTransitiveDependencies:
              _InitializedWebViewFamily._allTransitiveDependencies,
        );

  final ThemeData theme;

  @override
  bool operator ==(Object other) {
    return other is _InitializedWebViewProvider && other.theme == theme;
  }

  @override
  int get hashCode {
    var hash = _SystemHash.combine(0, runtimeType.hashCode);
    hash = _SystemHash.combine(hash, theme.hashCode);

    return _SystemHash.finish(hash);
  }
}

String _$loadedWebViewHash() => r'ef2873a714b5e824a38b4d1608065a2f70127261';
typedef _LoadedWebViewRef = AutoDisposeFutureProviderRef<WebView>;

/// See also [_loadedWebView].
@ProviderFor(_loadedWebView)
const _loadedWebViewProvider = _LoadedWebViewFamily();

/// See also [_loadedWebView].
class _LoadedWebViewFamily extends Family<AsyncValue<WebView>> {
  /// See also [_loadedWebView].
  const _LoadedWebViewFamily();

  /// See also [_loadedWebView].
  _LoadedWebViewProvider call({
    required ThemeData theme,
    required String owner,
    required String repository,
    required String issueNumber,
  }) {
    return _LoadedWebViewProvider(
      theme: theme,
      owner: owner,
      repository: repository,
      issueNumber: issueNumber,
    );
  }

  @override
  _LoadedWebViewProvider getProviderOverride(
    covariant _LoadedWebViewProvider provider,
  ) {
    return call(
      theme: provider.theme,
      owner: provider.owner,
      repository: provider.repository,
      issueNumber: provider.issueNumber,
    );
  }

  static const Iterable<ProviderOrFamily>? _dependencies = null;

  @override
  Iterable<ProviderOrFamily>? get dependencies => _dependencies;

  static const Iterable<ProviderOrFamily>? _allTransitiveDependencies = null;

  @override
  Iterable<ProviderOrFamily>? get allTransitiveDependencies =>
      _allTransitiveDependencies;

  @override
  String? get name => r'_loadedWebViewProvider';
}

/// See also [_loadedWebView].
class _LoadedWebViewProvider extends AutoDisposeFutureProvider<WebView> {
  /// See also [_loadedWebView].
  _LoadedWebViewProvider({
    required this.theme,
    required this.owner,
    required this.repository,
    required this.issueNumber,
  }) : super.internal(
          (ref) => _loadedWebView(
            ref,
            theme: theme,
            owner: owner,
            repository: repository,
            issueNumber: issueNumber,
          ),
          from: _loadedWebViewProvider,
          name: r'_loadedWebViewProvider',
          debugGetCreateSourceHash:
              const bool.fromEnvironment('dart.vm.product')
                  ? null
                  : _$loadedWebViewHash,
          dependencies: _LoadedWebViewFamily._dependencies,
          allTransitiveDependencies:
              _LoadedWebViewFamily._allTransitiveDependencies,
        );

  final ThemeData theme;
  final String owner;
  final String repository;
  final String issueNumber;

  @override
  bool operator ==(Object other) {
    return other is _LoadedWebViewProvider &&
        other.theme == theme &&
        other.owner == owner &&
        other.repository == repository &&
        other.issueNumber == issueNumber;
  }

  @override
  int get hashCode {
    var hash = _SystemHash.combine(0, runtimeType.hashCode);
    hash = _SystemHash.combine(hash, theme.hashCode);
    hash = _SystemHash.combine(hash, owner.hashCode);
    hash = _SystemHash.combine(hash, repository.hashCode);
    hash = _SystemHash.combine(hash, issueNumber.hashCode);

    return _SystemHash.finish(hash);
  }
}
// ignore_for_file: unnecessary_raw_strings, subtype_of_sealed_class, invalid_use_of_internal_member, do_not_use_environment, prefer_const_constructors, public_member_api_docs, avoid_private_typedef_functions
