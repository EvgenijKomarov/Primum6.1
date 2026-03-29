//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'course_theme_dto.g.dart';

/// CourseThemeDto
///
/// Properties:
/// * [id] 
/// * [themeName] 
/// * [isActive] 
@BuiltValue()
abstract class CourseThemeDto implements Built<CourseThemeDto, CourseThemeDtoBuilder> {
  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'themeName')
  String? get themeName;

  @BuiltValueField(wireName: r'isActive')
  bool get isActive;

  CourseThemeDto._();

  factory CourseThemeDto([void updates(CourseThemeDtoBuilder b)]) = _$CourseThemeDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CourseThemeDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CourseThemeDto> get serializer => _$CourseThemeDtoSerializer();
}

class _$CourseThemeDtoSerializer implements PrimitiveSerializer<CourseThemeDto> {
  @override
  final Iterable<Type> types = const [CourseThemeDto, _$CourseThemeDto];

  @override
  final String wireName = r'CourseThemeDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CourseThemeDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
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
    CourseThemeDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CourseThemeDtoBuilder result,
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
  CourseThemeDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CourseThemeDtoBuilder();
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

