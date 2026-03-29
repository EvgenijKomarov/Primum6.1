// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseDto extends CourseDto {
  @override
  final int id;
  @override
  final String? name;
  @override
  final String? teacherName;
  @override
  final String? courseThemeName;
  @override
  final String? about;
  @override
  final int courseThemeId;
  @override
  final int teacherId;
  @override
  final int price;
  @override
  final int maxLessons;
  @override
  final int freeLessons;
  @override
  final String? teacherAbout;
  @override
  final bool isActive;
  @override
  final int level;
  @override
  final String? rank;
  @override
  final ApproveStatus approveStatus;

  factory _$CourseDto([void Function(CourseDtoBuilder)? updates]) =>
      (CourseDtoBuilder()..update(updates))._build();

  _$CourseDto._(
      {required this.id,
      this.name,
      this.teacherName,
      this.courseThemeName,
      this.about,
      required this.courseThemeId,
      required this.teacherId,
      required this.price,
      required this.maxLessons,
      required this.freeLessons,
      this.teacherAbout,
      required this.isActive,
      required this.level,
      this.rank,
      required this.approveStatus})
      : super._();
  @override
  CourseDto rebuild(void Function(CourseDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  CourseDtoBuilder toBuilder() => CourseDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseDto &&
        id == other.id &&
        name == other.name &&
        teacherName == other.teacherName &&
        courseThemeName == other.courseThemeName &&
        about == other.about &&
        courseThemeId == other.courseThemeId &&
        teacherId == other.teacherId &&
        price == other.price &&
        maxLessons == other.maxLessons &&
        freeLessons == other.freeLessons &&
        teacherAbout == other.teacherAbout &&
        isActive == other.isActive &&
        level == other.level &&
        rank == other.rank &&
        approveStatus == other.approveStatus;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, name.hashCode);
    _$hash = $jc(_$hash, teacherName.hashCode);
    _$hash = $jc(_$hash, courseThemeName.hashCode);
    _$hash = $jc(_$hash, about.hashCode);
    _$hash = $jc(_$hash, courseThemeId.hashCode);
    _$hash = $jc(_$hash, teacherId.hashCode);
    _$hash = $jc(_$hash, price.hashCode);
    _$hash = $jc(_$hash, maxLessons.hashCode);
    _$hash = $jc(_$hash, freeLessons.hashCode);
    _$hash = $jc(_$hash, teacherAbout.hashCode);
    _$hash = $jc(_$hash, isActive.hashCode);
    _$hash = $jc(_$hash, level.hashCode);
    _$hash = $jc(_$hash, rank.hashCode);
    _$hash = $jc(_$hash, approveStatus.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'CourseDto')
          ..add('id', id)
          ..add('name', name)
          ..add('teacherName', teacherName)
          ..add('courseThemeName', courseThemeName)
          ..add('about', about)
          ..add('courseThemeId', courseThemeId)
          ..add('teacherId', teacherId)
          ..add('price', price)
          ..add('maxLessons', maxLessons)
          ..add('freeLessons', freeLessons)
          ..add('teacherAbout', teacherAbout)
          ..add('isActive', isActive)
          ..add('level', level)
          ..add('rank', rank)
          ..add('approveStatus', approveStatus))
        .toString();
  }
}

class CourseDtoBuilder implements Builder<CourseDto, CourseDtoBuilder> {
  _$CourseDto? _$v;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  String? _name;
  String? get name => _$this._name;
  set name(String? name) => _$this._name = name;

  String? _teacherName;
  String? get teacherName => _$this._teacherName;
  set teacherName(String? teacherName) => _$this._teacherName = teacherName;

  String? _courseThemeName;
  String? get courseThemeName => _$this._courseThemeName;
  set courseThemeName(String? courseThemeName) =>
      _$this._courseThemeName = courseThemeName;

  String? _about;
  String? get about => _$this._about;
  set about(String? about) => _$this._about = about;

  int? _courseThemeId;
  int? get courseThemeId => _$this._courseThemeId;
  set courseThemeId(int? courseThemeId) =>
      _$this._courseThemeId = courseThemeId;

  int? _teacherId;
  int? get teacherId => _$this._teacherId;
  set teacherId(int? teacherId) => _$this._teacherId = teacherId;

  int? _price;
  int? get price => _$this._price;
  set price(int? price) => _$this._price = price;

  int? _maxLessons;
  int? get maxLessons => _$this._maxLessons;
  set maxLessons(int? maxLessons) => _$this._maxLessons = maxLessons;

  int? _freeLessons;
  int? get freeLessons => _$this._freeLessons;
  set freeLessons(int? freeLessons) => _$this._freeLessons = freeLessons;

  String? _teacherAbout;
  String? get teacherAbout => _$this._teacherAbout;
  set teacherAbout(String? teacherAbout) => _$this._teacherAbout = teacherAbout;

  bool? _isActive;
  bool? get isActive => _$this._isActive;
  set isActive(bool? isActive) => _$this._isActive = isActive;

  int? _level;
  int? get level => _$this._level;
  set level(int? level) => _$this._level = level;

  String? _rank;
  String? get rank => _$this._rank;
  set rank(String? rank) => _$this._rank = rank;

  ApproveStatus? _approveStatus;
  ApproveStatus? get approveStatus => _$this._approveStatus;
  set approveStatus(ApproveStatus? approveStatus) =>
      _$this._approveStatus = approveStatus;

  CourseDtoBuilder() {
    CourseDto._defaults(this);
  }

  CourseDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _name = $v.name;
      _teacherName = $v.teacherName;
      _courseThemeName = $v.courseThemeName;
      _about = $v.about;
      _courseThemeId = $v.courseThemeId;
      _teacherId = $v.teacherId;
      _price = $v.price;
      _maxLessons = $v.maxLessons;
      _freeLessons = $v.freeLessons;
      _teacherAbout = $v.teacherAbout;
      _isActive = $v.isActive;
      _level = $v.level;
      _rank = $v.rank;
      _approveStatus = $v.approveStatus;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(CourseDto other) {
    _$v = other as _$CourseDto;
  }

  @override
  void update(void Function(CourseDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseDto build() => _build();

  _$CourseDto _build() {
    final _$result = _$v ??
        _$CourseDto._(
          id: BuiltValueNullFieldError.checkNotNull(id, r'CourseDto', 'id'),
          name: name,
          teacherName: teacherName,
          courseThemeName: courseThemeName,
          about: about,
          courseThemeId: BuiltValueNullFieldError.checkNotNull(
              courseThemeId, r'CourseDto', 'courseThemeId'),
          teacherId: BuiltValueNullFieldError.checkNotNull(
              teacherId, r'CourseDto', 'teacherId'),
          price: BuiltValueNullFieldError.checkNotNull(
              price, r'CourseDto', 'price'),
          maxLessons: BuiltValueNullFieldError.checkNotNull(
              maxLessons, r'CourseDto', 'maxLessons'),
          freeLessons: BuiltValueNullFieldError.checkNotNull(
              freeLessons, r'CourseDto', 'freeLessons'),
          teacherAbout: teacherAbout,
          isActive: BuiltValueNullFieldError.checkNotNull(
              isActive, r'CourseDto', 'isActive'),
          level: BuiltValueNullFieldError.checkNotNull(
              level, r'CourseDto', 'level'),
          rank: rank,
          approveStatus: BuiltValueNullFieldError.checkNotNull(
              approveStatus, r'CourseDto', 'approveStatus'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
