// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'teacher_rank_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$TeacherRankDto extends TeacherRankDto {
  @override
  final int id;
  @override
  final int level;
  @override
  final String? rank;
  @override
  final int requiredExperience;
  @override
  final double earningMultiplier;

  factory _$TeacherRankDto([void Function(TeacherRankDtoBuilder)? updates]) =>
      (TeacherRankDtoBuilder()..update(updates))._build();

  _$TeacherRankDto._(
      {required this.id,
      required this.level,
      this.rank,
      required this.requiredExperience,
      required this.earningMultiplier})
      : super._();
  @override
  TeacherRankDto rebuild(void Function(TeacherRankDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  TeacherRankDtoBuilder toBuilder() => TeacherRankDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is TeacherRankDto &&
        id == other.id &&
        level == other.level &&
        rank == other.rank &&
        requiredExperience == other.requiredExperience &&
        earningMultiplier == other.earningMultiplier;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, level.hashCode);
    _$hash = $jc(_$hash, rank.hashCode);
    _$hash = $jc(_$hash, requiredExperience.hashCode);
    _$hash = $jc(_$hash, earningMultiplier.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'TeacherRankDto')
          ..add('id', id)
          ..add('level', level)
          ..add('rank', rank)
          ..add('requiredExperience', requiredExperience)
          ..add('earningMultiplier', earningMultiplier))
        .toString();
  }
}

class TeacherRankDtoBuilder
    implements Builder<TeacherRankDto, TeacherRankDtoBuilder> {
  _$TeacherRankDto? _$v;

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

  double? _earningMultiplier;
  double? get earningMultiplier => _$this._earningMultiplier;
  set earningMultiplier(double? earningMultiplier) =>
      _$this._earningMultiplier = earningMultiplier;

  TeacherRankDtoBuilder() {
    TeacherRankDto._defaults(this);
  }

  TeacherRankDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _level = $v.level;
      _rank = $v.rank;
      _requiredExperience = $v.requiredExperience;
      _earningMultiplier = $v.earningMultiplier;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(TeacherRankDto other) {
    _$v = other as _$TeacherRankDto;
  }

  @override
  void update(void Function(TeacherRankDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  TeacherRankDto build() => _build();

  _$TeacherRankDto _build() {
    final _$result = _$v ??
        _$TeacherRankDto._(
          id: BuiltValueNullFieldError.checkNotNull(
              id, r'TeacherRankDto', 'id'),
          level: BuiltValueNullFieldError.checkNotNull(
              level, r'TeacherRankDto', 'level'),
          rank: rank,
          requiredExperience: BuiltValueNullFieldError.checkNotNull(
              requiredExperience, r'TeacherRankDto', 'requiredExperience'),
          earningMultiplier: BuiltValueNullFieldError.checkNotNull(
              earningMultiplier, r'TeacherRankDto', 'earningMultiplier'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
