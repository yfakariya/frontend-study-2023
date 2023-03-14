/// Generated file. Do not edit.
///
/// Locales: 2
/// Strings: 82 (41 per locale)
///
/// Built on 2023-03-14 at 13:48 UTC

// coverage:ignore-file
// ignore_for_file: type=lint

import 'package:flutter/widgets.dart';
import 'package:slang/builder/model/node.dart';
import 'package:slang_flutter/slang_flutter.dart';
export 'package:slang_flutter/slang_flutter.dart';

const AppLocale _baseLocale = AppLocale.en;

/// Supported locales, see extension methods below.
///
/// Usage:
/// - LocaleSettings.setLocale(AppLocale.en) // set locale
/// - Locale locale = AppLocale.en.flutterLocale // get flutter locale from enum
/// - if (LocaleSettings.currentLocale == AppLocale.en) // locale check
enum AppLocale with BaseAppLocale<AppLocale, _L10nEn> {
	en(languageCode: 'en', build: _L10nEn.build),
	ja(languageCode: 'ja', build: _L10nJa.build);

	const AppLocale({required this.languageCode, this.scriptCode, this.countryCode, required this.build}); // ignore: unused_element

	@override final String languageCode;
	@override final String? scriptCode;
	@override final String? countryCode;
	@override final TranslationBuilder<AppLocale, _L10nEn> build;

	/// Gets current instance managed by [LocaleSettings].
	_L10nEn get translations => LocaleSettings.instance.translationMap[this]!;
}

/// Method A: Simple
///
/// No rebuild after locale change.
/// Translation happens during initialization of the widget (call of l10n).
/// Configurable via 'translate_var'.
///
/// Usage:
/// String a = l10n.someKey.anotherKey;
/// String b = l10n['someKey.anotherKey']; // Only for edge cases!
_L10nEn get l10n => LocaleSettings.instance.currentTranslations;

/// Method B: Advanced
///
/// All widgets using this method will trigger a rebuild when locale changes.
/// Use this if you have e.g. a settings page where the user can select the locale during runtime.
///
/// Step 1:
/// wrap your App with
/// TranslationProvider(
/// 	child: MyApp()
/// );
///
/// Step 2:
/// final l10n = Translations.of(context); // Get l10n variable.
/// String a = l10n.someKey.anotherKey; // Use l10n variable.
/// String b = l10n['someKey.anotherKey']; // Only for edge cases!
class Translations {
	Translations._(); // no constructor

	static _L10nEn of(BuildContext context) => InheritedLocaleData.of<AppLocale, _L10nEn>(context).translations;
}

/// The provider for method B
class TranslationProvider extends BaseTranslationProvider<AppLocale, _L10nEn> {
	TranslationProvider({required super.child}) : super(settings: LocaleSettings.instance);

	static InheritedLocaleData<AppLocale, _L10nEn> of(BuildContext context) => InheritedLocaleData.of<AppLocale, _L10nEn>(context);
}

/// Method B shorthand via [BuildContext] extension method.
/// Configurable via 'translate_var'.
///
/// Usage (e.g. in a widget's build method):
/// context.l10n.someKey.anotherKey
extension BuildContextTranslationsExtension on BuildContext {
	_L10nEn get l10n => TranslationProvider.of(this).translations;
}

/// Manages all translation instances and the current locale
class LocaleSettings extends BaseFlutterLocaleSettings<AppLocale, _L10nEn> {
	LocaleSettings._() : super(utils: AppLocaleUtils.instance);

	static final instance = LocaleSettings._();

	// static aliases (checkout base methods for documentation)
	static AppLocale get currentLocale => instance.currentLocale;
	static Stream<AppLocale> getLocaleStream() => instance.getLocaleStream();
	static AppLocale setLocale(AppLocale locale, {bool? listenToDeviceLocale = false}) => instance.setLocale(locale, listenToDeviceLocale: listenToDeviceLocale);
	static AppLocale setLocaleRaw(String rawLocale, {bool? listenToDeviceLocale = false}) => instance.setLocaleRaw(rawLocale, listenToDeviceLocale: listenToDeviceLocale);
	static AppLocale useDeviceLocale() => instance.useDeviceLocale();
	@Deprecated('Use [AppLocaleUtils.supportedLocales]') static List<Locale> get supportedLocales => instance.supportedLocales;
	@Deprecated('Use [AppLocaleUtils.supportedLocalesRaw]') static List<String> get supportedLocalesRaw => instance.supportedLocalesRaw;
	static void setPluralResolver({String? language, AppLocale? locale, PluralResolver? cardinalResolver, PluralResolver? ordinalResolver}) => instance.setPluralResolver(
		language: language,
		locale: locale,
		cardinalResolver: cardinalResolver,
		ordinalResolver: ordinalResolver,
	);
}

/// Provides utility functions without any side effects.
class AppLocaleUtils extends BaseAppLocaleUtils<AppLocale, _L10nEn> {
	AppLocaleUtils._() : super(baseLocale: _baseLocale, locales: AppLocale.values);

	static final instance = AppLocaleUtils._();

	// static aliases (checkout base methods for documentation)
	static AppLocale parse(String rawLocale) => instance.parse(rawLocale);
	static AppLocale parseLocaleParts({required String languageCode, String? scriptCode, String? countryCode}) => instance.parseLocaleParts(languageCode: languageCode, scriptCode: scriptCode, countryCode: countryCode);
	static AppLocale findDeviceLocale() => instance.findDeviceLocale();
	static List<Locale> get supportedLocales => instance.supportedLocales;
	static List<String> get supportedLocalesRaw => instance.supportedLocalesRaw;
}

// translations

// Path: <root>
class _L10nEn implements BaseTranslations<AppLocale, _L10nEn> {

	/// You can call this constructor and build your own translation instance of this locale.
	/// Constructing via the enum [AppLocale.build] is preferred.
	_L10nEn.build({Map<String, Node>? overrides, PluralResolver? cardinalResolver, PluralResolver? ordinalResolver})
		: assert(overrides == null, 'Set "translation_overrides: true" in order to enable this feature.'),
		  $meta = TranslationMetadata(
		    locale: AppLocale.en,
		    overrides: overrides ?? {},
		    cardinalResolver: cardinalResolver,
		    ordinalResolver: ordinalResolver,
		  ) {
		$meta.setFlatMapFunction(_flatMapFunction);
	}

	/// Metadata for the translations of <en>.
	@override final TranslationMetadata<AppLocale, _L10nEn> $meta;

	/// Access flat map
	dynamic operator[](String key) => $meta.getTranslation(key);

	late final _L10nEn _root = this; // ignore: unused_field

	// Translations
	late final _L10nCommonEn common = _L10nCommonEn._(_root);
	late final _L10nFormFieldsEn formFields = _L10nFormFieldsEn._(_root);
	late final _L10nHomeEn home = _L10nHomeEn._(_root);
	late final _L10nIssueEn issue = _L10nIssueEn._(_root);
	late final _L10nIssuesEn issues = _L10nIssuesEn._(_root);
	late final _L10nSignInEn signIn = _L10nSignInEn._(_root);
}

// Path: common
class _L10nCommonEn {
	_L10nCommonEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String titleTemplate({required Object screenName}) => '${screenName} - GitHub Viewer';
	String get error => 'An unexpected error is occurred. Try again.';
}

// Path: formFields
class _L10nFormFieldsEn {
	_L10nFormFieldsEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	late final _L10nFormFieldsClientIdEn clientId = _L10nFormFieldsClientIdEn._(_root);
	late final _L10nFormFieldsClientSecretEn clientSecret = _L10nFormFieldsClientSecretEn._(_root);
	late final _L10nFormFieldsDoPersistEn doPersist = _L10nFormFieldsDoPersistEn._(_root);
	late final _L10nFormFieldsRepositoryEn repository = _L10nFormFieldsRepositoryEn._(_root);
	late final _L10nFormFieldsSortKeyEn sortKey = _L10nFormFieldsSortKeyEn._(_root);
	late final _L10nFormFieldsDirectionEn direction = _L10nFormFieldsDirectionEn._(_root);
	late final _L10nFormFieldsIssueStateEn issueState = _L10nFormFieldsIssueStateEn._(_root);
	late final _L10nFormFieldsSinceEn since = _L10nFormFieldsSinceEn._(_root);
	late final _L10nFormFieldsIssuesPerPagesEn issuesPerPages = _L10nFormFieldsIssuesPerPagesEn._(_root);
}

// Path: home
class _L10nHomeEn {
	_L10nHomeEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get title => 'Home';
}

// Path: issue
class _L10nIssueEn {
	_L10nIssueEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String title({required Object issueNumber, required Object issueTitle}) => 'Issue #${issueNumber}: ${issueTitle}';
}

// Path: issues
class _L10nIssuesEn {
	_L10nIssuesEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get title => 'Issues';
	String get search => 'Search';
	String page({required Object page}) => 'Page ${page}';
	String get next => 'Next';
	String get previous => 'Previous';
	String get searchCondition => 'Search Condition';
	String searchResult({required Object count}) => 'Search Result (${count} items)';
	String get empty => 'There are no issues found.';
}

// Path: signIn
class _L10nSignInEn {
	_L10nSignInEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get title => 'Sign in';
	String get signInButtonLabel => 'Sign in';
	TextSpan credentialDescription({required InlineSpan link}) => TextSpan(children: [
		const TextSpan(text: 'Input GitHub client ID and client secret which you registered. See '),
		link,
		const TextSpan(text: ' for details.'),
	]);
}

// Path: formFields.clientId
class _L10nFormFieldsClientIdEn {
	_L10nFormFieldsClientIdEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Client ID';
	String get hint => 'Client ID configured in your GitHub account setting for this app.';
}

// Path: formFields.clientSecret
class _L10nFormFieldsClientSecretEn {
	_L10nFormFieldsClientSecretEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Client Secret';
	String get hint => 'Client Secret configured in your GitHub account setting for this app.';
}

// Path: formFields.doPersist
class _L10nFormFieldsDoPersistEn {
	_L10nFormFieldsDoPersistEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Persists these values.';
	String get hint => 'Check it if you want to persist these inputs.';
}

// Path: formFields.repository
class _L10nFormFieldsRepositoryEn {
	_L10nFormFieldsRepositoryEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Repository (optional)';
	String get hint => 'Target repository name with \'user/repo\' format to be searched.';
}

// Path: formFields.sortKey
class _L10nFormFieldsSortKeyEn {
	_L10nFormFieldsSortKeyEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Sort key';
	String get hint => 'Select sort key of issues. Default is \'Created date/time\'.';
	String get created => 'Created date/time';
	String get updated => 'Updated date/time';
	String get comments => 'Commented date/time';
}

// Path: formFields.direction
class _L10nFormFieldsDirectionEn {
	_L10nFormFieldsDirectionEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'List direction';
	String get hint => 'Select direction of issues list which ordered by sort key. Default is \'Descsendant\'.';
	String get asc => 'Ascendant';
	String get desc => 'Descendant';
}

// Path: formFields.issueState
class _L10nFormFieldsIssueStateEn {
	_L10nFormFieldsIssueStateEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Issue state';
	String get hint => 'Select issue state to be shown. Default is \'Open\'.';
	String get open => 'Opening';
	String get closed => 'Closed';
	String get all => 'All';
}

// Path: formFields.since
class _L10nFormFieldsSinceEn {
	_L10nFormFieldsSinceEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Since (optional)';
	String get hint => 'Specify oldest date and time of issues to be shown.';
}

// Path: formFields.issuesPerPages
class _L10nFormFieldsIssuesPerPagesEn {
	_L10nFormFieldsIssuesPerPagesEn._(this._root);

	final _L10nEn _root; // ignore: unused_field

	// Translations
	String get label => 'Issues per pages';
	String get hint => 'Maximum issues shown in a page. Default is 20.';
}

// Path: <root>
class _L10nJa implements _L10nEn {

	/// You can call this constructor and build your own translation instance of this locale.
	/// Constructing via the enum [AppLocale.build] is preferred.
	_L10nJa.build({Map<String, Node>? overrides, PluralResolver? cardinalResolver, PluralResolver? ordinalResolver})
		: assert(overrides == null, 'Set "translation_overrides: true" in order to enable this feature.'),
		  $meta = TranslationMetadata(
		    locale: AppLocale.ja,
		    overrides: overrides ?? {},
		    cardinalResolver: cardinalResolver,
		    ordinalResolver: ordinalResolver,
		  ) {
		$meta.setFlatMapFunction(_flatMapFunction);
	}

	/// Metadata for the translations of <ja>.
	@override final TranslationMetadata<AppLocale, _L10nEn> $meta;

	/// Access flat map
	@override dynamic operator[](String key) => $meta.getTranslation(key);

	@override late final _L10nJa _root = this; // ignore: unused_field

	// Translations
	@override late final _L10nCommonJa common = _L10nCommonJa._(_root);
	@override late final _L10nFormFieldsJa formFields = _L10nFormFieldsJa._(_root);
	@override late final _L10nHomeJa home = _L10nHomeJa._(_root);
	@override late final _L10nIssuesJa issues = _L10nIssuesJa._(_root);
	@override late final _L10nIssueJa issue = _L10nIssueJa._(_root);
	@override late final _L10nSignInJa signIn = _L10nSignInJa._(_root);
}

// Path: common
class _L10nCommonJa implements _L10nCommonEn {
	_L10nCommonJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String titleTemplate({required Object screenName}) => '${screenName} - GitHub ビューアー';
	@override String get error => '予想外の問題が発生しました。もう一度やり直してみてください。';
}

// Path: formFields
class _L10nFormFieldsJa implements _L10nFormFieldsEn {
	_L10nFormFieldsJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override late final _L10nFormFieldsClientIdJa clientId = _L10nFormFieldsClientIdJa._(_root);
	@override late final _L10nFormFieldsClientSecretJa clientSecret = _L10nFormFieldsClientSecretJa._(_root);
	@override late final _L10nFormFieldsDoPersistJa doPersist = _L10nFormFieldsDoPersistJa._(_root);
	@override late final _L10nFormFieldsRepositoryJa repository = _L10nFormFieldsRepositoryJa._(_root);
	@override late final _L10nFormFieldsSortKeyJa sortKey = _L10nFormFieldsSortKeyJa._(_root);
	@override late final _L10nFormFieldsDirectionJa direction = _L10nFormFieldsDirectionJa._(_root);
	@override late final _L10nFormFieldsIssueStateJa issueState = _L10nFormFieldsIssueStateJa._(_root);
	@override late final _L10nFormFieldsSinceJa since = _L10nFormFieldsSinceJa._(_root);
	@override late final _L10nFormFieldsIssuesPerPagesJa issuesPerPages = _L10nFormFieldsIssuesPerPagesJa._(_root);
}

// Path: home
class _L10nHomeJa implements _L10nHomeEn {
	_L10nHomeJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get title => 'ホーム';
}

// Path: issues
class _L10nIssuesJa implements _L10nIssuesEn {
	_L10nIssuesJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get title => '課題一覧';
	@override String get search => '検索';
	@override String page({required Object page}) => '${page} ページ';
	@override String get next => '次へ';
	@override String get previous => '前へ';
	@override String get searchCondition => '検索条件';
	@override String searchResult({required Object count}) => '検索結果（${count} 件）';
	@override String get empty => '一致する課題がありません。';
}

// Path: issue
class _L10nIssueJa implements _L10nIssueEn {
	_L10nIssueJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String title({required Object issueNumber, required Object issueTitle}) => '課題 #${issueNumber}: ${issueTitle}';
}

// Path: signIn
class _L10nSignInJa implements _L10nSignInEn {
	_L10nSignInJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get title => 'サインイン';
	@override String get signInButtonLabel => 'サインイン';
	@override TextSpan credentialDescription({required InlineSpan link}) => TextSpan(children: [
		const TextSpan(text: 'GitHubに登録した、クライアントIDとクライアントシークレットを入力してください。詳細については、'),
		link,
		const TextSpan(text: ' を参照してください。'),
	]);
}

// Path: formFields.clientId
class _L10nFormFieldsClientIdJa implements _L10nFormFieldsClientIdEn {
	_L10nFormFieldsClientIdJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => 'クライアントID';
	@override String get hint => 'このアプリ用にGitHubのアカウント設定で構成したクライアントID。';
}

// Path: formFields.clientSecret
class _L10nFormFieldsClientSecretJa implements _L10nFormFieldsClientSecretEn {
	_L10nFormFieldsClientSecretJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => 'クライアントシークレット';
	@override String get hint => 'このアプリ用にGitHubのアカウント設定で構成したクライアントシークレット。';
}

// Path: formFields.doPersist
class _L10nFormFieldsDoPersistJa implements _L10nFormFieldsDoPersistEn {
	_L10nFormFieldsDoPersistJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => 'これらの値を保存する';
	@override String get hint => 'これらの入力を保存したい場合にはチェックしてください。';
}

// Path: formFields.repository
class _L10nFormFieldsRepositoryJa implements _L10nFormFieldsRepositoryEn {
	_L10nFormFieldsRepositoryJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => 'リポジトリ（省略可能）';
	@override String get hint => '検索対象のリポジトリの名前。\'user/repo\'形式です。';
}

// Path: formFields.sortKey
class _L10nFormFieldsSortKeyJa implements _L10nFormFieldsSortKeyEn {
	_L10nFormFieldsSortKeyJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => '並び替えのキー';
	@override String get hint => '課題の並び替えのキーを選択してください。既定値は\'作成日時\'です。';
	@override String get created => '作成日時';
	@override String get updated => '更新日時';
	@override String get comments => 'コメント日時';
}

// Path: formFields.direction
class _L10nFormFieldsDirectionJa implements _L10nFormFieldsDirectionEn {
	_L10nFormFieldsDirectionJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => '一覧の並び順';
	@override String get hint => '「並び替えのキー」で並び替えられる課題の並び順を選択してください。既定値は\'降順\'です。';
	@override String get asc => '昇順';
	@override String get desc => '降順';
}

// Path: formFields.issueState
class _L10nFormFieldsIssueStateJa implements _L10nFormFieldsIssueStateEn {
	_L10nFormFieldsIssueStateJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => '課題の状態';
	@override String get hint => '表示する課題の状態。既定値は\'全て\'です。';
	@override String get open => 'オープン';
	@override String get closed => 'クローズ済み';
	@override String get all => '全て';
}

// Path: formFields.since
class _L10nFormFieldsSinceJa implements _L10nFormFieldsSinceEn {
	_L10nFormFieldsSinceJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => '表示開始日時（省略可能）';
	@override String get hint => '表示される課題の最も古い日時を指定してください。';
}

// Path: formFields.issuesPerPages
class _L10nFormFieldsIssuesPerPagesJa implements _L10nFormFieldsIssuesPerPagesEn {
	_L10nFormFieldsIssuesPerPagesJa._(this._root);

	@override final _L10nJa _root; // ignore: unused_field

	// Translations
	@override String get label => 'ページあたり課題数';
	@override String get hint => '1ページに表示される課題の最大数。既定値は20です。';
}

/// Flat map(s) containing all translations.
/// Only for edge cases! For simple maps, use the map function of this library.

extension on _L10nEn {
	dynamic _flatMapFunction(String path) {
		switch (path) {
			case 'common.titleTemplate': return ({required Object screenName}) => '${screenName} - GitHub Viewer';
			case 'common.error': return 'An unexpected error is occurred. Try again.';
			case 'formFields.clientId.label': return 'Client ID';
			case 'formFields.clientId.hint': return 'Client ID configured in your GitHub account setting for this app.';
			case 'formFields.clientSecret.label': return 'Client Secret';
			case 'formFields.clientSecret.hint': return 'Client Secret configured in your GitHub account setting for this app.';
			case 'formFields.doPersist.label': return 'Persists these values.';
			case 'formFields.doPersist.hint': return 'Check it if you want to persist these inputs.';
			case 'formFields.repository.label': return 'Repository (optional)';
			case 'formFields.repository.hint': return 'Target repository name with \'user/repo\' format to be searched.';
			case 'formFields.sortKey.label': return 'Sort key';
			case 'formFields.sortKey.hint': return 'Select sort key of issues. Default is \'Created date/time\'.';
			case 'formFields.sortKey.created': return 'Created date/time';
			case 'formFields.sortKey.updated': return 'Updated date/time';
			case 'formFields.sortKey.comments': return 'Commented date/time';
			case 'formFields.direction.label': return 'List direction';
			case 'formFields.direction.hint': return 'Select direction of issues list which ordered by sort key. Default is \'Descsendant\'.';
			case 'formFields.direction.asc': return 'Ascendant';
			case 'formFields.direction.desc': return 'Descendant';
			case 'formFields.issueState.label': return 'Issue state';
			case 'formFields.issueState.hint': return 'Select issue state to be shown. Default is \'Open\'.';
			case 'formFields.issueState.open': return 'Opening';
			case 'formFields.issueState.closed': return 'Closed';
			case 'formFields.issueState.all': return 'All';
			case 'formFields.since.label': return 'Since (optional)';
			case 'formFields.since.hint': return 'Specify oldest date and time of issues to be shown.';
			case 'formFields.issuesPerPages.label': return 'Issues per pages';
			case 'formFields.issuesPerPages.hint': return 'Maximum issues shown in a page. Default is 20.';
			case 'home.title': return 'Home';
			case 'issue.title': return ({required Object issueNumber, required Object issueTitle}) => 'Issue #${issueNumber}: ${issueTitle}';
			case 'issues.title': return 'Issues';
			case 'issues.search': return 'Search';
			case 'issues.page': return ({required Object page}) => 'Page ${page}';
			case 'issues.next': return 'Next';
			case 'issues.previous': return 'Previous';
			case 'issues.searchCondition': return 'Search Condition';
			case 'issues.searchResult': return ({required Object count}) => 'Search Result (${count} items)';
			case 'issues.empty': return 'There are no issues found.';
			case 'signIn.title': return 'Sign in';
			case 'signIn.signInButtonLabel': return 'Sign in';
			case 'signIn.credentialDescription': return ({required InlineSpan link}) => TextSpan(children: [
				const TextSpan(text: 'Input GitHub client ID and client secret which you registered. See '),
				link,
				const TextSpan(text: ' for details.'),
			]);
			default: return null;
		}
	}
}

extension on _L10nJa {
	dynamic _flatMapFunction(String path) {
		switch (path) {
			case 'common.titleTemplate': return ({required Object screenName}) => '${screenName} - GitHub ビューアー';
			case 'common.error': return '予想外の問題が発生しました。もう一度やり直してみてください。';
			case 'formFields.clientId.label': return 'クライアントID';
			case 'formFields.clientId.hint': return 'このアプリ用にGitHubのアカウント設定で構成したクライアントID。';
			case 'formFields.clientSecret.label': return 'クライアントシークレット';
			case 'formFields.clientSecret.hint': return 'このアプリ用にGitHubのアカウント設定で構成したクライアントシークレット。';
			case 'formFields.doPersist.label': return 'これらの値を保存する';
			case 'formFields.doPersist.hint': return 'これらの入力を保存したい場合にはチェックしてください。';
			case 'formFields.repository.label': return 'リポジトリ（省略可能）';
			case 'formFields.repository.hint': return '検索対象のリポジトリの名前。\'user/repo\'形式です。';
			case 'formFields.sortKey.label': return '並び替えのキー';
			case 'formFields.sortKey.hint': return '課題の並び替えのキーを選択してください。既定値は\'作成日時\'です。';
			case 'formFields.sortKey.created': return '作成日時';
			case 'formFields.sortKey.updated': return '更新日時';
			case 'formFields.sortKey.comments': return 'コメント日時';
			case 'formFields.direction.label': return '一覧の並び順';
			case 'formFields.direction.hint': return '「並び替えのキー」で並び替えられる課題の並び順を選択してください。既定値は\'降順\'です。';
			case 'formFields.direction.asc': return '昇順';
			case 'formFields.direction.desc': return '降順';
			case 'formFields.issueState.label': return '課題の状態';
			case 'formFields.issueState.hint': return '表示する課題の状態。既定値は\'全て\'です。';
			case 'formFields.issueState.open': return 'オープン';
			case 'formFields.issueState.closed': return 'クローズ済み';
			case 'formFields.issueState.all': return '全て';
			case 'formFields.since.label': return '表示開始日時（省略可能）';
			case 'formFields.since.hint': return '表示される課題の最も古い日時を指定してください。';
			case 'formFields.issuesPerPages.label': return 'ページあたり課題数';
			case 'formFields.issuesPerPages.hint': return '1ページに表示される課題の最大数。既定値は20です。';
			case 'home.title': return 'ホーム';
			case 'issues.title': return '課題一覧';
			case 'issues.search': return '検索';
			case 'issues.page': return ({required Object page}) => '${page} ページ';
			case 'issues.next': return '次へ';
			case 'issues.previous': return '前へ';
			case 'issues.searchCondition': return '検索条件';
			case 'issues.searchResult': return ({required Object count}) => '検索結果（${count} 件）';
			case 'issues.empty': return '一致する課題がありません。';
			case 'issue.title': return ({required Object issueNumber, required Object issueTitle}) => '課題 #${issueNumber}: ${issueTitle}';
			case 'signIn.title': return 'サインイン';
			case 'signIn.signInButtonLabel': return 'サインイン';
			case 'signIn.credentialDescription': return ({required InlineSpan link}) => TextSpan(children: [
				const TextSpan(text: 'GitHubに登録した、クライアントIDとクライアントシークレットを入力してください。詳細については、'),
				link,
				const TextSpan(text: ' を参照してください。'),
			]);
			default: return null;
		}
	}
}
