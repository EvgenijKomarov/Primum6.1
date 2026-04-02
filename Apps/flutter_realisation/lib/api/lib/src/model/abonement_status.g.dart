// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'abonement_status.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const AbonementStatus _$number0 = const AbonementStatus._('number0');
const AbonementStatus _$number1 = const AbonementStatus._('number1');
const AbonementStatus _$number2 = const AbonementStatus._('number2');

AbonementStatus _$valueOf(String name) {
  switch (name) {
    case 'number0':
      return _$number0;
    case 'number1':
      return _$number1;
    case 'number2':
      return _$number2;
    default:
      throw ArgumentError(name);
  }
}

final BuiltSet<AbonementStatus> _$values = BuiltSet<AbonementStatus>(
  const <AbonementStatus>[_$number0, _$number1, _$number2],
);

class _$AbonementStatusMeta {
  const _$AbonementStatusMeta();
  AbonementStatus get number0 => _$number0;
  AbonementStatus get number1 => _$number1;
  AbonementStatus get number2 => _$number2;
  AbonementStatus valueOf(String name) => _$valueOf(name);
  BuiltSet<AbonementStatus> get values => _$values;
}

mixin _$AbonementStatusMixin {
  // ignore: non_constant_identifier_names
  _$AbonementStatusMeta get AbonementStatus => const _$AbonementStatusMeta();
}

Serializer<AbonementStatus> _$abonementStatusSerializer =
    _$AbonementStatusSerializer();

class _$AbonementStatusSerializer
    implements PrimitiveSerializer<AbonementStatus> {
  static const Map<String, Object> _toWire = const <String, Object>{
    'number0': 0,
    'number1': 1,
    'number2': 2,
  };
  static const Map<Object, String> _fromWire = const <Object, String>{
    0: 'number0',
    1: 'number1',
    2: 'number2',
  };

  @override
  final Iterable<Type> types = const <Type>[AbonementStatus];
  @override
  final String wireName = 'AbonementStatus';

  @override
  Object serialize(
    Serializers serializers,
    AbonementStatus object, {
    FullType specifiedType = FullType.unspecified,
  }) => _toWire[object.name] ?? object.name;

  @override
  AbonementStatus deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) => AbonementStatus.valueOf(
    _fromWire[serialized] ?? (serialized is String ? serialized : ''),
  );
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
