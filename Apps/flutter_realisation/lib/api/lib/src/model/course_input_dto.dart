//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'course_input_dto.g.dart';

/// CourseInputDto
///
/// Properties:
/// * [name] 
/// * [description] 
/// * [price] 
/// * [freeLessons] 
/// * [maxLessons] 
/// * [courseThemeId] 
@BuiltValue()
abstract class CourseInputDto implements Built<CourseInputDto, CourseInputDtoBuilder> {
  @BuiltValueField(wireName: r'name')
  String? get name;

  @BuiltValueField(wireName: r'description')
  String? get description;

  @BuiltValueField(wireName: r'price')
  int? get price;

  @BuiltValueField(wireName: r'freeLessons')
  int? get freeLessons;

  @BuiltValueField(wireName: r'maxLessons')
  int? get maxLessons;

  @BuiltValueField(wireName: r'courseThemeId')
  int? get courseThemeId;

  CourseInputDto._();

  factory CourseInputDto([void updates(CourseInputDtoBuilder b)]) = _$CourseInputDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CourseInputDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CourseInputDto> get serializer => _$CourseInputDtoSerializer();
}

class _$CourseInputDtoSerializer implements PrimitiveSerializer<CourseInputDto> {
  @override
  final Iterable<Type> types = const [CourseInputDto, _$CourseInputDto];

  @override
  final String wireName = r'CourseInputDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CourseInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.name != null) {
      yield r'name';
      yield serializers.serialize(
        object.name,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.description != null) {
      yield r'description';
      yield serializers.serialize(
        object.description,
        specifiedType: const FullType.nullable(String),
      );
    }
    if (object.price != null) {
      yield r'price';
      yield serializers.serialize(
        object.price,
        specifiedType: const FullType(int),
      );
    }
    if (object.freeLessons != null) {
      yield r'freeLessons';
      yield serializers.serialize(
        object.freeLessons,
        specifiedType: const FullType(int),
      );
    }
    if (object.maxLessons != null) {
      yield r'maxLessons';
      yield serializers.serialize(
        object.maxLessons,
        specifiedType: const FullType(int),
      );
    }
    if (object.courseThemeId != null) {
      yield r'courseThemeId';
      yield serializers.serialize(
        object.courseThemeId,
        specifiedType: const FullType(int),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    CourseInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CourseInputDtoBuilder result,
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
        case r'description':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.description = valueDes;
          break;
        case r'price':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.price = valueDes;
          break;
        case r'freeLessons':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.freeLessons = valueDes;
          break;
        case r'maxLessons':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.maxLessons = valueDes;
          break;
        case r'courseThemeId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.courseThemeId = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  CourseInputDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CourseInputDtoBuilder();
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

