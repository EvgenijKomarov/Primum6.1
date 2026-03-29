// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_meaning.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const IncidentMeaning _$number0 = const IncidentMeaning._('number0');
const IncidentMeaning _$number1 = const IncidentMeaning._('number1');
const IncidentMeaning _$number2 = const IncidentMeaning._('number2');
const IncidentMeaning _$number3 = const IncidentMeaning._('number3');
const IncidentMeaning _$number4 = const IncidentMeaning._('number4');

IncidentMeaning _$valueOf(String name) {
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
    default:
      throw ArgumentError(name);
  }
}

final BuiltSet<IncidentMeaning> _$values =
    BuiltSet<IncidentMeaning>(const <IncidentMeaning>[
  _$number0,
  _$number1,
  _$number2,
  _$number3,
  _$number4,
]);

class _$IncidentMeaningMeta {
  const _$IncidentMeaningMeta();
  IncidentMeaning get number0 => _$number0;
  IncidentMeaning get number1 => _$number1;
  IncidentMeaning get number2 => _$number2;
  IncidentMeaning get number3 => _$number3;
  IncidentMeaning get number4 => _$number4;
  IncidentMeaning valueOf(String name) => _$valueOf(name);
  BuiltSet<IncidentMeaning> get values => _$values;
}

abstract class _$IncidentMeaningMixin {
  // ignore: non_constant_identifier_names
  _$IncidentMeaningMeta get IncidentMeaning => const _$IncidentMeaningMeta();
}

Serializer<IncidentMeaning> _$incidentMeaningSerializer =
    _$IncidentMeaningSerializer();

class _$IncidentMeaningSerializer
    implements PrimitiveSerializer<IncidentMeaning> {
  static const Map<String, Object> _toWire = const <String, Object>{
    'number0': 0,
    'number1': 1,
    'number2': 2,
    'number3': 3,
    'number4': 4,
  };
  static const Map<Object, String> _fromWire = const <Object, String>{
    0: 'number0',
    1: 'number1',
    2: 'number2',
    3: 'number3',
    4: 'number4',
  };

  @override
  final Iterable<Type> types = const <Type>[IncidentMeaning];
  @override
  final String wireName = 'IncidentMeaning';

  @override
  Object serialize(Serializers serializers, IncidentMeaning object,
          {FullType specifiedType = FullType.unspecified}) =>
      _toWire[object.name] ?? object.name;

  @override
  IncidentMeaning deserialize(Serializers serializers, Object serialized,
          {FullType specifiedType = FullType.unspecified}) =>
      IncidentMeaning.valueOf(
          _fromWire[serialized] ?? (serialized is String ? serialized : ''));
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
