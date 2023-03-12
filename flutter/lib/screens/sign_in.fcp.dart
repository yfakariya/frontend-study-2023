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

import 'package:easy_localization/easy_localization.dart'
    show StringTranslateExtension;

import 'package:flutter/foundation.dart' show Key, ValueChanged;

import 'package:flutter/gestures.dart'
    show DragStartBehavior, GestureTapCallback;

import 'package:flutter/material.dart'
    show
        InputBorder,
        InputCounterWidgetBuilder,
        InputDecoration,
        ListTileControlAffinity;

import 'package:flutter/painting.dart'
    show
        BorderSide,
        EdgeInsets,
        OutlinedBorder,
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
        TextEditingController,
        TextMagnifierConfiguration,
        Widget;

import 'package:flutter_form_builder/flutter_form_builder.dart'
    show FormBuilderCheckbox, FormBuilderTextField, ValueTransformer;

import 'package:form_companion_presenter/form_companion_presenter.dart';

import 'package:meta/meta.dart' show immutable, sealed;

import '../l10n/locale_keys.g.dart' show LocaleKeys;

import 'sign_in.dart';

/// Defines typed property state accessors
/// for [SignInPresenter].
@sealed
@immutable
class $SignInPresenterFormProperties implements FormProperties {
  final FormProperties _underlying;

  /// Gets a [SignInPresenter] instance which holds this properties state.
  SignInPresenter get presenter => _underlying.presenter as SignInPresenter;

  /// Gets a typed [PropertyDescriptor] accessor [$SignInPresenterPropertyDescriptors]
  /// for [SignInPresenter].
  late final $SignInPresenterPropertyDescriptors descriptors;

  /// Gets a typed property value accessor [$SignInPresenterPropertyValues]
  /// for [SignInPresenter].
  late final $SignInPresenterPropertyValues values;

  /// Returns a [$SignInPresenterFormProperties] which wraps [FormProperties].
  ///
  /// Note that this factory returns [underlying] if [underlying] is
  /// [$SignInPresenterFormProperties] type.
  factory $SignInPresenterFormProperties(FormProperties underlying) {
    if (underlying is $SignInPresenterFormProperties) {
      return underlying;
    }

    if (underlying.presenter is! SignInPresenter) {
      throw ArgumentError(
        'Specified FormProperties does not hold ${SignInPresenter} type presenter.',
        'underlying',
      );
    }

    return $SignInPresenterFormProperties._(underlying);
  }

  $SignInPresenterFormProperties._(this._underlying) {
    descriptors = $SignInPresenterPropertyDescriptors._(_underlying);
    values = $SignInPresenterPropertyValues._(_underlying);
  }

  @override
  bool canSubmit(BuildContext context) => _underlying.canSubmit(context);

  @override
  void Function()? submit(BuildContext context) => _underlying.submit(context);

  @override
  $SignInPresenterFormProperties copyWithProperties(
    Map<String, Object?> newValues,
  ) {
    final newUnderlying = _underlying.copyWithProperties(newValues);
    if (identical(newUnderlying, _underlying)) {
      return this;
    }

    return $SignInPresenterFormProperties(newUnderlying);
  }

  @override
  $SignInPresenterFormProperties copyWithProperty(
    String name,
    Object? newValue,
  ) {
    final newUnderlying = _underlying.copyWithProperty(name, newValue);
    if (identical(newUnderlying, _underlying)) {
      return this;
    }

    return $SignInPresenterFormProperties(newUnderlying);
  }

  /// Copies this instance with specified new property values specified via
  /// returned [$SignInPresenterFormPropertiesBuilder] object.
  ///
  /// You must call [$SignInPresenterFormPropertiesBuilder.build]
  /// to finish copying.
  $SignInPresenterFormPropertiesBuilder copyWith() =>
      $SignInPresenterFormPropertiesBuilder._(this);

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
/// for [SignInPresenterFormProperties].
@sealed
class $SignInPresenterPropertyDescriptors {
  final FormProperties _properties;

  $SignInPresenterPropertyDescriptors._(this._properties);

  /// Gets a [PropertyDescriptor] of `clientId` property.
  PropertyDescriptor<String, String> get clientId =>
      _properties.getDescriptor('clientId')
          as PropertyDescriptor<String, String>;

  /// Gets a [PropertyDescriptor] of `clientSecret` property.
  PropertyDescriptor<String, String> get clientSecret =>
      _properties.getDescriptor('clientSecret')
          as PropertyDescriptor<String, String>;

  /// Gets a [PropertyDescriptor] of `doPersist` property.
  PropertyDescriptor<bool, bool> get doPersist =>
      _properties.getDescriptor('doPersist') as PropertyDescriptor<bool, bool>;
}

/// Defines typed property value accessors
/// for [SignInPresenterFormProperties].
@sealed
class $SignInPresenterPropertyValues {
  final FormProperties _properties;

  $SignInPresenterPropertyValues._(this._properties);

  /// Gets a current value of `clientId` property.
  String get clientId => _properties.getValue('clientId') as String;

  /// Gets a current value of `clientSecret` property.
  String get clientSecret => _properties.getValue('clientSecret') as String;

  /// Gets a current value of `doPersist` property.
  bool get doPersist => _properties.getValue('doPersist') as bool;
}

/// Defines a builder to help [SignInPresenterFormProperties.copyWith].
@sealed
class $SignInPresenterFormPropertiesBuilder {
  final $SignInPresenterFormProperties _properties;
  final Map<String, Object?> _newValues = {};

  $SignInPresenterFormPropertiesBuilder._(this._properties);

  /// Sets a new value of `clientId` property.
  void clientId(String value) => _newValues['clientId'] = value;

  /// Sets a new value of `clientSecret` property.
  void clientSecret(String value) => _newValues['clientSecret'] = value;

  /// Sets a new value of `doPersist` property.
  void doPersist(bool value) => _newValues['doPersist'] = value;

  $SignInPresenterFormProperties build() =>
      _properties.copyWithProperties(_newValues);
}

/// Defines typed property accessors as extension properties for [SignInPresenter].
extension $SignInPresenterPropertyExtension on SignInPresenter {
  /// Gets a current [$SignInPresenterFormProperties] which holds properties' values
  /// and their [PropertyDescriptor]s.
  $SignInPresenterFormProperties get properties =>
      $SignInPresenterFormProperties(propertiesState);

  /// Resets [properties] (and underlying[CompanionPresenterMixin.propertiesState])
  /// with specified new [$SignInPresenterFormProperties].
  ///
  /// This method also calls [CompanionPresenterMixin.onPropertiesChanged] callback.
  ///
  /// This method returns passed [FormProperties] for convinience.
  ///
  /// This method is preferred over [CompanionPresenterMixin.resetPropertiesState]
  /// because takes and returns more specific [$SignInPresenterFormProperties] type.
  $SignInPresenterFormProperties resetProperties(
    $SignInPresenterFormProperties newProperties,
  ) {
    resetPropertiesState(newProperties);
    return newProperties;
  }
}

/// Defines [FormField] factory methods for properties of [SignInPresenter].
class $SignInPresenterFieldFactory {
  final $SignInPresenterFormProperties _properties;

  $SignInPresenterFieldFactory._(this._properties);

  /// Gets a [FormField] for `clientId` property.
  FormBuilderTextField clientId(
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
    final property = _properties.descriptors.clientId;
    return FormBuilderTextField(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      readOnly: readOnly,
      decoration: decoration ??
          const InputDecoration().copyWith(
              labelText: LocaleKeys.formFields_clientId_label.tr(),
              hintText: LocaleKeys.formFields_clientId_hint.tr()),
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

  /// Gets a [FormField] for `clientSecret` property.
  FormBuilderTextField clientSecret(
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
    final property = _properties.descriptors.clientSecret;
    return FormBuilderTextField(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      readOnly: readOnly,
      decoration: decoration ??
          const InputDecoration().copyWith(
              labelText: LocaleKeys.formFields_clientSecret_label.tr(),
              hintText: LocaleKeys.formFields_clientSecret_hint.tr()),
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

  /// Gets a [FormField] for `doPersist` property.
  FormBuilderCheckbox doPersist(
    BuildContext context, {
    Key? key,
    InputDecoration? decoration,
    ValueChanged<bool?>? onChanged,
    ValueTransformer<bool?>? valueTransformer,
    bool enabled = true,
    AutovalidateMode? autovalidateMode,
    VoidCallback? onReset,
    FocusNode? focusNode,
    required Widget title,
    Color? activeColor,
    bool autofocus = false,
    Color? checkColor,
    EdgeInsets contentPadding = EdgeInsets.zero,
    ListTileControlAffinity controlAffinity = ListTileControlAffinity.leading,
    Widget? secondary,
    bool selected = false,
    bool shouldRequestFocus = false,
    Widget? subtitle,
    bool tristate = false,
    OutlinedBorder? shape,
    BorderSide? side,
  }) {
    final property = _properties.descriptors.doPersist;
    return FormBuilderCheckbox(
      key: key,
      name: property.name,
      validator: property.getValidator(context),
      initialValue: property.getInitialValue(context),
      decoration: decoration ??
          const InputDecoration(
              border: InputBorder.none,
              focusedBorder: InputBorder.none,
              enabledBorder: InputBorder.none,
              errorBorder: InputBorder.none,
              disabledBorder: InputBorder.none),
      onChanged: property.onChanged(context, onChanged),
      valueTransformer: valueTransformer,
      enabled: enabled,
      autovalidateMode: autovalidateMode ?? AutovalidateMode.onUserInteraction,
      onReset: onReset,
      focusNode: focusNode,
      title: title,
      activeColor: activeColor,
      autofocus: autofocus,
      checkColor: checkColor,
      contentPadding: contentPadding,
      controlAffinity: controlAffinity,
      secondary: secondary,
      selected: selected,
      shouldRequestFocus: shouldRequestFocus,
      subtitle: subtitle,
      tristate: tristate,
      shape: shape,
      side: side,
    );
  }
}

/// Defines an extension property to get [$SignInPresenterFieldFactory] from [SignInPresenter].
extension $SignInPresenterFormPropertiesFieldFactoryExtension
    on $SignInPresenterFormProperties {
  /// Gets a [FormField] factory.
  $SignInPresenterFieldFactory get fields =>
      $SignInPresenterFieldFactory._(this);
}
