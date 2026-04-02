// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'abonement_input_status.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const AbonementInputStatus _$number0 = const AbonementInputStatus._('number0');
const AbonementInputStatus _$number1 = const AbonementInputStatus._('number1');

AbonementInputStatus _$valueOf(String name) {
  switch (name) {
    case 'number0':
      return _$number0;
    case 'number1':
      return _$number1;
    default:
      throw ArgumentError(name);
  }
}

final BuiltSet<AbonementInputStatus> _$values = BuiltSet<AbonementInputStatus>(
  const <AbonementInputStatus>[_$number0, _$number1],
);

class _$AbonementInputStatusMeta {
  const _$AbonementInputStatusMeta();
  AbonementInputStatus get number0 => _$number0;
  AbonementInputStatus get number1 => _$number1;
  AbonementInputStatus valueOf(String name) => _$valueOf(name);
  BuiltSet<AbonementInputStatus> get values => _$values;
}

mixin _$AbonementInputStatusMixin {
  // ignore: non_constant_identifier_names
  _$AbonementInputStatusMeta get AbonementInputStatus =>
      const _$AbonementInputStatusMeta();
}

Serializer<AbonementInputStatus> _$abonementInputStatusSerializer =
    _$AbonementInputStatusSerializer();

class _$AbonementInputStatusSerializer
    implements PrimitiveSerializer<AbonementInputStatus> {
  static const Map<String, Object> _toWire = const <String, Object>{
    'number0': 0,
    'number1': 1,
  };
  static const Map<Object, String> _fromWire = const <Object, String>{
    0: 'number0',
    1: 'number1',
  };

  @override
  final Iterable<Type> types = const <Type>[AbonementInputStatus];
  @override
  final String wireName = 'AbonementInputStatus';

  @override
  Object serialize(
    Serializers serializers,
    AbonementInputStatus object, {
    FullType specifiedType = FullType.unspecified,
  }) => _toWire[object.name] ?? object.name;

  @override
  AbonementInputStatus deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) => AbonementInputStatus.valueOf(
    _fromWire[serialized] ?? (serialized is String ? serialized : ''),
  );
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
