// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_decision_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$IncidentDecisionInputDto extends IncidentDecisionInputDto {
  @override
  final int? objectId;
  @override
  final IncidentMeaning? meaning;
  @override
  final IncidentDecision? decision;
  @override
  final String? decisionExplanation;

  factory _$IncidentDecisionInputDto(
          [void Function(IncidentDecisionInputDtoBuilder)? updates]) =>
      (IncidentDecisionInputDtoBuilder()..update(updates))._build();

  _$IncidentDecisionInputDto._(
      {this.objectId, this.meaning, this.decision, this.decisionExplanation})
      : super._();
  @override
  IncidentDecisionInputDto rebuild(
          void Function(IncidentDecisionInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  IncidentDecisionInputDtoBuilder toBuilder() =>
      IncidentDecisionInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is IncidentDecisionInputDto &&
        objectId == other.objectId &&
        meaning == other.meaning &&
        decision == other.decision &&
        decisionExplanation == other.decisionExplanation;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, objectId.hashCode);
    _$hash = $jc(_$hash, meaning.hashCode);
    _$hash = $jc(_$hash, decision.hashCode);
    _$hash = $jc(_$hash, decisionExplanation.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'IncidentDecisionInputDto')
          ..add('objectId', objectId)
          ..add('meaning', meaning)
          ..add('decision', decision)
          ..add('decisionExplanation', decisionExplanation))
        .toString();
  }
}

class IncidentDecisionInputDtoBuilder
    implements
        Builder<IncidentDecisionInputDto, IncidentDecisionInputDtoBuilder> {
  _$IncidentDecisionInputDto? _$v;

  int? _objectId;
  int? get objectId => _$this._objectId;
  set objectId(int? objectId) => _$this._objectId = objectId;

  IncidentMeaning? _meaning;
  IncidentMeaning? get meaning => _$this._meaning;
  set meaning(IncidentMeaning? meaning) => _$this._meaning = meaning;

  IncidentDecision? _decision;
  IncidentDecision? get decision => _$this._decision;
  set decision(IncidentDecision? decision) => _$this._decision = decision;

  String? _decisionExplanation;
  String? get decisionExplanation => _$this._decisionExplanation;
  set decisionExplanation(String? decisionExplanation) =>
      _$this._decisionExplanation = decisionExplanation;

  IncidentDecisionInputDtoBuilder() {
    IncidentDecisionInputDto._defaults(this);
  }

  IncidentDecisionInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _objectId = $v.objectId;
      _meaning = $v.meaning;
      _decision = $v.decision;
      _decisionExplanation = $v.decisionExplanation;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(IncidentDecisionInputDto other) {
    _$v = other as _$IncidentDecisionInputDto;
  }

  @override
  void update(void Function(IncidentDecisionInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  IncidentDecisionInputDto build() => _build();

  _$IncidentDecisionInputDto _build() {
    final _$result = _$v ??
        _$IncidentDecisionInputDto._(
          objectId: objectId,
          meaning: meaning,
          decision: decision,
          decisionExplanation: decisionExplanation,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
