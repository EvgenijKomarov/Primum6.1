// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_rank_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseRankDto extends CourseRankDto {
  @override
  final int id;
  @override
  final int level;
  @override
  final String? rank;
  @override
  final int requiredExperience;

  factory _$CourseRankDto([void Function(CourseRankDtoBuilder)? updates]) =>
      (CourseRankDtoBuilder()..update(updates))._build();

  _$CourseRankDto._({
    required this.id,
    required this.level,
    this.rank,
    required this.requiredExperience,
  }) : super._();
  @override
  CourseRankDto rebuild(void Function(CourseRankDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  CourseRankDtoBuilder toBuilder() => CourseRankDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseRankDto &&
        id == other.id &&
        level == other.level &&
        rank == other.rank &&
        requiredExperience == other.requiredExperience;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, level.hashCode);
    _$hash = $jc(_$hash, rank.hashCode);
    _$hash = $jc(_$hash, requiredExperience.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'CourseRankDto')
          ..add('id', id)
          ..add('level', level)
          ..add('rank', rank)
          ..add('requiredExperience', requiredExperience))
        .toString();
  }
}

class CourseRankDtoBuilder
    implements Builder<CourseRankDto, CourseRankDtoBuilder> {
  _$CourseRankDto? _$v;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  int? _level;
  int? get level => _$this._level;
  set level(int? level) => _$this._level = level;

  String? _rank;
  String? get rank => _$this._rank;
  set rank(String? rank) => _$this._rank = rank;

  int? _requiredExperience;
  int? get requiredExperience => _$this._requiredExperience;
  set requiredExperience(int? requiredExperience) =>
      _$this._requiredExperience = requiredExperience;

  CourseRankDtoBuilder() {
    CourseRankDto._defaults(this);
  }

  CourseRankDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _level = $v.level;
      _rank = $v.rank;
      _requiredExperience = $v.requiredExperience;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(CourseRankDto other) {
    _$v = other as _$CourseRankDto;
  }

  @override
  void update(void Function(CourseRankDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseRankDto build() => _build();

  _$CourseRankDto _build() {
    final _$result =
        _$v ??
        _$CourseRankDto._(
          id: BuiltValueNullFieldError.checkNotNull(id, r'CourseRankDto', 'id'),
          level: BuiltValueNullFieldError.checkNotNull(
            level,
            r'CourseRankDto',
            'level',
          ),
          rank: rank,
          requiredExperience: BuiltValueNullFieldError.checkNotNull(
            requiredExperience,
            r'CourseRankDto',
            'requiredExperience',
          ),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
