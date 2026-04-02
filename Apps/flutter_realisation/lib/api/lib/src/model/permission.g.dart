// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'permission.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

const Permission _$number0 = const Permission._('number0');
const Permission _$number1 = const Permission._('number1');
const Permission _$number2 = const Permission._('number2');
const Permission _$number3 = const Permission._('number3');
const Permission _$number4 = const Permission._('number4');
const Permission _$number5 = const Permission._('number5');
const Permission _$number6 = const Permission._('number6');
const Permission _$number7 = const Permission._('number7');
const Permission _$number8 = const Permission._('number8');
const Permission _$number10 = const Permission._('number10');
const Permission _$number11 = const Permission._('number11');
const Permission _$number12 = const Permission._('number12');
const Permission _$number13 = const Permission._('number13');
const Permission _$number14 = const Permission._('number14');
const Permission _$number15 = const Permission._('number15');
const Permission _$number16 = const Permission._('number16');
const Permission _$number17 = const Permission._('number17');
const Permission _$number18 = const Permission._('number18');

Permission _$valueOf(String name) {
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
    case 'number6':
      return _$number6;
    case 'number7':
      return _$number7;
    case 'number8':
      return _$number8;
    case 'number10':
      return _$number10;
    case 'number11':
      return _$number11;
    case 'number12':
      return _$number12;
    case 'number13':
      return _$number13;
    case 'number14':
      return _$number14;
    case 'number15':
      return _$number15;
    case 'number16':
      return _$number16;
    case 'number17':
      return _$number17;
    case 'number18':
      return _$number18;
    default:
      throw ArgumentError(name);
  }
}

final BuiltSet<Permission> _$values = BuiltSet<Permission>(const <Permission>[
  _$number0,
  _$number1,
  _$number2,
  _$number3,
  _$number4,
  _$number5,
  _$number6,
  _$number7,
  _$number8,
  _$number10,
  _$number11,
  _$number12,
  _$number13,
  _$number14,
  _$number15,
  _$number16,
  _$number17,
  _$number18,
]);

class _$PermissionMeta {
  const _$PermissionMeta();
  Permission get number0 => _$number0;
  Permission get number1 => _$number1;
  Permission get number2 => _$number2;
  Permission get number3 => _$number3;
  Permission get number4 => _$number4;
  Permission get number5 => _$number5;
  Permission get number6 => _$number6;
  Permission get number7 => _$number7;
  Permission get number8 => _$number8;
  Permission get number10 => _$number10;
  Permission get number11 => _$number11;
  Permission get number12 => _$number12;
  Permission get number13 => _$number13;
  Permission get number14 => _$number14;
  Permission get number15 => _$number15;
  Permission get number16 => _$number16;
  Permission get number17 => _$number17;
  Permission get number18 => _$number18;
  Permission valueOf(String name) => _$valueOf(name);
  BuiltSet<Permission> get values => _$values;
}

mixin _$PermissionMixin {
  // ignore: non_constant_identifier_names
  _$PermissionMeta get Permission => const _$PermissionMeta();
}

Serializer<Permission> _$permissionSerializer = _$PermissionSerializer();

class _$PermissionSerializer implements PrimitiveSerializer<Permission> {
  static const Map<String, Object> _toWire = const <String, Object>{
    'number0': 0,
    'number1': 1,
    'number2': 2,
    'number3': 3,
    'number4': 4,
    'number5': 5,
    'number6': 6,
    'number7': 7,
    'number8': 8,
    'number10': 10,
    'number11': 11,
    'number12': 12,
    'number13': 13,
    'number14': 14,
    'number15': 15,
    'number16': 16,
    'number17': 17,
    'number18': 18,
  };
  static const Map<Object, String> _fromWire = const <Object, String>{
    0: 'number0',
    1: 'number1',
    2: 'number2',
    3: 'number3',
    4: 'number4',
    5: 'number5',
    6: 'number6',
    7: 'number7',
    8: 'number8',
    10: 'number10',
    11: 'number11',
    12: 'number12',
    13: 'number13',
    14: 'number14',
    15: 'number15',
    16: 'number16',
    17: 'number17',
    18: 'number18',
  };

  @override
  final Iterable<Type> types = const <Type>[Permission];
  @override
  final String wireName = 'Permission';

  @override
  Object serialize(
    Serializers serializers,
    Permission object, {
    FullType specifiedType = FullType.unspecified,
  }) => _toWire[object.name] ?? object.name;

  @override
  Permission deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) => Permission.valueOf(
    _fromWire[serialized] ?? (serialized is String ? serialized : ''),
  );
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
