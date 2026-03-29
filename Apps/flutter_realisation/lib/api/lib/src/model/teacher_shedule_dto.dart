//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/day_of_week.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'teacher_shedule_dto.g.dart';

/// TeacherSheduleDto
///
/// Properties:
/// * [id] 
/// * [dayOfWeek] 
/// * [time] 
/// * [isAvailable] 
/// * [studentName] 
/// * [studentId] 
/// * [courseName] 
/// * [courseId] 
@BuiltValue()
abstract class TeacherSheduleDto implements Built<TeacherSheduleDto, TeacherSheduleDtoBuilder> {
  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'dayOfWeek')
  DayOfWeek get dayOfWeek;
  // enum dayOfWeekEnum {  0,  1,  2,  3,  4,  5,  6,  };

  @BuiltValueField(wireName: r'time')
  int get time;

  @BuiltValueField(wireName: r'isAvailable')
  bool get isAvailable;

  @BuiltValueField(wireName: r'studentName')
  String? get studentName;

  @BuiltValueField(wireName: r'studentId')
  int? get studentId;

  @BuiltValueField(wireName: r'courseName')
  String? get courseName;

  @BuiltValueField(wireName: r'courseId')
  int? get courseId;

  TeacherSheduleDto._();

  factory TeacherSheduleDto([void updates(TeacherSheduleDtoBuilder b)]) = _$TeacherSheduleDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(TeacherSheduleDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<TeacherSheduleDto> get serializer => _$TeacherSheduleDtoSerializer();
}

class _$TeacherSheduleDtoSerializer implements PrimitiveSerializer<TeacherSheduleDto> {
  @override
  final Iterable<Type> types = const [TeacherSheduleDto, _$TeacherSheduleDto];

  @override
  final String wireName = r'TeacherSheduleDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    TeacherSheduleDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
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
    yield r'isAvailable';
    yield serializers.serialize(
      object.isAvailable,
      specifiedType: const FullType(bool),
    );
    yield r'studentName';
    yield object.studentName == null ? null : serializers.serialize(
      object.studentName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'studentId';
    yield object.studentId == null ? null : serializers.serialize(
      object.studentId,
      specifiedType: const FullType.nullable(int),
    );
    yield r'courseName';
    yield object.courseName == null ? null : serializers.serialize(
      object.courseName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'courseId';
    yield object.courseId == null ? null : serializers.serialize(
      object.courseId,
      specifiedType: const FullType.nullable(int),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    TeacherSheduleDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required TeacherSheduleDtoBuilder result,
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
        case r'isAvailable':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.isAvailable = valueDes;
          break;
        case r'studentName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.studentName = valueDes;
          break;
        case r'studentId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(int),
          ) as int?;
          if (valueDes == null) continue;
          result.studentId = valueDes;
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
            specifiedType: const FullType.nullable(int),
          ) as int?;
          if (valueDes == null) continue;
          result.courseId = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  TeacherSheduleDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = TeacherSheduleDtoBuilder();
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

