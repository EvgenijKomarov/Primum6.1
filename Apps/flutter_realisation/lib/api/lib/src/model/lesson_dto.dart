//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/lesson_status.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'lesson_dto.g.dart';

/// LessonDto
///
/// Properties:
/// * [courseName] 
/// * [courseId] 
/// * [id] 
/// * [teacherDisplayName] 
/// * [teacherId] 
/// * [studentDisplayName] 
/// * [studentId] 
/// * [abonementId] 
/// * [price] 
/// * [lessonStatus] 
/// * [dateTime] 
/// * [lessonLink] 
/// * [grade] 
@BuiltValue()
abstract class LessonDto implements Built<LessonDto, LessonDtoBuilder> {
  @BuiltValueField(wireName: r'courseName')
  String? get courseName;

  @BuiltValueField(wireName: r'courseId')
  int get courseId;

  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'teacherDisplayName')
  String? get teacherDisplayName;

  @BuiltValueField(wireName: r'teacherId')
  int get teacherId;

  @BuiltValueField(wireName: r'studentDisplayName')
  String? get studentDisplayName;

  @BuiltValueField(wireName: r'studentId')
  int get studentId;

  @BuiltValueField(wireName: r'abonementId')
  int get abonementId;

  @BuiltValueField(wireName: r'price')
  int get price;

  @BuiltValueField(wireName: r'lessonStatus')
  LessonStatus get lessonStatus;
  // enum lessonStatusEnum {  0,  1,  2,  3,  4,  };

  @BuiltValueField(wireName: r'dateTime')
  DateTime get dateTime;

  @BuiltValueField(wireName: r'lessonLink')
  String? get lessonLink;

  @BuiltValueField(wireName: r'grade')
  double? get grade;

  LessonDto._();

  factory LessonDto([void updates(LessonDtoBuilder b)]) = _$LessonDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(LessonDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<LessonDto> get serializer => _$LessonDtoSerializer();
}

class _$LessonDtoSerializer implements PrimitiveSerializer<LessonDto> {
  @override
  final Iterable<Type> types = const [LessonDto, _$LessonDto];

  @override
  final String wireName = r'LessonDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    LessonDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
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
    yield r'id';
    yield serializers.serialize(
      object.id,
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
    yield r'studentDisplayName';
    yield object.studentDisplayName == null ? null : serializers.serialize(
      object.studentDisplayName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'studentId';
    yield serializers.serialize(
      object.studentId,
      specifiedType: const FullType(int),
    );
    yield r'abonementId';
    yield serializers.serialize(
      object.abonementId,
      specifiedType: const FullType(int),
    );
    yield r'price';
    yield serializers.serialize(
      object.price,
      specifiedType: const FullType(int),
    );
    yield r'lessonStatus';
    yield serializers.serialize(
      object.lessonStatus,
      specifiedType: const FullType(LessonStatus),
    );
    yield r'dateTime';
    yield serializers.serialize(
      object.dateTime,
      specifiedType: const FullType(DateTime),
    );
    yield r'lessonLink';
    yield object.lessonLink == null ? null : serializers.serialize(
      object.lessonLink,
      specifiedType: const FullType.nullable(String),
    );
    yield r'grade';
    yield object.grade == null ? null : serializers.serialize(
      object.grade,
      specifiedType: const FullType.nullable(double),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    LessonDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required LessonDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
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
        case r'id':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.id = valueDes;
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
        case r'studentDisplayName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.studentDisplayName = valueDes;
          break;
        case r'studentId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.studentId = valueDes;
          break;
        case r'abonementId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.abonementId = valueDes;
          break;
        case r'price':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.price = valueDes;
          break;
        case r'lessonStatus':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(LessonStatus),
          ) as LessonStatus;
          result.lessonStatus = valueDes;
          break;
        case r'dateTime':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.dateTime = valueDes;
          break;
        case r'lessonLink':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.lessonLink = valueDes;
          break;
        case r'grade':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(double),
          ) as double?;
          if (valueDes == null) continue;
          result.grade = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  LessonDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = LessonDtoBuilder();
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

