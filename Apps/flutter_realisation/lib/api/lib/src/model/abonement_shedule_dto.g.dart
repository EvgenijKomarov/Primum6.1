// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'abonement_shedule_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$AbonementSheduleDto extends AbonementSheduleDto {
  @override
  final DayOfWeek dayOfWeek;
  @override
  final int time;
  @override
  final String? courseName;
  @override
  final int courseId;
  @override
  final String? teacherDisplayName;
  @override
  final int teacherId;
  @override
  final int id;

  factory _$AbonementSheduleDto(
          [void Function(AbonementSheduleDtoBuilder)? updates]) =>
      (AbonementSheduleDtoBuilder()..update(updates))._build();

  _$AbonementSheduleDto._(
      {required this.dayOfWeek,
      required this.time,
      this.courseName,
      required this.courseId,
      this.teacherDisplayName,
      required this.teacherId,
      required this.id})
      : super._();
  @override
  AbonementSheduleDto rebuild(
          void Function(AbonementSheduleDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  AbonementSheduleDtoBuilder toBuilder() =>
      AbonementSheduleDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is AbonementSheduleDto &&
        dayOfWeek == other.dayOfWeek &&
        time == other.time &&
        courseName == other.courseName &&
        courseId == other.courseId &&
        teacherDisplayName == other.teacherDisplayName &&
        teacherId == other.teacherId &&
        id == other.id;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, dayOfWeek.hashCode);
    _$hash = $jc(_$hash, time.hashCode);
    _$hash = $jc(_$hash, courseName.hashCode);
    _$hash = $jc(_$hash, courseId.hashCode);
    _$hash = $jc(_$hash, teacherDisplayName.hashCode);
    _$hash = $jc(_$hash, teacherId.hashCode);
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'AbonementSheduleDto')
          ..add('dayOfWeek', dayOfWeek)
          ..add('time', time)
          ..add('courseName', courseName)
          ..add('courseId', courseId)
          ..add('teacherDisplayName', teacherDisplayName)
          ..add('teacherId', teacherId)
          ..add('id', id))
        .toString();
  }
}

class AbonementSheduleDtoBuilder
    implements Builder<AbonementSheduleDto, AbonementSheduleDtoBuilder> {
  _$AbonementSheduleDto? _$v;

  DayOfWeek? _dayOfWeek;
  DayOfWeek? get dayOfWeek => _$this._dayOfWeek;
  set dayOfWeek(DayOfWeek? dayOfWeek) => _$this._dayOfWeek = dayOfWeek;

  int? _time;
  int? get time => _$this._time;
  set time(int? time) => _$this._time = time;

  String? _courseName;
  String? get courseName => _$this._courseName;
  set courseName(String? courseName) => _$this._courseName = courseName;

  int? _courseId;
  int? get courseId => _$this._courseId;
  set courseId(int? courseId) => _$this._courseId = courseId;

  String? _teacherDisplayName;
  String? get teacherDisplayName => _$this._teacherDisplayName;
  set teacherDisplayName(String? teacherDisplayName) =>
      _$this._teacherDisplayName = teacherDisplayName;

  int? _teacherId;
  int? get teacherId => _$this._teacherId;
  set teacherId(int? teacherId) => _$this._teacherId = teacherId;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  AbonementSheduleDtoBuilder() {
    AbonementSheduleDto._defaults(this);
  }

  AbonementSheduleDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _dayOfWeek = $v.dayOfWeek;
      _time = $v.time;
      _courseName = $v.courseName;
      _courseId = $v.courseId;
      _teacherDisplayName = $v.teacherDisplayName;
      _teacherId = $v.teacherId;
      _id = $v.id;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(AbonementSheduleDto other) {
    _$v = other as _$AbonementSheduleDto;
  }

  @override
  void update(void Function(AbonementSheduleDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  AbonementSheduleDto build() => _build();

  _$AbonementSheduleDto _build() {
    final _$result = _$v ??
        _$AbonementSheduleDto._(
          dayOfWeek: BuiltValueNullFieldError.checkNotNull(
              dayOfWeek, r'AbonementSheduleDto', 'dayOfWeek'),
          time: BuiltValueNullFieldError.checkNotNull(
              time, r'AbonementSheduleDto', 'time'),
          courseName: courseName,
          courseId: BuiltValueNullFieldError.checkNotNull(
              courseId, r'AbonementSheduleDto', 'courseId'),
          teacherDisplayName: teacherDisplayName,
          teacherId: BuiltValueNullFieldError.checkNotNull(
              teacherId, r'AbonementSheduleDto', 'teacherId'),
          id: BuiltValueNullFieldError.checkNotNull(
              id, r'AbonementSheduleDto', 'id'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
