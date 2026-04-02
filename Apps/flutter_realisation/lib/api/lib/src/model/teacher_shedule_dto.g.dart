// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'teacher_shedule_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$TeacherSheduleDto extends TeacherSheduleDto {
  @override
  final int id;
  @override
  final DayOfWeek dayOfWeek;
  @override
  final int time;
  @override
  final bool isAvailable;
  @override
  final String? studentName;
  @override
  final int? studentId;
  @override
  final String? courseName;
  @override
  final int? courseId;

  factory _$TeacherSheduleDto([
    void Function(TeacherSheduleDtoBuilder)? updates,
  ]) => (TeacherSheduleDtoBuilder()..update(updates))._build();

  _$TeacherSheduleDto._({
    required this.id,
    required this.dayOfWeek,
    required this.time,
    required this.isAvailable,
    this.studentName,
    this.studentId,
    this.courseName,
    this.courseId,
  }) : super._();
  @override
  TeacherSheduleDto rebuild(void Function(TeacherSheduleDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  TeacherSheduleDtoBuilder toBuilder() =>
      TeacherSheduleDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is TeacherSheduleDto &&
        id == other.id &&
        dayOfWeek == other.dayOfWeek &&
        time == other.time &&
        isAvailable == other.isAvailable &&
        studentName == other.studentName &&
        studentId == other.studentId &&
        courseName == other.courseName &&
        courseId == other.courseId;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, dayOfWeek.hashCode);
    _$hash = $jc(_$hash, time.hashCode);
    _$hash = $jc(_$hash, isAvailable.hashCode);
    _$hash = $jc(_$hash, studentName.hashCode);
    _$hash = $jc(_$hash, studentId.hashCode);
    _$hash = $jc(_$hash, courseName.hashCode);
    _$hash = $jc(_$hash, courseId.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'TeacherSheduleDto')
          ..add('id', id)
          ..add('dayOfWeek', dayOfWeek)
          ..add('time', time)
          ..add('isAvailable', isAvailable)
          ..add('studentName', studentName)
          ..add('studentId', studentId)
          ..add('courseName', courseName)
          ..add('courseId', courseId))
        .toString();
  }
}

class TeacherSheduleDtoBuilder
    implements Builder<TeacherSheduleDto, TeacherSheduleDtoBuilder> {
  _$TeacherSheduleDto? _$v;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  DayOfWeek? _dayOfWeek;
  DayOfWeek? get dayOfWeek => _$this._dayOfWeek;
  set dayOfWeek(DayOfWeek? dayOfWeek) => _$this._dayOfWeek = dayOfWeek;

  int? _time;
  int? get time => _$this._time;
  set time(int? time) => _$this._time = time;

  bool? _isAvailable;
  bool? get isAvailable => _$this._isAvailable;
  set isAvailable(bool? isAvailable) => _$this._isAvailable = isAvailable;

  String? _studentName;
  String? get studentName => _$this._studentName;
  set studentName(String? studentName) => _$this._studentName = studentName;

  int? _studentId;
  int? get studentId => _$this._studentId;
  set studentId(int? studentId) => _$this._studentId = studentId;

  String? _courseName;
  String? get courseName => _$this._courseName;
  set courseName(String? courseName) => _$this._courseName = courseName;

  int? _courseId;
  int? get courseId => _$this._courseId;
  set courseId(int? courseId) => _$this._courseId = courseId;

  TeacherSheduleDtoBuilder() {
    TeacherSheduleDto._defaults(this);
  }

  TeacherSheduleDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _dayOfWeek = $v.dayOfWeek;
      _time = $v.time;
      _isAvailable = $v.isAvailable;
      _studentName = $v.studentName;
      _studentId = $v.studentId;
      _courseName = $v.courseName;
      _courseId = $v.courseId;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(TeacherSheduleDto other) {
    _$v = other as _$TeacherSheduleDto;
  }

  @override
  void update(void Function(TeacherSheduleDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  TeacherSheduleDto build() => _build();

  _$TeacherSheduleDto _build() {
    final _$result =
        _$v ??
        _$TeacherSheduleDto._(
          id: BuiltValueNullFieldError.checkNotNull(
            id,
            r'TeacherSheduleDto',
            'id',
          ),
          dayOfWeek: BuiltValueNullFieldError.checkNotNull(
            dayOfWeek,
            r'TeacherSheduleDto',
            'dayOfWeek',
          ),
          time: BuiltValueNullFieldError.checkNotNull(
            time,
            r'TeacherSheduleDto',
            'time',
          ),
          isAvailable: BuiltValueNullFieldError.checkNotNull(
            isAvailable,
            r'TeacherSheduleDto',
            'isAvailable',
          ),
          studentName: studentName,
          studentId: studentId,
          courseName: courseName,
          courseId: courseId,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
