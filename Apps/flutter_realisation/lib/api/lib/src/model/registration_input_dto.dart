//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'registration_input_dto.g.dart';

/// RegistrationInputDto
///
/// Properties:
/// * [name] 
/// * [surname] 
/// * [patronymic] 
/// * [mailAdress] 
/// * [password] 
@BuiltValue()
abstract class RegistrationInputDto implements Built<RegistrationInputDto, RegistrationInputDtoBuilder> {
  @BuiltValueField(wireName: r'name')
  String? get name;

  @BuiltValueField(wireName: r'surname')
  String? get surname;

  @BuiltValueField(wireName: r'patronymic')
  String? get patronymic;

  @BuiltValueField(wireName: r'mailAdress')
  String? get mailAdress;

  @BuiltValueField(wireName: r'password')
  String? get password;

  RegistrationInputDto._();

  factory RegistrationInputDto([void updates(RegistrationInputDtoBuilder b)]) = _$RegistrationInputDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(RegistrationInputDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<RegistrationInputDto> get serializer => _$RegistrationInputDtoSerializer();
}

class _$RegistrationInputDtoSerializer implements PrimitiveSerializer<RegistrationInputDto> {
  @override
  final Iterable<Type> types = const [RegistrationInputDto, _$RegistrationInputDto];

  @override
  final String wireName = r'RegistrationInputDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    RegistrationInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.name != null) {
      yield r'name';
      yield serializers.serialize(
        object.name,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.surname != null) {
      yield r'surname';
      yield serializers.serialize(
        object.surname,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.patronymic != null) {
      yield r'patronymic';
      yield serializers.serialize(
        object.patronymic,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.mailAdress != null) {
      yield r'mailAdress';
      yield serializers.serialize(
        object.mailAdress,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.password != null) {
      yield r'password';
      yield serializers.serialize(
        object.password,
        specifiedType: const FullType.nullable(String),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    RegistrationInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required RegistrationInputDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
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
        case r'mailAdress':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.mailAdress = valueDes;
          break;
        case r'password':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.password = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  RegistrationInputDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = RegistrationInputDtoBuilder();
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

