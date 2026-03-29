//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'user_dto_lite.g.dart';

/// UserDtoLite
///
/// Properties:
/// * [id] 
/// * [name] 
/// * [surname] 
/// * [patronymic] 
/// * [displayName] 
/// * [isApprovedStudent] 
/// * [isApprovedTeacher] 
/// * [isAdmin] 
/// * [isAvailable] 
@BuiltValue()
abstract class UserDtoLite implements Built<UserDtoLite, UserDtoLiteBuilder> {
  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'name')
  String? get name;

  @BuiltValueField(wireName: r'surname')
  String? get surname;

  @BuiltValueField(wireName: r'patronymic')
  String? get patronymic;

  @BuiltValueField(wireName: r'displayName')
  String? get displayName;

  @BuiltValueField(wireName: r'isApprovedStudent')
  bool? get isApprovedStudent;

  @BuiltValueField(wireName: r'isApprovedTeacher')
  bool? get isApprovedTeacher;

  @BuiltValueField(wireName: r'isAdmin')
  bool? get isAdmin;

  @BuiltValueField(wireName: r'isAvailable')
  bool get isAvailable;

  UserDtoLite._();

  factory UserDtoLite([void updates(UserDtoLiteBuilder b)]) = _$UserDtoLite;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(UserDtoLiteBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<UserDtoLite> get serializer => _$UserDtoLiteSerializer();
}

class _$UserDtoLiteSerializer implements PrimitiveSerializer<UserDtoLite> {
  @override
  final Iterable<Type> types = const [UserDtoLite, _$UserDtoLite];

  @override
  final String wireName = r'UserDtoLite';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    UserDtoLite object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
    yield r'name';
    yield object.name == null ? null : serializers.serialize(
      object.name,
      specifiedType: const FullType.nullable(String),
    );
    yield r'surname';
    yield object.surname == null ? null : serializers.serialize(
      object.surname,
      specifiedType: const FullType.nullable(String),
    );
    yield r'patronymic';
    yield object.patronymic == null ? null : serializers.serialize(
      object.patronymic,
      specifiedType: const FullType.nullable(String),
    );
    yield r'displayName';
    yield object.displayName == null ? null : serializers.serialize(
      object.displayName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'isApprovedStudent';
    yield object.isApprovedStudent == null ? null : serializers.serialize(
      object.isApprovedStudent,
      specifiedType: const FullType.nullable(bool),
    );
    yield r'isApprovedTeacher';
    yield object.isApprovedTeacher == null ? null : serializers.serialize(
      object.isApprovedTeacher,
      specifiedType: const FullType.nullable(bool),
    );
    yield r'isAdmin';
    yield object.isAdmin == null ? null : serializers.serialize(
      object.isAdmin,
      specifiedType: const FullType.nullable(bool),
    );
    yield r'isAvailable';
    yield serializers.serialize(
      object.isAvailable,
      specifiedType: const FullType(bool),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    UserDtoLite object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required UserDtoLiteBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'id':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.id = valueDes;
          break;
        case r'name':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.name = valueDes;
          break;
        case r'surname':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.surname = valueDes;
          break;
        case r'patronymic':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.patronymic = valueDes;
          break;
        case r'displayName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.displayName = valueDes;
          break;
        case r'isApprovedStudent':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(bool),
          ) as bool?;
          if (valueDes == null) continue;
          result.isApprovedStudent = valueDes;
          break;
        case r'isApprovedTeacher':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(bool),
          ) as bool?;
          if (valueDes == null) continue;
          result.isApprovedTeacher = valueDes;
          break;
        case r'isAdmin':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(bool),
          ) as bool?;
          if (valueDes == null) continue;
          result.isAdmin = valueDes;
          break;
        case r'isAvailable':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.isAvailable = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  UserDtoLite deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = UserDtoLiteBuilder();
    final serializedList = (serialized as Iterable<Object?>).toList();
    final unhandled = <Object?>[];
    _deserializeProperties(
      serializers,
      serialized,
      specifiedType: specifiedType,
      serializedList: serializedList,
      unhandled: unhandled,
      result: result,
    );
    return result.build();
  }
}

