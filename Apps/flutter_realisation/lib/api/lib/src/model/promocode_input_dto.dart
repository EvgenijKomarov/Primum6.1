//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'promocode_input_dto.g.dart';

/// PromocodeInputDto
///
/// Properties:
/// * [code] 
/// * [coinsPrice] 
/// * [title] 
/// * [description] 
@BuiltValue()
abstract class PromocodeInputDto implements Built<PromocodeInputDto, PromocodeInputDtoBuilder> {
  @BuiltValueField(wireName: r'code')
  String? get code;

  @BuiltValueField(wireName: r'coinsPrice')
  int get coinsPrice;

  @BuiltValueField(wireName: r'title')
  String? get title;

  @BuiltValueField(wireName: r'description')
  String? get description;

  PromocodeInputDto._();

  factory PromocodeInputDto([void updates(PromocodeInputDtoBuilder b)]) = _$PromocodeInputDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(PromocodeInputDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<PromocodeInputDto> get serializer => _$PromocodeInputDtoSerializer();
}

class _$PromocodeInputDtoSerializer implements PrimitiveSerializer<PromocodeInputDto> {
  @override
  final Iterable<Type> types = const [PromocodeInputDto, _$PromocodeInputDto];

  @override
  final String wireName = r'PromocodeInputDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    PromocodeInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'code';
    yield object.code == null ? null : serializers.serialize(
      object.code,
      specifiedType: const FullType.nullable(String),
    );
    yield r'coinsPrice';
    yield serializers.serialize(
      object.coinsPrice,
      specifiedType: const FullType(int),
    );
    yield r'title';
    yield object.title == null ? null : serializers.serialize(
      object.title,
      specifiedType: const FullType.nullable(String),
    );
    yield r'description';
    yield object.description == null ? null : serializers.serialize(
      object.description,
      specifiedType: const FullType.nullable(String),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    PromocodeInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required PromocodeInputDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'code':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.code = valueDes;
          break;
        case r'coinsPrice':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.coinsPrice = valueDes;
          break;
        case r'title':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.title = valueDes;
          break;
        case r'description':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.description = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  PromocodeInputDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = PromocodeInputDtoBuilder();
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

