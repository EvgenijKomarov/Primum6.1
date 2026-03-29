// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'teacher_shedule_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$TeacherSheduleInputDto extends TeacherSheduleInputDto {
  @override
  final int? time;
  @override
  final DayOfWeek? dayOfWeek;

  factory _$TeacherSheduleInputDto(
          [void Function(TeacherSheduleInputDtoBuilder)? updates]) =>
      (TeacherSheduleInputDtoBuilder()..update(updates))._build();

  _$TeacherSheduleInputDto._({this.time, this.dayOfWeek}) : super._();
  @override
  TeacherSheduleInputDto rebuild(
          void Function(TeacherSheduleInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  TeacherSheduleInputDtoBuilder toBuilder() =>
      TeacherSheduleInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is TeacherSheduleInputDto &&
        time == other.time &&
        dayOfWeek == other.dayOfWeek;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, time.hashCode);
    _$hash = $jc(_$hash, dayOfWeek.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'TeacherSheduleInputDto')
          ..add('time', time)
          ..add('dayOfWeek', dayOfWeek))
        .toString();
  }
}

class TeacherSheduleInputDtoBuilder
    implements Builder<TeacherSheduleInputDto, TeacherSheduleInputDtoBuilder> {
  _$TeacherSheduleInputDto? _$v;

  int? _time;
  int? get time => _$this._time;
  set time(int? time) => _$this._time = time;

  DayOfWeek? _dayOfWeek;
  DayOfWeek? get dayOfWeek => _$this._dayOfWeek;
  set dayOfWeek(DayOfWeek? dayOfWeek) => _$this._dayOfWeek = dayOfWeek;

  TeacherSheduleInputDtoBuilder() {
    TeacherSheduleInputDto._defaults(this);
  }

  TeacherSheduleInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _time = $v.time;
      _dayOfWeek = $v.dayOfWeek;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(TeacherSheduleInputDto other) {
    _$v = other as _$TeacherSheduleInputDto;
  }

  @override
  void update(void Function(TeacherSheduleInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  TeacherSheduleInputDto build() => _build();

  _$TeacherSheduleInputDto _build() {
    final _$result = _$v ??
        _$TeacherSheduleInputDto._(
          time: time,
          dayOfWeek: dayOfWeek,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
