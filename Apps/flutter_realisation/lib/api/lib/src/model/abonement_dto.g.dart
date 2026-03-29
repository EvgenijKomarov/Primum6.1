// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'abonement_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$AbonementDto extends AbonementDto {
  @override
  final String? studentDisplayName;
  @override
  final int studentId;
  @override
  final String? teacherDisplayName;
  @override
  final int teacherId;
  @override
  final String? courseName;
  @override
  final int? courseId;
  @override
  final String? courseThemeName;
  @override
  final int id;
  @override
  final int courseThemeId;
  @override
  final int pricePerLesson;
  @override
  final double? rating;
  @override
  final AbonementStatus abonementStatus;

  factory _$AbonementDto([void Function(AbonementDtoBuilder)? updates]) =>
      (AbonementDtoBuilder()..update(updates))._build();

  _$AbonementDto._(
      {this.studentDisplayName,
      required this.studentId,
      this.teacherDisplayName,
      required this.teacherId,
      this.courseName,
      this.courseId,
      this.courseThemeName,
      required this.id,
      required this.courseThemeId,
      required this.pricePerLesson,
      this.rating,
      required this.abonementStatus})
      : super._();
  @override
  AbonementDto rebuild(void Function(AbonementDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  AbonementDtoBuilder toBuilder() => AbonementDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is AbonementDto &&
        studentDisplayName == other.studentDisplayName &&
        studentId == other.studentId &&
        teacherDisplayName == other.teacherDisplayName &&
        teacherId == other.teacherId &&
        courseName == other.courseName &&
        courseId == other.courseId &&
        courseThemeName == other.courseThemeName &&
        id == other.id &&
        courseThemeId == other.courseThemeId &&
        pricePerLesson == other.pricePerLesson &&
        rating == other.rating &&
        abonementStatus == other.abonementStatus;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, studentDisplayName.hashCode);
    _$hash = $jc(_$hash, studentId.hashCode);
    _$hash = $jc(_$hash, teacherDisplayName.hashCode);
    _$hash = $jc(_$hash, teacherId.hashCode);
    _$hash = $jc(_$hash, courseName.hashCode);
    _$hash = $jc(_$hash, courseId.hashCode);
    _$hash = $jc(_$hash, courseThemeName.hashCode);
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, courseThemeId.hashCode);
    _$hash = $jc(_$hash, pricePerLesson.hashCode);
    _$hash = $jc(_$hash, rating.hashCode);
    _$hash = $jc(_$hash, abonementStatus.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'AbonementDto')
          ..add('studentDisplayName', studentDisplayName)
          ..add('studentId', studentId)
          ..add('teacherDisplayName', teacherDisplayName)
          ..add('teacherId', teacherId)
          ..add('courseName', courseName)
          ..add('courseId', courseId)
          ..add('courseThemeName', courseThemeName)
          ..add('id', id)
          ..add('courseThemeId', courseThemeId)
          ..add('pricePerLesson', pricePerLesson)
          ..add('rating', rating)
          ..add('abonementStatus', abonementStatus))
        .toString();
  }
}

class AbonementDtoBuilder
    implements Builder<AbonementDto, AbonementDtoBuilder> {
  _$AbonementDto? _$v;

  String? _studentDisplayName;
  String? get studentDisplayName => _$this._studentDisplayName;
  set studentDisplayName(String? studentDisplayName) =>
      _$this._studentDisplayName = studentDisplayName;

  int? _studentId;
  int? get studentId => _$this._studentId;
  set studentId(int? studentId) => _$this._studentId = studentId;

  String? _teacherDisplayName;
  String? get teacherDisplayName => _$this._teacherDisplayName;
  set teacherDisplayName(String? teacherDisplayName) =>
      _$this._teacherDisplayName = teacherDisplayName;

  int? _teacherId;
  int? get teacherId => _$this._teacherId;
  set teacherId(int? teacherId) => _$this._teacherId = teacherId;

  String? _courseName;
  String? get courseName => _$this._courseName;
  set courseName(String? courseName) => _$this._courseName = courseName;

  int? _courseId;
  int? get courseId => _$this._courseId;
  set courseId(int? courseId) => _$this._courseId = courseId;

  String? _courseThemeName;
  String? get courseThemeName => _$this._courseThemeName;
  set courseThemeName(String? courseThemeName) =>
      _$this._courseThemeName = courseThemeName;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  int? _courseThemeId;
  int? get courseThemeId => _$this._courseThemeId;
  set courseThemeId(int? courseThemeId) =>
      _$this._courseThemeId = courseThemeId;

  int? _pricePerLesson;
  int? get pricePerLesson => _$this._pricePerLesson;
  set pricePerLesson(int? pricePerLesson) =>
      _$this._pricePerLesson = pricePerLesson;

  double? _rating;
  double? get rating => _$this._rating;
  set rating(double? rating) => _$this._rating = rating;

  AbonementStatus? _abonementStatus;
  AbonementStatus? get abonementStatus => _$this._abonementStatus;
  set abonementStatus(AbonementStatus? abonementStatus) =>
      _$this._abonementStatus = abonementStatus;

  AbonementDtoBuilder() {
    AbonementDto._defaults(this);
  }

  AbonementDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _studentDisplayName = $v.studentDisplayName;
      _studentId = $v.studentId;
      _teacherDisplayName = $v.teacherDisplayName;
      _teacherId = $v.teacherId;
      _courseName = $v.courseName;
      _courseId = $v.courseId;
      _courseThemeName = $v.courseThemeName;
      _id = $v.id;
      _courseThemeId = $v.courseThemeId;
      _pricePerLesson = $v.pricePerLesson;
      _rating = $v.rating;
      _abonementStatus = $v.abonementStatus;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(AbonementDto other) {
    _$v = other as _$AbonementDto;
  }

  @override
  void update(void Function(AbonementDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  AbonementDto build() => _build();

  _$AbonementDto _build() {
    final _$result = _$v ??
        _$AbonementDto._(
          studentDisplayName: studentDisplayName,
          studentId: BuiltValueNullFieldError.checkNotNull(
              studentId, r'AbonementDto', 'studentId'),
          teacherDisplayName: teacherDisplayName,
          teacherId: BuiltValueNullFieldError.checkNotNull(
              teacherId, r'AbonementDto', 'teacherId'),
          courseName: courseName,
          courseId: courseId,
          courseThemeName: courseThemeName,
          id: BuiltValueNullFieldError.checkNotNull(id, r'AbonementDto', 'id'),
          courseThemeId: BuiltValueNullFieldError.checkNotNull(
              courseThemeId, r'AbonementDto', 'courseThemeId'),
          pricePerLesson: BuiltValueNullFieldError.checkNotNull(
              pricePerLesson, r'AbonementDto', 'pricePerLesson'),
          rating: rating,
          abonementStatus: BuiltValueNullFieldError.checkNotNull(
              abonementStatus, r'AbonementDto', 'abonementStatus'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
