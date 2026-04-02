// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'admin_profile_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$AdminProfileDto extends AdminProfileDto {
  @override
  final String? displayName;
  @override
  final int userId;
  @override
  final String? status;
  @override
  final BuiltMap<String, bool>? permissions;

  factory _$AdminProfileDto([void Function(AdminProfileDtoBuilder)? updates]) =>
      (AdminProfileDtoBuilder()..update(updates))._build();

  _$AdminProfileDto._({
    this.displayName,
    required this.userId,
    this.status,
    this.permissions,
  }) : super._();
  @override
  AdminProfileDto rebuild(void Function(AdminProfileDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  AdminProfileDtoBuilder toBuilder() => AdminProfileDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is AdminProfileDto &&
        displayName == other.displayName &&
        userId == other.userId &&
        status == other.status &&
        permissions == other.permissions;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, displayName.hashCode);
    _$hash = $jc(_$hash, userId.hashCode);
    _$hash = $jc(_$hash, status.hashCode);
    _$hash = $jc(_$hash, permissions.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'AdminProfileDto')
          ..add('displayName', displayName)
          ..add('userId', userId)
          ..add('status', status)
          ..add('permissions', permissions))
        .toString();
  }
}

class AdminProfileDtoBuilder
    implements Builder<AdminProfileDto, AdminProfileDtoBuilder> {
  _$AdminProfileDto? _$v;

  String? _displayName;
  String? get displayName => _$this._displayName;
  set displayName(String? displayName) => _$this._displayName = displayName;

  int? _userId;
  int? get userId => _$this._userId;
  set userId(int? userId) => _$this._userId = userId;

  String? _status;
  String? get status => _$this._status;
  set status(String? status) => _$this._status = status;

  MapBuilder<String, bool>? _permissions;
  MapBuilder<String, bool> get permissions =>
      _$this._permissions ??= MapBuilder<String, bool>();
  set permissions(MapBuilder<String, bool>? permissions) =>
      _$this._permissions = permissions;

  AdminProfileDtoBuilder() {
    AdminProfileDto._defaults(this);
  }

  AdminProfileDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _displayName = $v.displayName;
      _userId = $v.userId;
      _status = $v.status;
      _permissions = $v.permissions?.toBuilder();
      _$v = null;
    }
    return this;
  }

  @override
  void replace(AdminProfileDto other) {
    _$v = other as _$AdminProfileDto;
  }

  @override
  void update(void Function(AdminProfileDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  AdminProfileDto build() => _build();

  _$AdminProfileDto _build() {
    _$AdminProfileDto _$result;
    try {
      _$result =
          _$v ??
          _$AdminProfileDto._(
            displayName: displayName,
            userId: BuiltValueNullFieldError.checkNotNull(
              userId,
              r'AdminProfileDto',
              'userId',
            ),
            status: status,
            permissions: _permissions?.build(),
          );
    } catch (_) {
      late String _$failedField;
      try {
        _$failedField = 'permissions';
        _permissions?.build();
      } catch (e) {
        throw BuiltValueNestedFieldError(
          r'AdminProfileDto',
          _$failedField,
          e.toString(),
        );
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
