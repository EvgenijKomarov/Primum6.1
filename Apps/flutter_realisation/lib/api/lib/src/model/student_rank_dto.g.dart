// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'student_rank_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$StudentRankDto extends StudentRankDto {
  @override
  final int id;
  @override
  final int level;
  @override
  final String? rank;
  @override
  final int requiredExperience;
  @override
  final double coinDiscount;

  factory _$StudentRankDto([void Function(StudentRankDtoBuilder)? updates]) =>
      (StudentRankDtoBuilder()..update(updates))._build();

  _$StudentRankDto._({
    required this.id,
    required this.level,
    this.rank,
    required this.requiredExperience,
    required this.coinDiscount,
  }) : super._();
  @override
  StudentRankDto rebuild(void Function(StudentRankDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  StudentRankDtoBuilder toBuilder() => StudentRankDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is StudentRankDto &&
        id == other.id &&
        level == other.level &&
        rank == other.rank &&
        requiredExperience == other.requiredExperience &&
        coinDiscount == other.coinDiscount;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, level.hashCode);
    _$hash = $jc(_$hash, rank.hashCode);
    _$hash = $jc(_$hash, requiredExperience.hashCode);
    _$hash = $jc(_$hash, coinDiscount.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'StudentRankDto')
          ..add('id', id)
          ..add('level', level)
          ..add('rank', rank)
          ..add('requiredExperience', requiredExperience)
          ..add('coinDiscount', coinDiscount))
        .toString();
  }
}

class StudentRankDtoBuilder
    implements Builder<StudentRankDto, StudentRankDtoBuilder> {
  _$StudentRankDto? _$v;

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

  double? _coinDiscount;
  double? get coinDiscount => _$this._coinDiscount;
  set coinDiscount(double? coinDiscount) => _$this._coinDiscount = coinDiscount;

  StudentRankDtoBuilder() {
    StudentRankDto._defaults(this);
  }

  StudentRankDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _level = $v.level;
      _rank = $v.rank;
      _requiredExperience = $v.requiredExperience;
      _coinDiscount = $v.coinDiscount;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(StudentRankDto other) {
    _$v = other as _$StudentRankDto;
  }

  @override
  void update(void Function(StudentRankDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  StudentRankDto build() => _build();

  _$StudentRankDto _build() {
    final _$result =
        _$v ??
        _$StudentRankDto._(
          id: BuiltValueNullFieldError.checkNotNull(
            id,
            r'StudentRankDto',
            'id',
          ),
          level: BuiltValueNullFieldError.checkNotNull(
            level,
            r'StudentRankDto',
            'level',
          ),
          rank: rank,
          requiredExperience: BuiltValueNullFieldError.checkNotNull(
            requiredExperience,
            r'StudentRankDto',
            'requiredExperience',
          ),
          coinDiscount: BuiltValueNullFieldError.checkNotNull(
            coinDiscount,
            r'StudentRankDto',
            'coinDiscount',
          ),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
