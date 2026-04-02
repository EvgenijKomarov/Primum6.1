// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'approve_status.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const ApproveStatus _$number0 = const ApproveStatus._('number0');
const ApproveStatus _$number1 = const ApproveStatus._('number1');
const ApproveStatus _$number2 = const ApproveStatus._('number2');
const ApproveStatus _$number3 = const ApproveStatus._('number3');

ApproveStatus _$valueOf(String name) {
  switch (name) {
    case 'number0':
      return _$number0;
    case 'number1':
      return _$number1;
    case 'number2':
      return _$number2;
    case 'number3':
      return _$number3;
    default:
      throw ArgumentError(name);
  }
}

final BuiltSet<ApproveStatus> _$values = BuiltSet<ApproveStatus>(
  const <ApproveStatus>[_$number0, _$number1, _$number2, _$number3],
);

class _$ApproveStatusMeta {
  const _$ApproveStatusMeta();
  ApproveStatus get number0 => _$number0;
  ApproveStatus get number1 => _$number1;
  ApproveStatus get number2 => _$number2;
  ApproveStatus get number3 => _$number3;
  ApproveStatus valueOf(String name) => _$valueOf(name);
  BuiltSet<ApproveStatus> get values => _$values;
}

mixin _$ApproveStatusMixin {
  // ignore: non_constant_identifier_names
  _$ApproveStatusMeta get ApproveStatus => const _$ApproveStatusMeta();
}

Serializer<ApproveStatus> _$approveStatusSerializer =
    _$ApproveStatusSerializer();

class _$ApproveStatusSerializer implements PrimitiveSerializer<ApproveStatus> {
  static const Map<String, Object> _toWire = const <String, Object>{
    'number0': 0,
    'number1': 1,
    'number2': 2,
    'number3': 3,
  };
  static const Map<Object, String> _fromWire = const <Object, String>{
    0: 'number0',
    1: 'number1',
    2: 'number2',
    3: 'number3',
  };

  @override
  final Iterable<Type> types = const <Type>[ApproveStatus];
  @override
  final String wireName = 'ApproveStatus';

  @override
  Object serialize(
    Serializers serializers,
    ApproveStatus object, {
    FullType specifiedType = FullType.unspecified,
  }) => _toWire[object.name] ?? object.name;

  @override
  ApproveStatus deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) => ApproveStatus.valueOf(
    _fromWire[serialized] ?? (serialized is String ? serialized : ''),
  );
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
