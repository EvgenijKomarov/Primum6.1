// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'student_profile_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$StudentProfileDto extends StudentProfileDto {
  @override
  final String? displayName;
  @override
  final int userId;
  @override
  final int coins;
  @override
  final double? rating;
  @override
  final int level;
  @override
  final String? rank;

  factory _$StudentProfileDto(
          [void Function(StudentProfileDtoBuilder)? updates]) =>
      (StudentProfileDtoBuilder()..update(updates))._build();

  _$StudentProfileDto._(
      {this.displayName,
      required this.userId,
      required this.coins,
      this.rating,
      required this.level,
      this.rank})
      : super._();
  @override
  StudentProfileDto rebuild(void Function(StudentProfileDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  StudentProfileDtoBuilder toBuilder() =>
      StudentProfileDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is StudentProfileDto &&
        displayName == other.displayName &&
        userId == other.userId &&
        coins == other.coins &&
        rating == other.rating &&
        level == other.level &&
        rank == other.rank;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, displayName.hashCode);
    _$hash = $jc(_$hash, userId.hashCode);
    _$hash = $jc(_$hash, coins.hashCode);
    _$hash = $jc(_$hash, rating.hashCode);
    _$hash = $jc(_$hash, level.hashCode);
    _$hash = $jc(_$hash, rank.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'StudentProfileDto')
          ..add('displayName', displayName)
          ..add('userId', userId)
          ..add('coins', coins)
          ..add('rating', rating)
          ..add('level', level)
          ..add('rank', rank))
        .toString();
  }
}

class StudentProfileDtoBuilder
    implements Builder<StudentProfileDto, StudentProfileDtoBuilder> {
  _$StudentProfileDto? _$v;

  String? _displayName;
  String? get displayName => _$this._displayName;
  set displayName(String? displayName) => _$this._displayName = displayName;

  int? _userId;
  int? get userId => _$this._userId;
  set userId(int? userId) => _$this._userId = userId;

  int? _coins;
  int? get coins => _$this._coins;
  set coins(int? coins) => _$this._coins = coins;

  double? _rating;
  double? get rating => _$this._rating;
  set rating(double? rating) => _$this._rating = rating;

  int? _level;
  int? get level => _$this._level;
  set level(int? level) => _$this._level = level;

  String? _rank;
  String? get rank => _$this._rank;
  set rank(String? rank) => _$this._rank = rank;

  StudentProfileDtoBuilder() {
    StudentProfileDto._defaults(this);
  }

  StudentProfileDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _displayName = $v.displayName;
      _userId = $v.userId;
      _coins = $v.coins;
      _rating = $v.rating;
      _level = $v.level;
      _rank = $v.rank;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(StudentProfileDto other) {
    _$v = other as _$StudentProfileDto;
  }

  @override
  void update(void Function(StudentProfileDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  StudentProfileDto build() => _build();

  _$StudentProfileDto _build() {
    final _$result = _$v ??
        _$StudentProfileDto._(
          displayName: displayName,
          userId: BuiltValueNullFieldError.checkNotNull(
              userId, r'StudentProfileDto', 'userId'),
          coins: BuiltValueNullFieldError.checkNotNull(
              coins, r'StudentProfileDto', 'coins'),
          rating: rating,
          level: BuiltValueNullFieldError.checkNotNull(
              level, r'StudentProfileDto', 'level'),
          rank: rank,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
