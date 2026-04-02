// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_decision.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const IncidentDecision _$number0 = const IncidentDecision._('number0');
const IncidentDecision _$number1 = const IncidentDecision._('number1');
const IncidentDecision _$number2 = const IncidentDecision._('number2');
const IncidentDecision _$number3 = const IncidentDecision._('number3');
const IncidentDecision _$number4 = const IncidentDecision._('number4');
const IncidentDecision _$number5 = const IncidentDecision._('number5');

IncidentDecision _$valueOf(String name) {
  switch (name) {
    case 'number0':
      return _$number0;
    case 'number1':
      return _$number1;
    case 'number2':
      return _$number2;
    case 'number3':
      return _$number3;
    case 'number4':
      return _$number4;
    case 'number5':
      return _$number5;
    default:
      throw ArgumentError(name);
  }
}

final BuiltSet<IncidentDecision> _$values = BuiltSet<IncidentDecision>(
  const <IncidentDecision>[
    _$number0,
    _$number1,
    _$number2,
    _$number3,
    _$number4,
    _$number5,
  ],
);

class _$IncidentDecisionMeta {
  const _$IncidentDecisionMeta();
  IncidentDecision get number0 => _$number0;
  IncidentDecision get number1 => _$number1;
  IncidentDecision get number2 => _$number2;
  IncidentDecision get number3 => _$number3;
  IncidentDecision get number4 => _$number4;
  IncidentDecision get number5 => _$number5;
  IncidentDecision valueOf(String name) => _$valueOf(name);
  BuiltSet<IncidentDecision> get values => _$values;
}

mixin _$IncidentDecisionMixin {
  // ignore: non_constant_identifier_names
  _$IncidentDecisionMeta get IncidentDecision => const _$IncidentDecisionMeta();
}

Serializer<IncidentDecision> _$incidentDecisionSerializer =
    _$IncidentDecisionSerializer();

class _$IncidentDecisionSerializer
    implements PrimitiveSerializer<IncidentDecision> {
  static const Map<String, Object> _toWire = const <String, Object>{
    'number0': 0,
    'number1': 1,
    'number2': 2,
    'number3': 3,
    'number4': 4,
    'number5': 5,
  };
  static const Map<Object, String> _fromWire = const <Object, String>{
    0: 'number0',
    1: 'number1',
    2: 'number2',
    3: 'number3',
    4: 'number4',
    5: 'number5',
  };

  @override
  final Iterable<Type> types = const <Type>[IncidentDecision];
  @override
  final String wireName = 'IncidentDecision';

  @override
  Object serialize(
    Serializers serializers,
    IncidentDecision object, {
    FullType specifiedType = FullType.unspecified,
  }) => _toWire[object.name] ?? object.name;

  @override
  IncidentDecision deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) => IncidentDecision.valueOf(
    _fromWire[serialized] ?? (serialized is String ? serialized : ''),
  );
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
