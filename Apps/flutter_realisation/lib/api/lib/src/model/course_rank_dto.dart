//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'course_rank_dto.g.dart';

/// CourseRankDto
///
/// Properties:
/// * [id] 
/// * [level] 
/// * [rank] 
/// * [requiredExperience] 
@BuiltValue()
abstract class CourseRankDto implements Built<CourseRankDto, CourseRankDtoBuilder> {
  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'level')
  int get level;

  @BuiltValueField(wireName: r'rank')
  String? get rank;

  @BuiltValueField(wireName: r'requiredExperience')
  int get requiredExperience;

  CourseRankDto._();

  factory CourseRankDto([void updates(CourseRankDtoBuilder b)]) = _$CourseRankDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CourseRankDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CourseRankDto> get serializer => _$CourseRankDtoSerializer();
}

class _$CourseRankDtoSerializer implements PrimitiveSerializer<CourseRankDto> {
  @override
  final Iterable<Type> types = const [CourseRankDto, _$CourseRankDto];

  @override
  final String wireName = r'CourseRankDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CourseRankDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
    yield r'level';
    yield serializers.serialize(
      object.level,
      specifiedType: const FullType(int),
    );
    yield r'rank';
    yield object.rank == null ? null : serializers.serialize(
      object.rank,
      specifiedType: const FullType.nullable(String),
    );
    yield r'requiredExperience';
    yield serializers.serialize(
      object.requiredExperience,
      specifiedType: const FullType(int),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    CourseRankDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CourseRankDtoBuilder result,
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
        case r'level':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.level = valueDes;
          break;
        case r'rank':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.rank = valueDes;
          break;
        case r'requiredExperience':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.requiredExperience = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  CourseRankDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CourseRankDtoBuilder();
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

