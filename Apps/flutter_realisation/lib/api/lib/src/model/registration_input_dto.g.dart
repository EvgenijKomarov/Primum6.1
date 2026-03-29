// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'registration_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$RegistrationInputDto extends RegistrationInputDto {
  @override
  final String? name;
  @override
  final String? surname;
  @override
  final String? patronymic;
  @override
  final String? mailAdress;
  @override
  final String? password;

  factory _$RegistrationInputDto(
          [void Function(RegistrationInputDtoBuilder)? updates]) =>
      (RegistrationInputDtoBuilder()..update(updates))._build();

  _$RegistrationInputDto._(
      {this.name,
      this.surname,
      this.patronymic,
      this.mailAdress,
      this.password})
      : super._();
  @override
  RegistrationInputDto rebuild(
          void Function(RegistrationInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  RegistrationInputDtoBuilder toBuilder() =>
      RegistrationInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is RegistrationInputDto &&
        name == other.name &&
        surname == other.surname &&
        patronymic == other.patronymic &&
        mailAdress == other.mailAdress &&
        password == other.password;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, name.hashCode);
    _$hash = $jc(_$hash, surname.hashCode);
    _$hash = $jc(_$hash, patronymic.hashCode);
    _$hash = $jc(_$hash, mailAdress.hashCode);
    _$hash = $jc(_$hash, password.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'RegistrationInputDto')
          ..add('name', name)
          ..add('surname', surname)
          ..add('patronymic', patronymic)
          ..add('mailAdress', mailAdress)
          ..add('password', password))
        .toString();
  }
}

class RegistrationInputDtoBuilder
    implements Builder<RegistrationInputDto, RegistrationInputDtoBuilder> {
  _$RegistrationInputDto? _$v;

  String? _name;
  String? get name => _$this._name;
  set name(String? name) => _$this._name = name;

  String? _surname;
  String? get surname => _$this._surname;
  set surname(String? surname) => _$this._surname = surname;

  String? _patronymic;
  String? get patronymic => _$this._patronymic;
  set patronymic(String? patronymic) => _$this._patronymic = patronymic;

  String? _mailAdress;
  String? get mailAdress => _$this._mailAdress;
  set mailAdress(String? mailAdress) => _$this._mailAdress = mailAdress;

  String? _password;
  String? get password => _$this._password;
  set password(String? password) => _$this._password = password;

  RegistrationInputDtoBuilder() {
    RegistrationInputDto._defaults(this);
  }

  RegistrationInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _name = $v.name;
      _surname = $v.surname;
      _patronymic = $v.patronymic;
      _mailAdress = $v.mailAdress;
      _password = $v.password;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(RegistrationInputDto other) {
    _$v = other as _$RegistrationInputDto;
  }

  @override
  void update(void Function(RegistrationInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  RegistrationInputDto build() => _build();

  _$RegistrationInputDto _build() {
    final _$result = _$v ??
        _$RegistrationInputDto._(
          name: name,
          surname: surname,
          patronymic: patronymic,
          mailAdress: mailAdress,
          password: password,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
