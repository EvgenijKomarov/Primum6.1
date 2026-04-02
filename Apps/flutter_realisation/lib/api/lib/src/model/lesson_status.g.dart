// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'lesson_status.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const LessonStatus _$number0 = const LessonStatus._('number0');
const LessonStatus _$number1 = const LessonStatus._('number1');
const LessonStatus _$number2 = const LessonStatus._('number2');
const LessonStatus _$number3 = const LessonStatus._('number3');
const LessonStatus _$number4 = const LessonStatus._('number4');

LessonStatus _$valueOf(String name) {
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

final BuiltSet<LessonStatus> _$values = BuiltSet<LessonStatus>(
  const <LessonStatus>[_$number0, _$number1, _$number2, _$number3, _$number4],
);

class _$LessonStatusMeta {
  const _$LessonStatusMeta();
  LessonStatus get number0 => _$number0;
  LessonStatus get number1 => _$number1;
  LessonStatus get number2 => _$number2;
  LessonStatus get number3 => _$number3;
  LessonStatus get number4 => _$number4;
  LessonStatus valueOf(String name) => _$valueOf(name);
  BuiltSet<LessonStatus> get values => _$values;
}

mixin _$LessonStatusMixin {
  // ignore: non_constant_identifier_names
  _$LessonStatusMeta get LessonStatus => const _$LessonStatusMeta();
}

Serializer<LessonStatus> _$lessonStatusSerializer = _$LessonStatusSerializer();

class _$LessonStatusSerializer implements PrimitiveSerializer<LessonStatus> {
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
  final Iterable<Type> types = const <Type>[LessonStatus];
  @override
  final String wireName = 'LessonStatus';

  @override
  Object serialize(
    Serializers serializers,
    LessonStatus object, {
    FullType specifiedType = FullType.unspecified,
  }) => _toWire[object.name] ?? object.name;

  @override
  LessonStatus deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) => LessonStatus.valueOf(
    _fromWire[serialized] ?? (serialized is String ? serialized : ''),
  );
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
