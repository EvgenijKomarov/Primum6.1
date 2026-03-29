// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_dto_lite.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$UserDtoLite extends UserDtoLite {
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
  final bool? isApprovedStudent;
  @override
  final bool? isApprovedTeacher;
  @override
  final bool? isAdmin;
  @override
  final bool isAvailable;

  factory _$UserDtoLite([void Function(UserDtoLiteBuilder)? updates]) =>
      (UserDtoLiteBuilder()..update(updates))._build();

  _$UserDtoLite._(
      {required this.id,
      this.name,
      this.surname,
      this.patronymic,
      this.displayName,
      this.isApprovedStudent,
      this.isApprovedTeacher,
      this.isAdmin,
      required this.isAvailable})
      : super._();
  @override
  UserDtoLite rebuild(void Function(UserDtoLiteBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  UserDtoLiteBuilder toBuilder() => UserDtoLiteBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is UserDtoLite &&
        id == other.id &&
        name == other.name &&
        surname == other.surname &&
        patronymic == other.patronymic &&
        displayName == other.displayName &&
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
    _$hash = $jc(_$hash, isApprovedStudent.hashCode);
    _$hash = $jc(_$hash, isApprovedTeacher.hashCode);
    _$hash = $jc(_$hash, isAdmin.hashCode);
    _$hash = $jc(_$hash, isAvailable.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'UserDtoLite')
          ..add('id', id)
          ..add('name', name)
          ..add('surname', surname)
          ..add('patronymic', patronymic)
          ..add('displayName', displayName)
          ..add('isApprovedStudent', isApprovedStudent)
          ..add('isApprovedTeacher', isApprovedTeacher)
          ..add('isAdmin', isAdmin)
          ..add('isAvailable', isAvailable))
        .toString();
  }
}

class UserDtoLiteBuilder implements Builder<UserDtoLite, UserDtoLiteBuilder> {
  _$UserDtoLite? _$v;

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

  UserDtoLiteBuilder() {
    UserDtoLite._defaults(this);
  }

  UserDtoLiteBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _name = $v.name;
      _surname = $v.surname;
      _patronymic = $v.patronymic;
      _displayName = $v.displayName;
      _isApprovedStudent = $v.isApprovedStudent;
      _isApprovedTeacher = $v.isApprovedTeacher;
      _isAdmin = $v.isAdmin;
      _isAvailable = $v.isAvailable;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(UserDtoLite other) {
    _$v = other as _$UserDtoLite;
  }

  @override
  void update(void Function(UserDtoLiteBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  UserDtoLite build() => _build();

  _$UserDtoLite _build() {
    final _$result = _$v ??
        _$UserDtoLite._(
          id: BuiltValueNullFieldError.checkNotNull(id, r'UserDtoLite', 'id'),
          name: name,
          surname: surname,
          patronymic: patronymic,
          displayName: displayName,
          isApprovedStudent: isApprovedStudent,
          isApprovedTeacher: isApprovedTeacher,
          isAdmin: isAdmin,
          isAvailable: BuiltValueNullFieldError.checkNotNull(
              isAvailable, r'UserDtoLite', 'isAvailable'),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
