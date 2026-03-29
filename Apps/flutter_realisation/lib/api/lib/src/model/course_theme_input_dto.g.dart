// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_theme_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseThemeInputDto extends CourseThemeInputDto {
  @override
  final String? themeName;
  @override
  final bool isActive;

  factory _$CourseThemeInputDto(
          [void Function(CourseThemeInputDtoBuilder)? updates]) =>
      (CourseThemeInputDtoBuilder()..update(updates))._build();

  _$CourseThemeInputDto._({this.themeName, required this.isActive}) : super._();
  @override
  CourseThemeInputDto rebuild(
          void Function(CourseThemeInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  CourseThemeInputDtoBuilder toBuilder() =>
      CourseThemeInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseThemeInputDto &&
        themeName == other.themeName &&
        isActive == other.isActive;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, themeName.hashCode);
    _$hash = $jc(_$hash, isActive.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'CourseThemeInputDto')
          ..add('themeName', themeName)
          ..add('isActive', isActive))
        .toString();
  }
}

class CourseThemeInputDtoBuilder
    implements Builder<CourseThemeInputDto, CourseThemeInputDtoBuilder> {
  _$CourseThemeInputDto? _$v;

  String? _themeName;
  String? get themeName => _$this._themeName;
  set themeName(String? themeName) => _$this._themeName = themeName;

  bool? _isActive;
  bool? get isActive => _$this._isActive;
  set isActive(bool? isActive) => _$this._isActive = isActive;

  CourseThemeInputDtoBuilder() {
    CourseThemeInputDto._defaults(this);
  }

  CourseThemeInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _themeName = $v.themeName;
      _isActive = $v.isActive;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(CourseThemeInputDto other) {
    _$v = other as _$CourseThemeInputDto;
  }

  @override
  void update(void Function(CourseThemeInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseThemeInputDto build() => _build();

  _$CourseThemeInputDto _build() {
    final _$result = _$v ??
        _$CourseThemeInputDto._(
          themeName: themeName,
          isActive: BuiltValueNullFieldError.checkNotNull(
              isActive, r'CourseThemeInputDto', 'isActive'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
