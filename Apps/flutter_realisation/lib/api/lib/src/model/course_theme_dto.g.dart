// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_theme_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseThemeDto extends CourseThemeDto {
  @override
  final int id;
  @override
  final String? themeName;
  @override
  final bool isActive;

  factory _$CourseThemeDto([void Function(CourseThemeDtoBuilder)? updates]) =>
      (CourseThemeDtoBuilder()..update(updates))._build();

  _$CourseThemeDto._({required this.id, this.themeName, required this.isActive})
      : super._();
  @override
  CourseThemeDto rebuild(void Function(CourseThemeDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  CourseThemeDtoBuilder toBuilder() => CourseThemeDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseThemeDto &&
        id == other.id &&
        themeName == other.themeName &&
        isActive == other.isActive;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, themeName.hashCode);
    _$hash = $jc(_$hash, isActive.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'CourseThemeDto')
          ..add('id', id)
          ..add('themeName', themeName)
          ..add('isActive', isActive))
        .toString();
  }
}

class CourseThemeDtoBuilder
    implements Builder<CourseThemeDto, CourseThemeDtoBuilder> {
  _$CourseThemeDto? _$v;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  String? _themeName;
  String? get themeName => _$this._themeName;
  set themeName(String? themeName) => _$this._themeName = themeName;

  bool? _isActive;
  bool? get isActive => _$this._isActive;
  set isActive(bool? isActive) => _$this._isActive = isActive;

  CourseThemeDtoBuilder() {
    CourseThemeDto._defaults(this);
  }

  CourseThemeDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _themeName = $v.themeName;
      _isActive = $v.isActive;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(CourseThemeDto other) {
    _$v = other as _$CourseThemeDto;
  }

  @override
  void update(void Function(CourseThemeDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseThemeDto build() => _build();

  _$CourseThemeDto _build() {
    final _$result = _$v ??
        _$CourseThemeDto._(
          id: BuiltValueNullFieldError.checkNotNull(
              id, r'CourseThemeDto', 'id'),
          themeName: themeName,
          isActive: BuiltValueNullFieldError.checkNotNull(
              isActive, r'CourseThemeDto', 'isActive'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
