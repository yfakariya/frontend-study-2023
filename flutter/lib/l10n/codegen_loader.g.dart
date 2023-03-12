// DO NOT EDIT. This is code generated via package:easy_localization/generate.dart

// ignore_for_file: prefer_single_quotes

import 'dart:ui';

import 'package:easy_localization/easy_localization.dart' show AssetLoader;

class CodegenLoader extends AssetLoader{
  const CodegenLoader();

  @override
  Future<Map<String, dynamic>> load(String fullPath, Locale locale ) {
    return Future.value(mapLocales[locale.toString()]);
  }

  static const Map<String,dynamic> en = {
  "titleTemplate": "{screenName} - GitHub Viewer",
  "common": {
    "error": "An unexpected error is occurred. Try again."
  },
  "home": {
    "title": "Home"
  },
  "signIn": {
    "title": "Sign in",
    "signInButtonLabel": "Sign in",
    "credentialDescriptionBeforeLink": "Input GitHub client ID and client secret which you registered. See ",
    "credentialDescriptionAfterLink": " for details."
  },
  "issues": {
    "title": "Issues",
    "search": "Search",
    "page": "Page {page}",
    "next": "Next",
    "previous": "Previous",
    "searchCondition": "Search Condition",
    "searchResult": "Search Result ({count} items)",
    "empty": "There are no issues found."
  },
  "issue": {
    "title": "Issue {issueNumber} - {issueTitle}"
  },
  "formFields": {
    "clientId": {
      "label": "Client ID",
      "hint": "Client ID configured in your GitHub account setting for this app."
    },
    "clientSecret": {
      "label": "Client Secret",
      "hint": "Client Secret configured in your GitHub account setting for this app."
    },
    "doPersist": {
      "label": "Persists these values",
      "hint": "Check it if you want to persist these inputs."
    },
    "repository": {
      "label": "Repository (optional)",
      "hint": "Target repository name with 'user/repo' format to be searched."
    },
    "sortKey": {
      "label": "Sort key",
      "hint": "Select sort key of issues. Default is 'Created date/time'.",
      "created": "Created date/time",
      "updated": "Updated date/time",
      "comments": "Commented date/time"
    },
    "direction": {
      "label": "List direction",
      "hint": "Select direction of issues list which ordered by sort key. Default is 'Descsendant'.",
      "asc": "Ascendant",
      "desc": "Descendant"
    },
    "issueState": {
      "label": "Issue state",
      "hint": "Select issue state to be shown. Default is 'All'.",
      "open": "Opening",
      "closed": "Closed",
      "all": "All"
    },
    "since": {
      "label": "Since (optional)",
      "hint": "Specify oldest date and time of issues to be shown."
    },
    "issuesPerPages": {
      "label": "Issues per pages",
      "hint": "Maximum issues shown in a page. Default is 20."
    }
  }
};
static const Map<String,dynamic> ja = {
  "titleTemplate": "{screenName} - GitHub ビューアー",
  "common": {
    "error": "予想外の問題が発生しました。もう一度やり直してみてください。"
  },
  "home": {
    "title": "ホーム"
  },
  "signIn": {
    "title": "サインイン",
    "signInButtonLabel": "サインイン",
    "credentialDescriptionBeforeLink": "GitHubに登録した、クライアントIDとクライアントシークレットを入力してください。詳細については、",
    "credentialDescriptionAfterLink": "を参照してください。"
  },
  "issues": {
    "title": "課題一覧",
    "search": "検索",
    "page": "{page} ページ",
    "next": "次へ",
    "previous": "前へ",
    "searchCondition": "検索条件",
    "searchResult": "検索結果（{count}件）",
    "empty": "一致する課題がありません。"
  },
  "issue": {
    "title": "課題 {issueNumber} - {issueTitle}"
  },
  "formFields": {
    "clientId": {
      "label": "クライアントID",
      "hint": "このアプリ用にGitHubのアカウント設定で構成したクライアントID。"
    },
    "clientSecret": {
      "label": "クライアントシークレット",
      "hint": "このアプリ用にGitHubのアカウント設定で構成したクライアントシークレット。"
    },
    "doPersist": {
      "label": "これらの値を保存する",
      "hint": "これらの入力を保存したい場合にはチェックしてください。"
    },
    "repository": {
      "label": "リポジトリ（省略可能）",
      "hint": "検索対象のリポジトリの名前。'user/repo'形式です。"
    },
    "sortKey": {
      "label": "並び替えのキー",
      "hint": "課題の並び替えのキーを選択してください。既定値は'作成日時'です。",
      "created": "作成日時",
      "updated": "更新日時",
      "comments": "コメント日時"
    },
    "direction": {
      "label": "一覧の並び順",
      "hint": "「並び替えのキー」で並び替えられる課題の並び順を選択してください。既定値は'降順'です。",
      "asc": "昇順",
      "desc": "降順"
    },
    "issueState": {
      "label": "課題の状態",
      "hint": "表示する課題の状態。既定値は'全て'です。",
      "open": "オープン",
      "closed": "クローズ済み",
      "all": "全て"
    },
    "since": {
      "label": "表示開始日時（省略可能）",
      "hint": "表示される課題の最も古い日時を指定してください。"
    },
    "issuesPerPages": {
      "label": "ページあたり課題数",
      "hint": "1ページに表示される課題の最大数。既定値は20です。"
    }
  }
};
static const Map<String, Map<String,dynamic>> mapLocales = {"en": en, "ja": ja};
}
