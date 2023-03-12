// See LICENCE file in the root.

import 'package:flutter/material.dart';

class ChipPanel extends StatelessWidget {
  final String? plainText;
  final List<InlineSpan>? richTexts;
  final Color? backgroundColor;
  final Icon? icon;
  ChipPanel({
    super.key,
    this.icon,
    this.backgroundColor,
    this.plainText,
    this.richTexts,
  }) : assert(
          plainText != null || richTexts != null,
          'Specify one of plainText or richTexts.',
        );

  ChipPanel.info({Key? key, String? plainText, List<InlineSpan>? richTexts})
      : this(
          key: key,
          backgroundColor: Colors.blue[100],
          icon: Icon(
            Icons.info,
            color: Colors.blue[800],
          ),
          plainText: plainText,
          richTexts: richTexts,
        );

  ChipPanel.error({Key? key, String? plainText, List<InlineSpan>? richTexts})
      : this(
          key: key,
          backgroundColor: Colors.red[100],
          icon: Icon(
            Icons.error,
            color: Colors.red[800],
          ),
          plainText: plainText,
          richTexts: richTexts,
        );

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final textColor = theme.brightness == Brightness.dark
        ? theme.colorScheme.background
        : theme.colorScheme.onBackground;
    final style = theme.textTheme.titleSmall!.copyWith(color: textColor);
    return Padding(
      padding: EdgeInsets.all(4),
      child: Container(
        decoration: BoxDecoration(
          border: Border.all(color: Theme.of(context).colorScheme.onBackground),
          borderRadius: BorderRadius.circular(8),
          color: backgroundColor,
        ),
        padding: EdgeInsets.all(4),
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            if (icon != null) icon!,
            SizedBox(width: 8),
            Expanded(
              child: plainText != null
                  ? Text(
                      plainText!,
                      style: style,
                    )
                  : Text.rich(
                      TextSpan(
                        children: richTexts,
                        style: style,
                      ),
                    ),
            ),
          ],
        ),
      ),
    );
  }
}
