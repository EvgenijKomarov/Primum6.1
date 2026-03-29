//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/day_of_week.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'abonement_shedule_dto.g.dart';

/// AbonementSheduleDto
///
/// Properties:
/// * [dayOfWeek] 
/// * [time] 
/// * [courseName] 
/// * [courseId] 
/// * [teacherDisplayName] 
/// * [teacherId] 
/// * [id] 
@BuiltValue()
abstract class AbonementSheduleDto implements Built<AbonementSheduleDto, AbonementSheduleDtoBuilder> {
  @BuiltValueField(wireName: r'dayOfWeek')
  DayOfWeek get dayOfWeek;
  // enum dayOfWeekEnum {  0,  1,  2,  3,  4,  5,  6,  };

  @BuiltValueField(wireName: r'time')
  int get time;

  @BuiltValueField(wireName: r'courseName')
  String? get courseName;

  @BuiltValueField(wireName: r'courseId')
  int get courseId;

  @BuiltValueField(wireName: r'teacherDisplayName')
  String? get teacherDisplayName;

  @BuiltValueField(wireName: r'teacherId')
  int get teacherId;

  @BuiltValueField(wireName: r'id')
  int get id;

  AbonementSheduleDto._();

  factory AbonementSheduleDto([void updates(AbonementSheduleDtoBuilder b)]) = _$AbonementSheduleDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(AbonementSheduleDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<AbonementSheduleDto> get serializer => _$AbonementSheduleDtoSerializer();
}

class _$AbonementSheduleDtoSerializer implements PrimitiveSerializer<AbonementSheduleDto> {
  @override
  final Iterable<Type> types = const [AbonementSheduleDto, _$AbonementSheduleDto];

  @override
  final String wireName = r'AbonementSheduleDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    AbonementSheduleDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'dayOfWeek';
    yield serializers.serialize(
      object.dayOfWeek,
      specifiedType: const FullType(DayOfWeek),
    );
    yield r'time';
    yield serializers.serialize(
      object.time,
      specifiedType: const FullType(int),
    );
    yield r'courseName';
    yield object.courseName == null ? null : serializers.serialize(
      object.courseName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'courseId';
    yield serializers.serialize(
      object.courseId,
      specifiedType: const FullType(int),
    );
    yield r'teacherDisplayName';
    yield object.teacherDisplayName == null ? null : serializers.serialize(
      object.teacherDisplayName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'teacherId';
    yield serializers.serialize(
      object.teacherId,
      specifiedType: const FullType(int),
    );
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    AbonementSheduleDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required AbonementSheduleDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'dayOfWeek':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DayOfWeek),
          ) as DayOfWeek;
          result.dayOfWeek = valueDes;
          break;
        case r'time':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.time = valueDes;
          break;
        case r'courseName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.courseName = valueDes;
          break;
        case r'courseId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.courseId = valueDes;
          break;
        case r'teacherDisplayName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.teacherDisplayName = valueDes;
          break;
        case r'teacherId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.teacherId = valueDes;
          break;
        case r'id':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.id = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  AbonementSheduleDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = AbonementSheduleDtoBuilder();
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

