// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_log_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$IncidentLogDto extends IncidentLogDto {
  @override
  final int id;
  @override
  final int adminUserId;
  @override
  final DateTime dateTime;
  @override
  final String? adminDisplayName;
  @override
  final String? description;

  factory _$IncidentLogDto([void Function(IncidentLogDtoBuilder)? updates]) =>
      (IncidentLogDtoBuilder()..update(updates))._build();

  _$IncidentLogDto._({
    required this.id,
    required this.adminUserId,
    required this.dateTime,
    this.adminDisplayName,
    this.description,
  }) : super._();
  @override
  IncidentLogDto rebuild(void Function(IncidentLogDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  IncidentLogDtoBuilder toBuilder() => IncidentLogDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is IncidentLogDto &&
        id == other.id &&
        adminUserId == other.adminUserId &&
        dateTime == other.dateTime &&
        adminDisplayName == other.adminDisplayName &&
        description == other.description;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, adminUserId.hashCode);
    _$hash = $jc(_$hash, dateTime.hashCode);
    _$hash = $jc(_$hash, adminDisplayName.hashCode);
    _$hash = $jc(_$hash, description.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'IncidentLogDto')
          ..add('id', id)
          ..add('adminUserId', adminUserId)
          ..add('dateTime', dateTime)
          ..add('adminDisplayName', adminDisplayName)
          ..add('description', description))
        .toString();
  }
}

class IncidentLogDtoBuilder
    implements Builder<IncidentLogDto, IncidentLogDtoBuilder> {
  _$IncidentLogDto? _$v;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  int? _adminUserId;
  int? get adminUserId => _$this._adminUserId;
  set adminUserId(int? adminUserId) => _$this._adminUserId = adminUserId;

  DateTime? _dateTime;
  DateTime? get dateTime => _$this._dateTime;
  set dateTime(DateTime? dateTime) => _$this._dateTime = dateTime;

  String? _adminDisplayName;
  String? get adminDisplayName => _$this._adminDisplayName;
  set adminDisplayName(String? adminDisplayName) =>
      _$this._adminDisplayName = adminDisplayName;

  String? _description;
  String? get description => _$this._description;
  set description(String? description) => _$this._description = description;

  IncidentLogDtoBuilder() {
    IncidentLogDto._defaults(this);
  }

  IncidentLogDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _adminUserId = $v.adminUserId;
      _dateTime = $v.dateTime;
      _adminDisplayName = $v.adminDisplayName;
      _description = $v.description;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(IncidentLogDto other) {
    _$v = other as _$IncidentLogDto;
  }

  @override
  void update(void Function(IncidentLogDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  IncidentLogDto build() => _build();

  _$IncidentLogDto _build() {
    final _$result =
        _$v ??
        _$IncidentLogDto._(
          id: BuiltValueNullFieldError.checkNotNull(
            id,
            r'IncidentLogDto',
            'id',
          ),
          adminUserId: BuiltValueNullFieldError.checkNotNull(
            adminUserId,
            r'IncidentLogDto',
            'adminUserId',
          ),
          dateTime: BuiltValueNullFieldError.checkNotNull(
            dateTime,
            r'IncidentLogDto',
            'dateTime',
          ),
          adminDisplayName: adminDisplayName,
          description: description,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
