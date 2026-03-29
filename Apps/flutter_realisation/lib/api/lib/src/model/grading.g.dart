// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'grading.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const Grading _$number0 = const Grading._('number0');
const Grading _$number1 = const Grading._('number1');
const Grading _$number2 = const Grading._('number2');
const Grading _$number3 = const Grading._('number3');
const Grading _$number4 = const Grading._('number4');
const Grading _$number5 = const Grading._('number5');

Grading _$valueOf(String name) {
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

final BuiltSet<Grading> _$values = BuiltSet<Grading>(const <Grading>[
  _$number0,
  _$number1,
  _$number2,
  _$number3,
  _$number4,
  _$number5,
]);

class _$GradingMeta {
  const _$GradingMeta();
  Grading get number0 => _$number0;
  Grading get number1 => _$number1;
  Grading get number2 => _$number2;
  Grading get number3 => _$number3;
  Grading get number4 => _$number4;
  Grading get number5 => _$number5;
  Grading valueOf(String name) => _$valueOf(name);
  BuiltSet<Grading> get values => _$values;
}

abstract class _$GradingMixin {
  // ignore: non_constant_identifier_names
  _$GradingMeta get Grading => const _$GradingMeta();
}

Serializer<Grading> _$gradingSerializer = _$GradingSerializer();

class _$GradingSerializer implements PrimitiveSerializer<Grading> {
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
  final Iterable<Type> types = const <Type>[Grading];
  @override
  final String wireName = 'Grading';

  @override
  Object serialize(Serializers serializers, Grading object,
          {FullType specifiedType = FullType.unspecified}) =>
      _toWire[object.name] ?? object.name;

  @override
  Grading deserialize(Serializers serializers, Object serialized,
          {FullType specifiedType = FullType.unspecified}) =>
      Grading.valueOf(
          _fromWire[serialized] ?? (serialized is String ? serialized : ''));
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
