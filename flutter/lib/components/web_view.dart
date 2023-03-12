// See LICENCE file in the root.

import 'dart:async';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:meta/meta.dart';
import 'package:url_launcher/url_launcher_string.dart';
import 'package:webview_flutter/webview_flutter.dart' as mobile;
import 'package:webview_windows/webview_windows.dart' as windows;

import 'platform.dart';

abstract class WebView {
  final Color fontColor;
  WebView({
    required this.fontColor,
  });

  static FutureOr<WebView> create(ThemeData theme) async {
    if (!isDesktop) {
      final result = _MobileWebView(
        fontColor: theme.colorScheme.onBackground,
      );
      await result._initialize(theme);
      return result;
    }
    if (Platform.isWindows) {
      final result = _WindowsWebView(
        fontColor: theme.colorScheme.onBackground,
      );
      await result._initialize(theme);
      return result;
    }

    return _PlainTextView(
      fontColor: theme.colorScheme.onBackground,
    );
  }

  FutureOr<void> loadHtml(String html) {
    return loadHtmlCore('''
<html>
<head>
<style type="text/css">
body {
  color: #${fontColor.red.toRadixString(16)}${fontColor.green.toRadixString(16)}${fontColor.blue.toRadixString(16)};
}
</style>
<body>
$html
</body>
</html>
''');
  }

  @visibleForOverriding
  FutureOr<void> loadHtmlCore(String html);

  Widget build(BuildContext context);
}

class _MobileWebView extends WebView {
  late final mobile.WebViewController _controller;

  _MobileWebView({required super.fontColor});

  FutureOr<void> _initialize(ThemeData theme) async {
    final controller = mobile.WebViewController();
    await controller.setBackgroundColor(theme.canvasColor);
    await controller.setNavigationDelegate(
      mobile.NavigationDelegate(
        onNavigationRequest: (request) async {
          await launchUrlString(request.url);
          return mobile.NavigationDecision.prevent;
        },
      ),
    );

    _controller = controller;
  }

  FutureOr<void> loadHtmlCore(String html) async {
    await _controller.loadHtmlString(html);
  }

  Widget build(BuildContext context) => mobile.WebViewWidget(
        controller: _controller,
      );
}

class _WindowsWebView extends WebView {
  late final windows.WebviewController _controller;

  _WindowsWebView({required super.fontColor});

  FutureOr<void> _initialize(ThemeData theme) async {
    final controller = windows.WebviewController();
    await controller.initialize();
    await controller.setBackgroundColor(theme.canvasColor);
    // TODO(yfakariya): set navigation policy after fix

    _controller = controller;
  }

  FutureOr<void> loadHtmlCore(String html) async {
    await _controller.loadStringContent(html);
  }

  Widget build(BuildContext context) => windows.Webview(_controller);
}

class _PlainTextView extends WebView {
  String _content = '';

  _PlainTextView({required super.fontColor});

  FutureOr<void> loadHtml(String html) => loadHtmlCore(html);

  FutureOr<void> loadHtmlCore(String html) {
    _content = html;
  }

  Widget build(BuildContext context) => Text(_content);
}
