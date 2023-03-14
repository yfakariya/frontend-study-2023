// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target

// **************************************************************************
// CompanionGenerator
// **************************************************************************

import 'dart:ui'
    show Brightness, Color, Radius, TextAlign, TextDirection, VoidCallback;

import 'dart:ui' as ui show BoxHeightStyle, BoxWidthStyle;

import 'package:flutter/foundation.dart' show Key, ValueChanged;

import 'package:flutter/gestures.dart'
    show DragStartBehavior, GestureTapCallback;

import 'package:flutter/material.dart'
    show
        DropdownButtonBuilder,
        DropdownMenuItem,
        InputCounterWidgetBuilder,
        InputDecoration;

import 'package:flutter/painting.dart'
    show
        AlignmentDirectional,
        AlignmentGeometry,
        BorderRadius,
        EdgeInsets,
        StrutStyle,
        TextAlignVertical,
        TextStyle;

import 'package:flutter/services.dart'
    show
        MaxLengthEnforcement,
        MouseCursor,
        SmartDashesType,
        SmartQuotesType,
        TextCapitalization,
        TextInputAction,
        TextInputFormatter,
        TextInputType;

import 'package:flutter/widgets.dart'
    show
        AutovalidateMode,
        BuildContext,
        EditableTextContextMenuBuilder,
        FocusNode,
        ScrollController,
        ScrollPhysics,
        Text,
        TextEditingController,
        TextMagnifierConfiguration,
        Widget;

import 'package:flutter_form_builder/flutter_form_builder.dart'
    show FormBuilderDropdown, FormBuilderTextField, ValueTransformer;

import 'package:form_companion_presenter/form_companion_presenter.dart';

import 'package:meta/meta.dart' show immutable, sealed;

import '../l10n/l10n.g.dart' show l10n;

import '../models/issues.dart' show IssueListSortKey, IssueState, ListDirection;

import 'issues.dart';

/// Defines typed property state accessors
/// for [IssuesPresenter].
@sealed
@immutable
class $IssuesPresenterFormProperties implements FormProperties {
  final FormProperties _underlying;

  /// Gets a [IssuesPresenter] instance which holds this properties state.
  IssuesPresenter get presenter => _underlying.presenter as IssuesPresenter;

  /// Gets a typed [PropertyDescriptor] accessor [$IssuesPresenterPropertyDescriptors]
  /// for [IssuesPresenter].
  late final $IssuesPresenterPropertyDescriptors descriptors;

  /// Gets a typed property value accessor [$IssuesPresenterPropertyValues]
  /// for [IssuesPresenter].
  late final $IssuesPresenterPropertyValues values;

  /// Returns a [$IssuesPresenterFormProperties] which wraps [FormProperties].
  ///
  /// Note that this factory returns [underlying] if [underlying] is
  /// [$IssuesPresenterFormProperties] type.
  factory $IssuesPresenterFormProperties(FormProperties underlying) {
    if (underlying is $IssuesPresenterFormProperties) {
      return underlying;
    }

    if (underlying.presenter is! IssuesPresenter) {
      throw ArgumentError(
        'Specified FormProperties does not hold ${IssuesPresenter} type presenter.',
        'underlying',
      );
    }

    return $IssuesPresenterFormProperties._(underlying);
  }

  $IssuesPresenterFormProperties._(this._underlying) {
    descriptors = $IssuesPresenterPropertyDescriptors._(_underlying);
    values = $IssuesPresenterPropertyValues._(_underlying);
  }

  @override
  bool canSubmit(BuildContext context) => _underlying.canSubmit(context);

  @override
  void Function()? submit(BuildContext context) => _underlying.submit(context);

  @override
  $IssuesPresenterFormProperties copyWithProperties(
    Map<String, Object?> newValues,
  ) {
    final newUnderlying = _underlying.copyWithProperties(newValues);
    if (identical(newUnderlying, _underlying)) {
      return this;
    }

    return $IssuesPresenterFormProperties(newUnderlying);
  }

  @override
  $IssuesPresenterFormProperties copyWithProperty(
    String name,
    Object? newValue,
  ) {
    final newUnderlying = _underlying.copyWithProperty(name, newValue);
    if (identical(newUnderlying, _underlying)) {
      return this;
    }

    return $IssuesPresenterFormProperties(newUnderlying);
  }

  /// Copies this instance with specified new property values specified via
  /// returned [$IssuesPresenterFormPropertiesBuilder] object.
  ///
  /// You must call [$IssuesPresenterFormPropertiesBuilder.build]
  /// to finish copying.
  $IssuesPresenterFormPropertiesBuilder copyWith() =>
      $IssuesPresenterFormPropertiesBuilder._(this);

  @override
  PropertyDescriptor<P, F> getDescriptor<P extends Object, F extends Object>(
    String name,
  ) =>
      _underlying.getDescriptor<P, F>(name);

  @override
  PropertyDescriptor<P, F>?
      tryGetDescriptor<P extends Object, F extends Object>(
    String name,
  ) =>
          _underlying.tryGetDescriptor(name);

  @override
  Iterable<PropertyDescriptor<Object, Object>> getAllDescriptors() =>
      _underlying.getAllDescriptors();

  @override
  Object? getValue(String name) => _underlying.getValue(name);
}

/// Defines typed [PropertyDescriptor] accessors
/// for [IssuesPresenterFormProperties].
@sealed
class $IssuesPresenterPropertyDescriptors {
  final FormProperties _properties;

  $IssuesPresenterPropertyDescriptors._(this._properties);

  /// Gets a [PropertyDescriptor] of `repository` property.
  PropertyDescriptor<String, String> get repository =>
      _properties.getDescriptor('repository')
          as PropertyDescriptor<String, String>;

  /// Gets a [PropertyDescriptor] of `sortKey` property.
  PropertyDescriptor<IssueListSortKey, IssueListSortKey> get sortKey =>
      _properties.getDescriptor('sortKey')
          as PropertyDescriptor<IssueListSortKey, IssueListSortKey>;

  /// Gets a [PropertyDescriptor] of `issueState` property.
  PropertyDescriptor<IssueState, IssueState> get issueState =>
      _properties.getDescriptor('issueState')
          as PropertyDescriptor<IssueState, IssueState>;

  /// Gets a [PropertyDescriptor] of `direction` property.
  PropertyDescriptor<ListDirection, ListDirection> get direction =>
      _properties.getDescriptor('direction')
          as PropertyDescriptor<ListDirection, ListDirection>;

  /// Gets a [PropertyDescriptor] of `issuesPerPages` property.
  PropertyDescriptor<int, String> get issuesPerPages =>
      _properties.getDescriptor('issuesPerPages')
          as PropertyDescriptor<int, String>;
}

/// Defines typed property value accessors
/// for [IssuesPresenterFormProperties].
@sealed
class $IssuesPresenterPropertyValues {
  final FormProperties _properties;

  $IssuesPresenterPropertyValues._(this._properties);

  /// Gets a current value of `repository` property.
  String get repository => _properties.getValue('repository') as String;

  /// Gets a current value of `sortKey` property.
  IssueListSortKey get sortKey =>
      _properties.getValue('sortKey') as IssueListSortKey;

  /// Gets a current value of `issueState` property.
  IssueState get issueState => _properties.getValue('issueState') as IssueState;

  /// Gets a current value of `direction` property.
  ListDirection get direction =>
      _properties.getValue('direction') as ListDirection;

  /// Gets a current value of `issuesPerPages` property.
  int get issuesPerPages => _properties.getValue('issuesPerPages') as int;
}

/// Defines a builder to help [IssuesPresenterFormProperties.copyWith].
@sealed
class $IssuesPresenterFormPropertiesBuilder {
  final $IssuesPresenterFormProperties _properties;
  final Map<String, Object?> _newValues = {};

  $IssuesPresenterFormPropertiesBuilder._(this._properties);

  /// Sets a new value of `repository` property.
  void repository(String value) => _newValues['repository'] = value;

  /// Sets a new value of `sortKey` property.
  void sortKey(IssueListSortKey value) => _newValues['sortKey'] = value;

  /// Sets a new value of `issueState` property.
  void issueState(IssueState value) => _newValues['issueState'] = value;

  /// Sets a new value of `direction` property.
  void direction(ListDirection value) => _newValues['direction'] = value;

  /// Sets a new value of `issuesPerPages` property.
  void issuesPerPages(int value) => _newValues['issuesPerPages'] = value;

  $IssuesPresenterFormProperties build() =>
      _properties.copyWithProperties(_newValues);
}

/// Defines typed property accessors as extension properties for [IssuesPresenter].
extension $IssuesPresenterPropertyExtension on IssuesPresenter {
  /// Gets a current [$IssuesPresenterFormProperties] which holds properties' values
  /// and their [PropertyDescriptor]s.
  $IssuesPresenterFormProperties get properties =>
      $IssuesPresenterFormProperties(propertiesState);

  /// Resets [properties] (and underlying[CompanionPresenterMixin.propertiesState])
  /// with specified new [$IssuesPresenterFormProperties].
  ///
  /// This method also calls [CompanionPresenterMixin.onPropertiesChanged] callback.
  ///
  /// This method returns passed [FormProperties] for convinience.
  ///
  /// This method is preferred over [CompanionPresenterMixin.resetPropertiesState]
  /// because takes and returns more specific [$IssuesPresenterFormProperties] type.
  $IssuesPresenterFormProperties resetProperties(
    $IssuesPresenterFormProperties newProperties,
  ) {
    resetPropertiesState(newProperties);
    return newProperties;
  }
}

/// Defines [FormField] factory methods for properties of [IssuesPresenter].
class $IssuesPresenterFieldFactory {
  final $IssuesPresenterFormProperties _properties;

  $IssuesPresenterFieldFactory._(this._properties);

  /// Gets a [FormField] for `repository` property.
  FormBuilderTextField repository(
    BuildContext context, {
    Key? key,
    bool readOnly = false,
    InputDecoration? decoration,
    ValueChanged<String?>? onChanged,
    ValueTransformer<String?>? valueTransformer,
    bool enabled = true,
    AutovalidateMode? autovalidateMode,
    VoidCallback? onReset,
    FocusNode? focusNode,
    int? maxLines = 1,
    bool? obscureText,
    TextCapitalization textCapitalization = TextCapitalization.none,
    EdgeInsets scrollPadding = const EdgeInsets.all(20.0),
    bool enableInteractiveSelection = true,
    MaxLengthEnforcement? maxLengthEnforcement,
    TextAlign textAlign = TextAlign.start,
    bool autofocus = false,
    bool autocorrect = true,
    double cursorWidth = 2.0,
    double? cursorHeight,
    TextInputType? keyboardType,
    TextStyle? style,
    TextEditingController? controller,
    TextInputAction? textInputAction,
    StrutStyle? strutStyle,
    TextDirection? textDirection,
    int? maxLength,
    VoidCallback? onEditingComplete,
    ValueChanged<String?>? onSubmitted,
    List<TextInputFormatter>? inputFormatters,
    Radius? cursorRadius,
    Color? cursorColor,
    Brightness? keyboardAppearance,
    InputCounterWidgetBuilder? buildCounter,
    bool expands = false,
    int? minLines,
    bool? showCursor,
    GestureTapCallback? onTap,
    bool enableSuggestions = false,
    TextAlignVertical? textAlignVertical,
    DragStartBehavior dragStartBehavior = DragStartBehavior.start,
    ScrollController? scrollController,
    ScrollPhysics? scrollPhysics,
    ui.BoxWidthStyle selectionWidthStyle = ui.BoxWidthStyle.tight,
    SmartDashesType? smartDashesType,
    SmartQuotesType? smartQuotesType,
    ui.BoxHeightStyle selectionHeightStyle = ui.BoxHeightStyle.tight,
    Iterable<String>? autofillHints,
    String obscuringCharacter = '•',
    MouseCursor? mouseCursor,
    EditableTextContextMenuBuilder? contextMenuBuilder,
    TextMagnifierConfiguration? magnifierConfiguration,
  }) {
    final property = _properties.descriptors.repository;
    return FormBuilderTextField(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      readOnly: readOnly,
      decoration: decoration ??
          const InputDecoration().copyWith(
              labelText: l10n.formFields.repository.label,
              hintText: l10n.formFields.repository.hint),
      onChanged: property.onChanged(context, onChanged),
      valueTransformer: valueTransformer,
      enabled: enabled,
      autovalidateMode: autovalidateMode ?? AutovalidateMode.onUserInteraction,
      onReset: onReset,
      focusNode: focusNode,
      maxLines: maxLines,
      obscureText: obscureText ?? property.valueTraits.isSensitive,
      textCapitalization: textCapitalization,
      scrollPadding: scrollPadding,
      enableInteractiveSelection: enableInteractiveSelection,
      maxLengthEnforcement: maxLengthEnforcement,
      textAlign: textAlign,
      autofocus: autofocus,
      autocorrect: autocorrect,
      cursorWidth: cursorWidth,
      cursorHeight: cursorHeight,
      keyboardType: keyboardType,
      style: style,
      controller: controller,
      textInputAction: textInputAction,
      strutStyle: strutStyle,
      textDirection: textDirection,
      maxLength: maxLength,
      onEditingComplete: onEditingComplete,
      onSubmitted: onSubmitted,
      inputFormatters: inputFormatters,
      cursorRadius: cursorRadius,
      cursorColor: cursorColor,
      keyboardAppearance: keyboardAppearance,
      buildCounter: buildCounter,
      expands: expands,
      minLines: minLines,
      showCursor: showCursor,
      onTap: onTap,
      enableSuggestions: enableSuggestions,
      textAlignVertical: textAlignVertical,
      dragStartBehavior: dragStartBehavior,
      scrollController: scrollController,
      scrollPhysics: scrollPhysics,
      selectionWidthStyle: selectionWidthStyle,
      smartDashesType: smartDashesType,
      smartQuotesType: smartQuotesType,
      selectionHeightStyle: selectionHeightStyle,
      autofillHints: autofillHints,
      obscuringCharacter: obscuringCharacter,
      mouseCursor: mouseCursor,
      contextMenuBuilder: contextMenuBuilder,
      magnifierConfiguration: magnifierConfiguration,
    );
  }

  /// Gets a [FormField] for `sortKey` property.
  FormBuilderDropdown<IssueListSortKey> sortKey(
    BuildContext context, {
    Key? key,
    InputDecoration? decoration,
    ValueChanged<IssueListSortKey?>? onChanged,
    ValueTransformer<IssueListSortKey?>? valueTransformer,
    bool enabled = true,
    AutovalidateMode? autovalidateMode,
    VoidCallback? onReset,
    FocusNode? focusNode,
    List<DropdownMenuItem<IssueListSortKey>>? items,
    bool isExpanded = true,
    bool isDense = true,
    int elevation = 8,
    double iconSize = 24.0,
    TextStyle? style,
    Widget? disabledHint,
    Widget? icon,
    Color? iconDisabledColor,
    Color? iconEnabledColor,
    VoidCallback? onTap,
    bool autofocus = false,
    bool shouldRequestFocus = false,
    Color? dropdownColor,
    Color? focusColor,
    double? itemHeight,
    DropdownButtonBuilder? selectedItemBuilder,
    double? menuMaxHeight,
    bool? enableFeedback,
    BorderRadius? borderRadius,
    AlignmentGeometry alignment = AlignmentDirectional.centerStart,
  }) {
    final property = _properties.descriptors.sortKey;
    return FormBuilderDropdown<IssueListSortKey>(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      decoration: decoration ??
          const InputDecoration().copyWith(
              labelText: l10n.formFields.sortKey.label,
              hintText: l10n.formFields.sortKey.hint),
      onChanged: property.onChanged(context, onChanged),
      valueTransformer: valueTransformer,
      enabled: enabled,
      autovalidateMode: autovalidateMode ?? AutovalidateMode.onUserInteraction,
      onReset: onReset,
      focusNode: focusNode,
      items: [
        IssueListSortKey.created,
        IssueListSortKey.updated,
        IssueListSortKey.comments
      ]
          .map((x) => DropdownMenuItem<IssueListSortKey>(
              value: x,
              child: Text(l10n['formFields.sortKey.${x.name}'] as String)))
          .toList(),
      isExpanded: isExpanded,
      isDense: isDense,
      elevation: elevation,
      iconSize: iconSize,
      style: style,
      disabledHint: disabledHint,
      icon: icon,
      iconDisabledColor: iconDisabledColor,
      iconEnabledColor: iconEnabledColor,
      onTap: onTap,
      autofocus: autofocus,
      shouldRequestFocus: shouldRequestFocus,
      dropdownColor: dropdownColor,
      focusColor: focusColor,
      itemHeight: itemHeight,
      selectedItemBuilder: selectedItemBuilder,
      menuMaxHeight: menuMaxHeight,
      enableFeedback: enableFeedback,
      borderRadius: borderRadius,
      alignment: alignment,
    );
  }

  /// Gets a [FormField] for `issueState` property.
  FormBuilderDropdown<IssueState> issueState(
    BuildContext context, {
    Key? key,
    InputDecoration? decoration,
    ValueChanged<IssueState?>? onChanged,
    ValueTransformer<IssueState?>? valueTransformer,
    bool enabled = true,
    AutovalidateMode? autovalidateMode,
    VoidCallback? onReset,
    FocusNode? focusNode,
    List<DropdownMenuItem<IssueState>>? items,
    bool isExpanded = true,
    bool isDense = true,
    int elevation = 8,
    double iconSize = 24.0,
    TextStyle? style,
    Widget? disabledHint,
    Widget? icon,
    Color? iconDisabledColor,
    Color? iconEnabledColor,
    VoidCallback? onTap,
    bool autofocus = false,
    bool shouldRequestFocus = false,
    Color? dropdownColor,
    Color? focusColor,
    double? itemHeight,
    DropdownButtonBuilder? selectedItemBuilder,
    double? menuMaxHeight,
    bool? enableFeedback,
    BorderRadius? borderRadius,
    AlignmentGeometry alignment = AlignmentDirectional.centerStart,
  }) {
    final property = _properties.descriptors.issueState;
    return FormBuilderDropdown<IssueState>(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      decoration: decoration ??
          const InputDecoration().copyWith(
              labelText: l10n.formFields.issueState.label,
              hintText: l10n.formFields.issueState.hint),
      onChanged: property.onChanged(context, onChanged),
      valueTransformer: valueTransformer,
      enabled: enabled,
      autovalidateMode: autovalidateMode ?? AutovalidateMode.onUserInteraction,
      onReset: onReset,
      focusNode: focusNode,
      items: [IssueState.open, IssueState.closed, IssueState.all]
          .map((x) => DropdownMenuItem<IssueState>(
              value: x,
              child: Text(l10n['formFields.issueState.${x.name}'] as String)))
          .toList(),
      isExpanded: isExpanded,
      isDense: isDense,
      elevation: elevation,
      iconSize: iconSize,
      style: style,
      disabledHint: disabledHint,
      icon: icon,
      iconDisabledColor: iconDisabledColor,
      iconEnabledColor: iconEnabledColor,
      onTap: onTap,
      autofocus: autofocus,
      shouldRequestFocus: shouldRequestFocus,
      dropdownColor: dropdownColor,
      focusColor: focusColor,
      itemHeight: itemHeight,
      selectedItemBuilder: selectedItemBuilder,
      menuMaxHeight: menuMaxHeight,
      enableFeedback: enableFeedback,
      borderRadius: borderRadius,
      alignment: alignment,
    );
  }

  /// Gets a [FormField] for `direction` property.
  FormBuilderDropdown<ListDirection> direction(
    BuildContext context, {
    Key? key,
    InputDecoration? decoration,
    ValueChanged<ListDirection?>? onChanged,
    ValueTransformer<ListDirection?>? valueTransformer,
    bool enabled = true,
    AutovalidateMode? autovalidateMode,
    VoidCallback? onReset,
    FocusNode? focusNode,
    List<DropdownMenuItem<ListDirection>>? items,
    bool isExpanded = true,
    bool isDense = true,
    int elevation = 8,
    double iconSize = 24.0,
    TextStyle? style,
    Widget? disabledHint,
    Widget? icon,
    Color? iconDisabledColor,
    Color? iconEnabledColor,
    VoidCallback? onTap,
    bool autofocus = false,
    bool shouldRequestFocus = false,
    Color? dropdownColor,
    Color? focusColor,
    double? itemHeight,
    DropdownButtonBuilder? selectedItemBuilder,
    double? menuMaxHeight,
    bool? enableFeedback,
    BorderRadius? borderRadius,
    AlignmentGeometry alignment = AlignmentDirectional.centerStart,
  }) {
    final property = _properties.descriptors.direction;
    return FormBuilderDropdown<ListDirection>(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      decoration: decoration ??
          const InputDecoration().copyWith(
              labelText: l10n.formFields.direction.label,
              hintText: l10n.formFields.direction.hint),
      onChanged: property.onChanged(context, onChanged),
      valueTransformer: valueTransformer,
      enabled: enabled,
      autovalidateMode: autovalidateMode ?? AutovalidateMode.onUserInteraction,
      onReset: onReset,
      focusNode: focusNode,
      items: [ListDirection.asc, ListDirection.desc]
          .map((x) => DropdownMenuItem<ListDirection>(
              value: x,
              child: Text(l10n['formFields.direction.${x.name}'] as String)))
          .toList(),
      isExpanded: isExpanded,
      isDense: isDense,
      elevation: elevation,
      iconSize: iconSize,
      style: style,
      disabledHint: disabledHint,
      icon: icon,
      iconDisabledColor: iconDisabledColor,
      iconEnabledColor: iconEnabledColor,
      onTap: onTap,
      autofocus: autofocus,
      shouldRequestFocus: shouldRequestFocus,
      dropdownColor: dropdownColor,
      focusColor: focusColor,
      itemHeight: itemHeight,
      selectedItemBuilder: selectedItemBuilder,
      menuMaxHeight: menuMaxHeight,
      enableFeedback: enableFeedback,
      borderRadius: borderRadius,
      alignment: alignment,
    );
  }

  /// Gets a [FormField] for `issuesPerPages` property.
  FormBuilderTextField issuesPerPages(
    BuildContext context, {
    Key? key,
    bool readOnly = false,
    InputDecoration? decoration,
    ValueChanged<String?>? onChanged,
    ValueTransformer<String?>? valueTransformer,
    bool enabled = true,
    AutovalidateMode? autovalidateMode,
    VoidCallback? onReset,
    FocusNode? focusNode,
    int? maxLines = 1,
    bool? obscureText,
    TextCapitalization textCapitalization = TextCapitalization.none,
    EdgeInsets scrollPadding = const EdgeInsets.all(20.0),
    bool enableInteractiveSelection = true,
    MaxLengthEnforcement? maxLengthEnforcement,
    TextAlign textAlign = TextAlign.start,
    bool autofocus = false,
    bool autocorrect = true,
    double cursorWidth = 2.0,
    double? cursorHeight,
    TextInputType? keyboardType,
    TextStyle? style,
    TextEditingController? controller,
    TextInputAction? textInputAction,
    StrutStyle? strutStyle,
    TextDirection? textDirection,
    int? maxLength,
    VoidCallback? onEditingComplete,
    ValueChanged<String?>? onSubmitted,
    List<TextInputFormatter>? inputFormatters,
    Radius? cursorRadius,
    Color? cursorColor,
    Brightness? keyboardAppearance,
    InputCounterWidgetBuilder? buildCounter,
    bool expands = false,
    int? minLines,
    bool? showCursor,
    GestureTapCallback? onTap,
    bool enableSuggestions = false,
    TextAlignVertical? textAlignVertical,
    DragStartBehavior dragStartBehavior = DragStartBehavior.start,
    ScrollController? scrollController,
    ScrollPhysics? scrollPhysics,
    ui.BoxWidthStyle selectionWidthStyle = ui.BoxWidthStyle.tight,
    SmartDashesType? smartDashesType,
    SmartQuotesType? smartQuotesType,
    ui.BoxHeightStyle selectionHeightStyle = ui.BoxHeightStyle.tight,
    Iterable<String>? autofillHints,
    String obscuringCharacter = '•',
    MouseCursor? mouseCursor,
    EditableTextContextMenuBuilder? contextMenuBuilder,
    TextMagnifierConfiguration? magnifierConfiguration,
  }) {
    final property = _properties.descriptors.issuesPerPages;
    return FormBuilderTextField(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      readOnly: readOnly,
      decoration: decoration ??
          const InputDecoration().copyWith(
              labelText: l10n.formFields.issuesPerPages.label,
              hintText: l10n.formFields.issuesPerPages.hint),
      onChanged: property.onChanged(context, onChanged),
      valueTransformer: valueTransformer,
      enabled: enabled,
      autovalidateMode: autovalidateMode ?? AutovalidateMode.onUserInteraction,
      onReset: onReset,
      focusNode: focusNode,
      maxLines: maxLines,
      obscureText: obscureText ?? property.valueTraits.isSensitive,
      textCapitalization: textCapitalization,
      scrollPadding: scrollPadding,
      enableInteractiveSelection: enableInteractiveSelection,
      maxLengthEnforcement: maxLengthEnforcement,
      textAlign: textAlign,
      autofocus: autofocus,
      autocorrect: autocorrect,
      cursorWidth: cursorWidth,
      cursorHeight: cursorHeight,
      keyboardType: keyboardType,
      style: style,
      controller: controller,
      textInputAction: textInputAction,
      strutStyle: strutStyle,
      textDirection: textDirection,
      maxLength: maxLength,
      onEditingComplete: onEditingComplete,
      onSubmitted: onSubmitted,
      inputFormatters: inputFormatters,
      cursorRadius: cursorRadius,
      cursorColor: cursorColor,
      keyboardAppearance: keyboardAppearance,
      buildCounter: buildCounter,
      expands: expands,
      minLines: minLines,
      showCursor: showCursor,
      onTap: onTap,
      enableSuggestions: enableSuggestions,
      textAlignVertical: textAlignVertical,
      dragStartBehavior: dragStartBehavior,
      scrollController: scrollController,
      scrollPhysics: scrollPhysics,
      selectionWidthStyle: selectionWidthStyle,
      smartDashesType: smartDashesType,
      smartQuotesType: smartQuotesType,
      selectionHeightStyle: selectionHeightStyle,
      autofillHints: autofillHints,
      obscuringCharacter: obscuringCharacter,
      mouseCursor: mouseCursor,
      contextMenuBuilder: contextMenuBuilder,
      magnifierConfiguration: magnifierConfiguration,
    );
  }
}

/// Defines an extension property to get [$IssuesPresenterFieldFactory] from [IssuesPresenter].
extension $IssuesPresenterFormPropertiesFieldFactoryExtension
    on $IssuesPresenterFormProperties {
  /// Gets a [FormField] factory.
  $IssuesPresenterFieldFactory get fields =>
      $IssuesPresenterFieldFactory._(this);
}
