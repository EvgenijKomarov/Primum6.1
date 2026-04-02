// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$IncidentDto extends IncidentDto {
  @override
  final int objectId;
  @override
  final String? commonInfo;
  @override
  final IncidentStatus status;
  @override
  final IncidentMeaning meaning;
  @override
  final Permission permissionBy;
  @override
  final BuiltList<IncidentDecision>? decisions;
  @override
  final BuiltList<IncidentLogDto>? linkedLogs;

  factory _$IncidentDto([void Function(IncidentDtoBuilder)? updates]) =>
      (IncidentDtoBuilder()..update(updates))._build();

  _$IncidentDto._({
    required this.objectId,
    this.commonInfo,
    required this.status,
    required this.meaning,
    required this.permissionBy,
    this.decisions,
    this.linkedLogs,
  }) : super._();
  @override
  IncidentDto rebuild(void Function(IncidentDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  IncidentDtoBuilder toBuilder() => IncidentDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is IncidentDto &&
        objectId == other.objectId &&
        commonInfo == other.commonInfo &&
        status == other.status &&
        meaning == other.meaning &&
        permissionBy == other.permissionBy &&
        decisions == other.decisions &&
        linkedLogs == other.linkedLogs;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, objectId.hashCode);
    _$hash = $jc(_$hash, commonInfo.hashCode);
    _$hash = $jc(_$hash, status.hashCode);
    _$hash = $jc(_$hash, meaning.hashCode);
    _$hash = $jc(_$hash, permissionBy.hashCode);
    _$hash = $jc(_$hash, decisions.hashCode);
    _$hash = $jc(_$hash, linkedLogs.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'IncidentDto')
          ..add('objectId', objectId)
          ..add('commonInfo', commonInfo)
          ..add('status', status)
          ..add('meaning', meaning)
          ..add('permissionBy', permissionBy)
          ..add('decisions', decisions)
          ..add('linkedLogs', linkedLogs))
        .toString();
  }
}

class IncidentDtoBuilder implements Builder<IncidentDto, IncidentDtoBuilder> {
  _$IncidentDto? _$v;

  int? _objectId;
  int? get objectId => _$this._objectId;
  set objectId(int? objectId) => _$this._objectId = objectId;

  String? _commonInfo;
  String? get commonInfo => _$this._commonInfo;
  set commonInfo(String? commonInfo) => _$this._commonInfo = commonInfo;

  IncidentStatus? _status;
  IncidentStatus? get status => _$this._status;
  set status(IncidentStatus? status) => _$this._status = status;

  IncidentMeaning? _meaning;
  IncidentMeaning? get meaning => _$this._meaning;
  set meaning(IncidentMeaning? meaning) => _$this._meaning = meaning;

  Permission? _permissionBy;
  Permission? get permissionBy => _$this._permissionBy;
  set permissionBy(Permission? permissionBy) =>
      _$this._permissionBy = permissionBy;

  ListBuilder<IncidentDecision>? _decisions;
  ListBuilder<IncidentDecision> get decisions =>
      _$this._decisions ??= ListBuilder<IncidentDecision>();
  set decisions(ListBuilder<IncidentDecision>? decisions) =>
      _$this._decisions = decisions;

  ListBuilder<IncidentLogDto>? _linkedLogs;
  ListBuilder<IncidentLogDto> get linkedLogs =>
      _$this._linkedLogs ??= ListBuilder<IncidentLogDto>();
  set linkedLogs(ListBuilder<IncidentLogDto>? linkedLogs) =>
      _$this._linkedLogs = linkedLogs;

  IncidentDtoBuilder() {
    IncidentDto._defaults(this);
  }

  IncidentDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _objectId = $v.objectId;
      _commonInfo = $v.commonInfo;
      _status = $v.status;
      _meaning = $v.meaning;
      _permissionBy = $v.permissionBy;
      _decisions = $v.decisions?.toBuilder();
      _linkedLogs = $v.linkedLogs?.toBuilder();
      _$v = null;
    }
    return this;
  }

  @override
  void replace(IncidentDto other) {
    _$v = other as _$IncidentDto;
  }

  @override
  void update(void Function(IncidentDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  IncidentDto build() => _build();

  _$IncidentDto _build() {
    _$IncidentDto _$result;
    try {
      _$result =
          _$v ??
          _$IncidentDto._(
            objectId: BuiltValueNullFieldError.checkNotNull(
              objectId,
              r'IncidentDto',
              'objectId',
            ),
            commonInfo: commonInfo,
            status: BuiltValueNullFieldError.checkNotNull(
              status,
              r'IncidentDto',
              'status',
            ),
            meaning: BuiltValueNullFieldError.checkNotNull(
              meaning,
              r'IncidentDto',
              'meaning',
            ),
            permissionBy: BuiltValueNullFieldError.checkNotNull(
              permissionBy,
              r'IncidentDto',
              'permissionBy',
            ),
            decisions: _decisions?.build(),
            linkedLogs: _linkedLogs?.build(),
          );
    } catch (_) {
      late String _$failedField;
      try {
        _$failedField = 'decisions';
        _decisions?.build();
        _$failedField = 'linkedLogs';
        _linkedLogs?.build();
      } catch (e) {
        throw BuiltValueNestedFieldError(
          r'IncidentDto',
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
