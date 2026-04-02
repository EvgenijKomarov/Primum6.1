// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'logging_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$LoggingInputDto extends LoggingInputDto {
  @override
  final String? email;
  @override
  final String? password;

  factory _$LoggingInputDto([void Function(LoggingInputDtoBuilder)? updates]) =>
      (LoggingInputDtoBuilder()..update(updates))._build();

  _$LoggingInputDto._({this.email, this.password}) : super._();
  @override
  LoggingInputDto rebuild(void Function(LoggingInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  LoggingInputDtoBuilder toBuilder() => LoggingInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is LoggingInputDto &&
        email == other.email &&
        password == other.password;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, email.hashCode);
    _$hash = $jc(_$hash, password.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'LoggingInputDto')
          ..add('email', email)
          ..add('password', password))
        .toString();
  }
}

class LoggingInputDtoBuilder
    implements Builder<LoggingInputDto, LoggingInputDtoBuilder> {
  _$LoggingInputDto? _$v;

  String? _email;
  String? get email => _$this._email;
  set email(String? email) => _$this._email = email;

  String? _password;
  String? get password => _$this._password;
  set password(String? password) => _$this._password = password;

  LoggingInputDtoBuilder() {
    LoggingInputDto._defaults(this);
  }

  LoggingInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _email = $v.email;
      _password = $v.password;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(LoggingInputDto other) {
    _$v = other as _$LoggingInputDto;
  }

  @override
  void update(void Function(LoggingInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  LoggingInputDto build() => _build();

  _$LoggingInputDto _build() {
    final _$result =
        _$v ?? _$LoggingInputDto._(email: email, password: password);
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
