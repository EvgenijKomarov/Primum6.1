// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'lesson_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$LessonDto extends LessonDto {
  @override
  final String? courseName;
  @override
  final int courseId;
  @override
  final int id;
  @override
  final String? teacherDisplayName;
  @override
  final int teacherId;
  @override
  final String? studentDisplayName;
  @override
  final int studentId;
  @override
  final int abonementId;
  @override
  final int price;
  @override
  final LessonStatus lessonStatus;
  @override
  final DateTime dateTime;
  @override
  final String? lessonLink;
  @override
  final double? grade;

  factory _$LessonDto([void Function(LessonDtoBuilder)? updates]) =>
      (LessonDtoBuilder()..update(updates))._build();

  _$LessonDto._({
    this.courseName,
    required this.courseId,
    required this.id,
    this.teacherDisplayName,
    required this.teacherId,
    this.studentDisplayName,
    required this.studentId,
    required this.abonementId,
    required this.price,
    required this.lessonStatus,
    required this.dateTime,
    this.lessonLink,
    this.grade,
  }) : super._();
  @override
  LessonDto rebuild(void Function(LessonDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  LessonDtoBuilder toBuilder() => LessonDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is LessonDto &&
        courseName == other.courseName &&
        courseId == other.courseId &&
        id == other.id &&
        teacherDisplayName == other.teacherDisplayName &&
        teacherId == other.teacherId &&
        studentDisplayName == other.studentDisplayName &&
        studentId == other.studentId &&
        abonementId == other.abonementId &&
        price == other.price &&
        lessonStatus == other.lessonStatus &&
        dateTime == other.dateTime &&
        lessonLink == other.lessonLink &&
        grade == other.grade;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, courseName.hashCode);
    _$hash = $jc(_$hash, courseId.hashCode);
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, teacherDisplayName.hashCode);
    _$hash = $jc(_$hash, teacherId.hashCode);
    _$hash = $jc(_$hash, studentDisplayName.hashCode);
    _$hash = $jc(_$hash, studentId.hashCode);
    _$hash = $jc(_$hash, abonementId.hashCode);
    _$hash = $jc(_$hash, price.hashCode);
    _$hash = $jc(_$hash, lessonStatus.hashCode);
    _$hash = $jc(_$hash, dateTime.hashCode);
    _$hash = $jc(_$hash, lessonLink.hashCode);
    _$hash = $jc(_$hash, grade.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'LessonDto')
          ..add('courseName', courseName)
          ..add('courseId', courseId)
          ..add('id', id)
          ..add('teacherDisplayName', teacherDisplayName)
          ..add('teacherId', teacherId)
          ..add('studentDisplayName', studentDisplayName)
          ..add('studentId', studentId)
          ..add('abonementId', abonementId)
          ..add('price', price)
          ..add('lessonStatus', lessonStatus)
          ..add('dateTime', dateTime)
          ..add('lessonLink', lessonLink)
          ..add('grade', grade))
        .toString();
  }
}

class LessonDtoBuilder implements Builder<LessonDto, LessonDtoBuilder> {
  _$LessonDto? _$v;

  String? _courseName;
  String? get courseName => _$this._courseName;
  set courseName(String? courseName) => _$this._courseName = courseName;

  int? _courseId;
  int? get courseId => _$this._courseId;
  set courseId(int? courseId) => _$this._courseId = courseId;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  String? _teacherDisplayName;
  String? get teacherDisplayName => _$this._teacherDisplayName;
  set teacherDisplayName(String? teacherDisplayName) =>
      _$this._teacherDisplayName = teacherDisplayName;

  int? _teacherId;
  int? get teacherId => _$this._teacherId;
  set teacherId(int? teacherId) => _$this._teacherId = teacherId;

  String? _studentDisplayName;
  String? get studentDisplayName => _$this._studentDisplayName;
  set studentDisplayName(String? studentDisplayName) =>
      _$this._studentDisplayName = studentDisplayName;

  int? _studentId;
  int? get studentId => _$this._studentId;
  set studentId(int? studentId) => _$this._studentId = studentId;

  int? _abonementId;
  int? get abonementId => _$this._abonementId;
  set abonementId(int? abonementId) => _$this._abonementId = abonementId;

  int? _price;
  int? get price => _$this._price;
  set price(int? price) => _$this._price = price;

  LessonStatus? _lessonStatus;
  LessonStatus? get lessonStatus => _$this._lessonStatus;
  set lessonStatus(LessonStatus? lessonStatus) =>
      _$this._lessonStatus = lessonStatus;

  DateTime? _dateTime;
  DateTime? get dateTime => _$this._dateTime;
  set dateTime(DateTime? dateTime) => _$this._dateTime = dateTime;

  String? _lessonLink;
  String? get lessonLink => _$this._lessonLink;
  set lessonLink(String? lessonLink) => _$this._lessonLink = lessonLink;

  double? _grade;
  double? get grade => _$this._grade;
  set grade(double? grade) => _$this._grade = grade;

  LessonDtoBuilder() {
    LessonDto._defaults(this);
  }

  LessonDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _courseName = $v.courseName;
      _courseId = $v.courseId;
      _id = $v.id;
      _teacherDisplayName = $v.teacherDisplayName;
      _teacherId = $v.teacherId;
      _studentDisplayName = $v.studentDisplayName;
      _studentId = $v.studentId;
      _abonementId = $v.abonementId;
      _price = $v.price;
      _lessonStatus = $v.lessonStatus;
      _dateTime = $v.dateTime;
      _lessonLink = $v.lessonLink;
      _grade = $v.grade;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(LessonDto other) {
    _$v = other as _$LessonDto;
  }

  @override
  void update(void Function(LessonDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  LessonDto build() => _build();

  _$LessonDto _build() {
    final _$result =
        _$v ??
        _$LessonDto._(
          courseName: courseName,
          courseId: BuiltValueNullFieldError.checkNotNull(
            courseId,
            r'LessonDto',
            'courseId',
          ),
          id: BuiltValueNullFieldError.checkNotNull(id, r'LessonDto', 'id'),
          teacherDisplayName: teacherDisplayName,
          teacherId: BuiltValueNullFieldError.checkNotNull(
            teacherId,
            r'LessonDto',
            'teacherId',
          ),
          studentDisplayName: studentDisplayName,
          studentId: BuiltValueNullFieldError.checkNotNull(
            studentId,
            r'LessonDto',
            'studentId',
          ),
          abonementId: BuiltValueNullFieldError.checkNotNull(
            abonementId,
            r'LessonDto',
            'abonementId',
          ),
          price: BuiltValueNullFieldError.checkNotNull(
            price,
            r'LessonDto',
            'price',
          ),
          lessonStatus: BuiltValueNullFieldError.checkNotNull(
            lessonStatus,
            r'LessonDto',
            'lessonStatus',
          ),
          dateTime: BuiltValueNullFieldError.checkNotNull(
            dateTime,
            r'LessonDto',
            'dateTime',
          ),
          lessonLink: lessonLink,
          grade: grade,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
