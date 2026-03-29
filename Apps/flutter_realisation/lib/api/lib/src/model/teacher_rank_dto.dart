//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'teacher_rank_dto.g.dart';

/// TeacherRankDto
///
/// Properties:
/// * [id] 
/// * [level] 
/// * [rank] 
/// * [requiredExperience] 
/// * [earningMultiplier] 
@BuiltValue()
abstract class TeacherRankDto implements Built<TeacherRankDto, TeacherRankDtoBuilder> {
  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'level')
  int get level;

  @BuiltValueField(wireName: r'rank')
  String? get rank;

  @BuiltValueField(wireName: r'requiredExperience')
  int get requiredExperience;

  @BuiltValueField(wireName: r'earningMultiplier')
  double get earningMultiplier;

  TeacherRankDto._();

  factory TeacherRankDto([void updates(TeacherRankDtoBuilder b)]) = _$TeacherRankDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(TeacherRankDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<TeacherRankDto> get serializer => _$TeacherRankDtoSerializer();
}

class _$TeacherRankDtoSerializer implements PrimitiveSerializer<TeacherRankDto> {
  @override
  final Iterable<Type> types = const [TeacherRankDto, _$TeacherRankDto];

  @override
  final String wireName = r'TeacherRankDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    TeacherRankDto object, {
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
    yield r'earningMultiplier';
    yield serializers.serialize(
      object.earningMultiplier,
      specifiedType: const FullType(double),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    TeacherRankDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required TeacherRankDtoBuilder result,
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
        case r'earningMultiplier':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(double),
          ) as double;
          result.earningMultiplier = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  TeacherRankDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = TeacherRankDtoBuilder();
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

