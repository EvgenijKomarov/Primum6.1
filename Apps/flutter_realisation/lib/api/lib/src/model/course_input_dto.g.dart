// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseInputDto extends CourseInputDto {
  @override
  final String? name;
  @override
  final String? description;
  @override
  final int? price;
  @override
  final int? freeLessons;
  @override
  final int? maxLessons;
  @override
  final int? courseThemeId;

  factory _$CourseInputDto([void Function(CourseInputDtoBuilder)? updates]) =>
      (CourseInputDtoBuilder()..update(updates))._build();

  _$CourseInputDto._(
      {this.name,
      this.description,
      this.price,
      this.freeLessons,
      this.maxLessons,
      this.courseThemeId})
      : super._();
  @override
  CourseInputDto rebuild(void Function(CourseInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  CourseInputDtoBuilder toBuilder() => CourseInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseInputDto &&
        name == other.name &&
        description == other.description &&
        price == other.price &&
        freeLessons == other.freeLessons &&
        maxLessons == other.maxLessons &&
        courseThemeId == other.courseThemeId;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, name.hashCode);
    _$hash = $jc(_$hash, description.hashCode);
    _$hash = $jc(_$hash, price.hashCode);
    _$hash = $jc(_$hash, freeLessons.hashCode);
    _$hash = $jc(_$hash, maxLessons.hashCode);
    _$hash = $jc(_$hash, courseThemeId.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'CourseInputDto')
          ..add('name', name)
          ..add('description', description)
          ..add('price', price)
          ..add('freeLessons', freeLessons)
          ..add('maxLessons', maxLessons)
          ..add('courseThemeId', courseThemeId))
        .toString();
  }
}

class CourseInputDtoBuilder
    implements Builder<CourseInputDto, CourseInputDtoBuilder> {
  _$CourseInputDto? _$v;

  String? _name;
  String? get name => _$this._name;
  set name(String? name) => _$this._name = name;

  String? _description;
  String? get description => _$this._description;
  set description(String? description) => _$this._description = description;

  int? _price;
  int? get price => _$this._price;
  set price(int? price) => _$this._price = price;

  int? _freeLessons;
  int? get freeLessons => _$this._freeLessons;
  set freeLessons(int? freeLessons) => _$this._freeLessons = freeLessons;

  int? _maxLessons;
  int? get maxLessons => _$this._maxLessons;
  set maxLessons(int? maxLessons) => _$this._maxLessons = maxLessons;

  int? _courseThemeId;
  int? get courseThemeId => _$this._courseThemeId;
  set courseThemeId(int? courseThemeId) =>
      _$this._courseThemeId = courseThemeId;

  CourseInputDtoBuilder() {
    CourseInputDto._defaults(this);
  }

  CourseInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _name = $v.name;
      _description = $v.description;
      _price = $v.price;
      _freeLessons = $v.freeLessons;
      _maxLessons = $v.maxLessons;
      _courseThemeId = $v.courseThemeId;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(CourseInputDto other) {
    _$v = other as _$CourseInputDto;
  }

  @override
  void update(void Function(CourseInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseInputDto build() => _build();

  _$CourseInputDto _build() {
    final _$result = _$v ??
        _$CourseInputDto._(
          name: name,
          description: description,
          price: price,
          freeLessons: freeLessons,
          maxLessons: maxLessons,
          courseThemeId: courseThemeId,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
