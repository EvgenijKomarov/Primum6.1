//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/abonement_status.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'abonement_dto.g.dart';

/// AbonementDto
///
/// Properties:
/// * [studentDisplayName] 
/// * [studentId] 
/// * [teacherDisplayName] 
/// * [teacherId] 
/// * [courseName] 
/// * [courseId] 
/// * [courseThemeName] 
/// * [id] 
/// * [courseThemeId] 
/// * [pricePerLesson] 
/// * [rating] 
/// * [abonementStatus] 
@BuiltValue()
abstract class AbonementDto implements Built<AbonementDto, AbonementDtoBuilder> {
  @BuiltValueField(wireName: r'studentDisplayName')
  String? get studentDisplayName;

  @BuiltValueField(wireName: r'studentId')
  int get studentId;

  @BuiltValueField(wireName: r'teacherDisplayName')
  String? get teacherDisplayName;

  @BuiltValueField(wireName: r'teacherId')
  int get teacherId;

  @BuiltValueField(wireName: r'courseName')
  String? get courseName;

  @BuiltValueField(wireName: r'courseId')
  int? get courseId;

  @BuiltValueField(wireName: r'courseThemeName')
  String? get courseThemeName;

  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'courseThemeId')
  int get courseThemeId;

  @BuiltValueField(wireName: r'pricePerLesson')
  int get pricePerLesson;

  @BuiltValueField(wireName: r'rating')
  double? get rating;

  @BuiltValueField(wireName: r'abonementStatus')
  AbonementStatus get abonementStatus;
  // enum abonementStatusEnum {  0,  1,  2,  };

  AbonementDto._();

  factory AbonementDto([void updates(AbonementDtoBuilder b)]) = _$AbonementDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(AbonementDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<AbonementDto> get serializer => _$AbonementDtoSerializer();
}

class _$AbonementDtoSerializer implements PrimitiveSerializer<AbonementDto> {
  @override
  final Iterable<Type> types = const [AbonementDto, _$AbonementDto];

  @override
  final String wireName = r'AbonementDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    AbonementDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
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
    yield r'courseThemeName';
    yield object.courseThemeName == null ? null : serializers.serialize(
      object.courseThemeName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
    yield r'courseThemeId';
    yield serializers.serialize(
      object.courseThemeId,
      specifiedType: const FullType(int),
    );
    yield r'pricePerLesson';
    yield serializers.serialize(
      object.pricePerLesson,
      specifiedType: const FullType(int),
    );
    yield r'rating';
    yield object.rating == null ? null : serializers.serialize(
      object.rating,
      specifiedType: const FullType.nullable(double),
    );
    yield r'abonementStatus';
    yield serializers.serialize(
      object.abonementStatus,
      specifiedType: const FullType(AbonementStatus),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    AbonementDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required AbonementDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
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
        case r'courseThemeName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.courseThemeName = valueDes;
          break;
        case r'id':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.id = valueDes;
          break;
        case r'courseThemeId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.courseThemeId = valueDes;
          break;
        case r'pricePerLesson':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.pricePerLesson = valueDes;
          break;
        case r'rating':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(double),
          ) as double?;
          if (valueDes == null) continue;
          result.rating = valueDes;
          break;
        case r'abonementStatus':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(AbonementStatus),
          ) as AbonementStatus;
          result.abonementStatus = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  AbonementDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = AbonementDtoBuilder();
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

