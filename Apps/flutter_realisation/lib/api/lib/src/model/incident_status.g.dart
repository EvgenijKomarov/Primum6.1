// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_status.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const IncidentStatus _$number0 = const IncidentStatus._('number0');
const IncidentStatus _$number1 = const IncidentStatus._('number1');
const IncidentStatus _$number2 = const IncidentStatus._('number2');
const IncidentStatus _$number3 = const IncidentStatus._('number3');
const IncidentStatus _$number4 = const IncidentStatus._('number4');

IncidentStatus _$valueOf(String name) {
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

final BuiltSet<IncidentStatus> _$values = BuiltSet<IncidentStatus>(
  const <IncidentStatus>[_$number0, _$number1, _$number2, _$number3, _$number4],
);

class _$IncidentStatusMeta {
  const _$IncidentStatusMeta();
  IncidentStatus get number0 => _$number0;
  IncidentStatus get number1 => _$number1;
  IncidentStatus get number2 => _$number2;
  IncidentStatus get number3 => _$number3;
  IncidentStatus get number4 => _$number4;
  IncidentStatus valueOf(String name) => _$valueOf(name);
  BuiltSet<IncidentStatus> get values => _$values;
}

mixin _$IncidentStatusMixin {
  // ignore: non_constant_identifier_names
  _$IncidentStatusMeta get IncidentStatus => const _$IncidentStatusMeta();
}

Serializer<IncidentStatus> _$incidentStatusSerializer =
    _$IncidentStatusSerializer();

class _$IncidentStatusSerializer
    implements PrimitiveSerializer<IncidentStatus> {
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
  final Iterable<Type> types = const <Type>[IncidentStatus];
  @override
  final String wireName = 'IncidentStatus';

  @override
  Object serialize(
    Serializers serializers,
    IncidentStatus object, {
    FullType specifiedType = FullType.unspecified,
  }) => _toWire[object.name] ?? object.name;

  @override
  IncidentStatus deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) => IncidentStatus.valueOf(
    _fromWire[serialized] ?? (serialized is String ? serialized : ''),
  );
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
