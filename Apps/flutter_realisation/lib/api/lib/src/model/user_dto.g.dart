// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$UserDto extends UserDto {
  @override
  final int id;
  @override
  final String? name;
  @override
  final String? surname;
  @override
  final String? patronymic;
  @override
  final String? displayName;
  @override
  final int cash;
  @override
  final bool isBanned;
  @override
  final bool mailConfirmed;
  @override
  final bool? isApprovedStudent;
  @override
  final bool? isApprovedTeacher;
  @override
  final bool? isAdmin;
  @override
  final bool isAvailable;

  factory _$UserDto([void Function(UserDtoBuilder)? updates]) =>
      (UserDtoBuilder()..update(updates))._build();

  _$UserDto._(
      {required this.id,
      this.name,
      this.surname,
      this.patronymic,
      this.displayName,
      required this.cash,
      required this.isBanned,
      required this.mailConfirmed,
      this.isApprovedStudent,
      this.isApprovedTeacher,
      this.isAdmin,
      required this.isAvailable})
      : super._();
  @override
  UserDto rebuild(void Function(UserDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  UserDtoBuilder toBuilder() => UserDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is UserDto &&
        id == other.id &&
        name == other.name &&
        surname == other.surname &&
        patronymic == other.patronymic &&
        displayName == other.displayName &&
        cash == other.cash &&
        isBanned == other.isBanned &&
        mailConfirmed == other.mailConfirmed &&
        isApprovedStudent == other.isApprovedStudent &&
        isApprovedTeacher == other.isApprovedTeacher &&
        isAdmin == other.isAdmin &&
        isAvailable == other.isAvailable;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, name.hashCode);
    _$hash = $jc(_$hash, surname.hashCode);
    _$hash = $jc(_$hash, patronymic.hashCode);
    _$hash = $jc(_$hash, displayName.hashCode);
    _$hash = $jc(_$hash, cash.hashCode);
    _$hash = $jc(_$hash, isBanned.hashCode);
    _$hash = $jc(_$hash, mailConfirmed.hashCode);
    _$hash = $jc(_$hash, isApprovedStudent.hashCode);
    _$hash = $jc(_$hash, isApprovedTeacher.hashCode);
    _$hash = $jc(_$hash, isAdmin.hashCode);
    _$hash = $jc(_$hash, isAvailable.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'UserDto')
          ..add('id', id)
          ..add('name', name)
          ..add('surname', surname)
          ..add('patronymic', patronymic)
          ..add('displayName', displayName)
          ..add('cash', cash)
          ..add('isBanned', isBanned)
          ..add('mailConfirmed', mailConfirmed)
          ..add('isApprovedStudent', isApprovedStudent)
          ..add('isApprovedTeacher', isApprovedTeacher)
          ..add('isAdmin', isAdmin)
          ..add('isAvailable', isAvailable))
        .toString();
  }
}

class UserDtoBuilder implements Builder<UserDto, UserDtoBuilder> {
  _$UserDto? _$v;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  String? _name;
  String? get name => _$this._name;
  set name(String? name) => _$this._name = name;

  String? _surname;
  String? get surname => _$this._surname;
  set surname(String? surname) => _$this._surname = surname;

  String? _patronymic;
  String? get patronymic => _$this._patronymic;
  set patronymic(String? patronymic) => _$this._patronymic = patronymic;

  String? _displayName;
  String? get displayName => _$this._displayName;
  set displayName(String? displayName) => _$this._displayName = displayName;

  int? _cash;
  int? get cash => _$this._cash;
  set cash(int? cash) => _$this._cash = cash;

  bool? _isBanned;
  bool? get isBanned => _$this._isBanned;
  set isBanned(bool? isBanned) => _$this._isBanned = isBanned;

  bool? _mailConfirmed;
  bool? get mailConfirmed => _$this._mailConfirmed;
  set mailConfirmed(bool? mailConfirmed) =>
      _$this._mailConfirmed = mailConfirmed;

  bool? _isApprovedStudent;
  bool? get isApprovedStudent => _$this._isApprovedStudent;
  set isApprovedStudent(bool? isApprovedStudent) =>
      _$this._isApprovedStudent = isApprovedStudent;

  bool? _isApprovedTeacher;
  bool? get isApprovedTeacher => _$this._isApprovedTeacher;
  set isApprovedTeacher(bool? isApprovedTeacher) =>
      _$this._isApprovedTeacher = isApprovedTeacher;

  bool? _isAdmin;
  bool? get isAdmin => _$this._isAdmin;
  set isAdmin(bool? isAdmin) => _$this._isAdmin = isAdmin;

  bool? _isAvailable;
  bool? get isAvailable => _$this._isAvailable;
  set isAvailable(bool? isAvailable) => _$this._isAvailable = isAvailable;

  UserDtoBuilder() {
    UserDto._defaults(this);
  }

  UserDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _name = $v.name;
      _surname = $v.surname;
      _patronymic = $v.patronymic;
      _displayName = $v.displayName;
      _cash = $v.cash;
      _isBanned = $v.isBanned;
      _mailConfirmed = $v.mailConfirmed;
      _isApprovedStudent = $v.isApprovedStudent;
      _isApprovedTeacher = $v.isApprovedTeacher;
      _isAdmin = $v.isAdmin;
      _isAvailable = $v.isAvailable;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(UserDto other) {
    _$v = other as _$UserDto;
  }

  @override
  void update(void Function(UserDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  UserDto build() => _build();

  _$UserDto _build() {
    final _$result = _$v ??
        _$UserDto._(
          id: BuiltValueNullFieldError.checkNotNull(id, r'UserDto', 'id'),
          name: name,
          surname: surname,
          patronymic: patronymic,
          displayName: displayName,
          cash: BuiltValueNullFieldError.checkNotNull(cash, r'UserDto', 'cash'),
          isBanned: BuiltValueNullFieldError.checkNotNull(
              isBanned, r'UserDto', 'isBanned'),
          mailConfirmed: BuiltValueNullFieldError.checkNotNull(
              mailConfirmed, r'UserDto', 'mailConfirmed'),
          isApprovedStudent: isApprovedStudent,
          isApprovedTeacher: isApprovedTeacher,
          isAdmin: isAdmin,
          isAvailable: BuiltValueNullFieldError.checkNotNull(
              isAvailable, r'UserDto', 'isAvailable'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
