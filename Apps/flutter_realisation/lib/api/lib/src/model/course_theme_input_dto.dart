//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'course_theme_input_dto.g.dart';

/// CourseThemeInputDto
///
/// Properties:
/// * [themeName] 
/// * [isActive] 
@BuiltValue()
abstract class CourseThemeInputDto implements Built<CourseThemeInputDto, CourseThemeInputDtoBuilder> {
  @BuiltValueField(wireName: r'themeName')
  String? get themeName;

  @BuiltValueField(wireName: r'isActive')
  bool get isActive;

  CourseThemeInputDto._();

  factory CourseThemeInputDto([void updates(CourseThemeInputDtoBuilder b)]) = _$CourseThemeInputDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CourseThemeInputDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CourseThemeInputDto> get serializer => _$CourseThemeInputDtoSerializer();
}

class _$CourseThemeInputDtoSerializer implements PrimitiveSerializer<CourseThemeInputDto> {
  @override
  final Iterable<Type> types = const [CourseThemeInputDto, _$CourseThemeInputDto];

  @override
  final String wireName = r'CourseThemeInputDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CourseThemeInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'themeName';
    yield object.themeName == null ? null : serializers.serialize(
      object.themeName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'isActive';
    yield serializers.serialize(
      object.isActive,
      specifiedType: const FullType(bool),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    CourseThemeInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CourseThemeInputDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'themeName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.themeName = valueDes;
          break;
        case r'isActive':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.isActive = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  CourseThemeInputDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CourseThemeInputDtoBuilder();
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

