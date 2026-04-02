// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'teacher_profile_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$TeacherProfileDto extends TeacherProfileDto {
  @override
  final String? displayName;
  @override
  final String? about;
  @override
  final int userId;
  @override
  final bool isAvailable;
  @override
  final int level;
  @override
  final String? rank;

  factory _$TeacherProfileDto([
    void Function(TeacherProfileDtoBuilder)? updates,
  ]) => (TeacherProfileDtoBuilder()..update(updates))._build();

  _$TeacherProfileDto._({
    this.displayName,
    this.about,
    required this.userId,
    required this.isAvailable,
    required this.level,
    this.rank,
  }) : super._();
  @override
  TeacherProfileDto rebuild(void Function(TeacherProfileDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  TeacherProfileDtoBuilder toBuilder() =>
      TeacherProfileDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is TeacherProfileDto &&
        displayName == other.displayName &&
        about == other.about &&
        userId == other.userId &&
        isAvailable == other.isAvailable &&
        level == other.level &&
        rank == other.rank;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, displayName.hashCode);
    _$hash = $jc(_$hash, about.hashCode);
    _$hash = $jc(_$hash, userId.hashCode);
    _$hash = $jc(_$hash, isAvailable.hashCode);
    _$hash = $jc(_$hash, level.hashCode);
    _$hash = $jc(_$hash, rank.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'TeacherProfileDto')
          ..add('displayName', displayName)
          ..add('about', about)
          ..add('userId', userId)
          ..add('isAvailable', isAvailable)
          ..add('level', level)
          ..add('rank', rank))
        .toString();
  }
}

class TeacherProfileDtoBuilder
    implements Builder<TeacherProfileDto, TeacherProfileDtoBuilder> {
  _$TeacherProfileDto? _$v;

  String? _displayName;
  String? get displayName => _$this._displayName;
  set displayName(String? displayName) => _$this._displayName = displayName;

  String? _about;
  String? get about => _$this._about;
  set about(String? about) => _$this._about = about;

  int? _userId;
  int? get userId => _$this._userId;
  set userId(int? userId) => _$this._userId = userId;

  bool? _isAvailable;
  bool? get isAvailable => _$this._isAvailable;
  set isAvailable(bool? isAvailable) => _$this._isAvailable = isAvailable;

  int? _level;
  int? get level => _$this._level;
  set level(int? level) => _$this._level = level;

  String? _rank;
  String? get rank => _$this._rank;
  set rank(String? rank) => _$this._rank = rank;

  TeacherProfileDtoBuilder() {
    TeacherProfileDto._defaults(this);
  }

  TeacherProfileDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _displayName = $v.displayName;
      _about = $v.about;
      _userId = $v.userId;
      _isAvailable = $v.isAvailable;
      _level = $v.level;
      _rank = $v.rank;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(TeacherProfileDto other) {
    _$v = other as _$TeacherProfileDto;
  }

  @override
  void update(void Function(TeacherProfileDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  TeacherProfileDto build() => _build();

  _$TeacherProfileDto _build() {
    final _$result =
        _$v ??
        _$TeacherProfileDto._(
          displayName: displayName,
          about: about,
          userId: BuiltValueNullFieldError.checkNotNull(
            userId,
            r'TeacherProfileDto',
            'userId',
          ),
          isAvailable: BuiltValueNullFieldError.checkNotNull(
            isAvailable,
            r'TeacherProfileDto',
            'isAvailable',
          ),
          level: BuiltValueNullFieldError.checkNotNull(
            level,
            r'TeacherProfileDto',
            'level',
          ),
          rank: rank,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
