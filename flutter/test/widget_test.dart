// See LICENCE file in the root.

import 'package:flutter_test/flutter_test.dart';
import 'package:github_viewer/app.dart';

import 'package:github_viewer/screens/sign_in.dart';

void main() {
  testWidgets('Show sign in page first', (tester) async {
    await tester.pumpWidget(const App());
    expect(find.byType(SignInPage), findsOneWidget);
  });

  // TODO(yfakariya): login flow with mocks
  // TODO(yfakariya): issues with mocks
}
