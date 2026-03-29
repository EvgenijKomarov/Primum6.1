//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/approve_status.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'course_dto.g.dart';

/// CourseDto
///
/// Properties:
/// * [id] 
/// * [name] 
/// * [teacherName] 
/// * [courseThemeName] 
/// * [about] 
/// * [courseThemeId] 
/// * [teacherId] 
/// * [price] 
/// * [maxLessons] 
/// * [freeLessons] 
/// * [teacherAbout] 
/// * [isActive] 
/// * [level] 
/// * [rank] 
/// * [approveStatus] 
@BuiltValue()
abstract class CourseDto implements Built<CourseDto, CourseDtoBuilder> {
  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'name')
  String? get name;

  @BuiltValueField(wireName: r'teacherName')
  String? get teacherName;

  @BuiltValueField(wireName: r'courseThemeName')
  String? get courseThemeName;

  @BuiltValueField(wireName: r'about')
  String? get about;

  @BuiltValueField(wireName: r'courseThemeId')
  int get courseThemeId;

  @BuiltValueField(wireName: r'teacherId')
  int get teacherId;

  @BuiltValueField(wireName: r'price')
  int get price;

  @BuiltValueField(wireName: r'maxLessons')
  int get maxLessons;

  @BuiltValueField(wireName: r'freeLessons')
  int get freeLessons;

  @BuiltValueField(wireName: r'teacherAbout')
  String? get teacherAbout;

  @BuiltValueField(wireName: r'isActive')
  bool get isActive;

  @BuiltValueField(wireName: r'level')
  int get level;

  @BuiltValueField(wireName: r'rank')
  String? get rank;

  @BuiltValueField(wireName: r'approveStatus')
  ApproveStatus get approveStatus;
  // enum approveStatusEnum {  0,  1,  2,  3,  };

  CourseDto._();

  factory CourseDto([void updates(CourseDtoBuilder b)]) = _$CourseDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CourseDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CourseDto> get serializer => _$CourseDtoSerializer();
}

class _$CourseDtoSerializer implements PrimitiveSerializer<CourseDto> {
  @override
  final Iterable<Type> types = const [CourseDto, _$CourseDto];

  @override
  final String wireName = r'CourseDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CourseDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
    yield r'name';
    yield object.name == null ? null : serializers.serialize(
      object.name,
      specifiedType: const FullType.nullable(String),
    );
    yield r'teacherName';
    yield object.teacherName == null ? null : serializers.serialize(
      object.teacherName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'courseThemeName';
    yield object.courseThemeName == null ? null : serializers.serialize(
      object.courseThemeName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'about';
    yield object.about == null ? null : serializers.serialize(
      object.about,
      specifiedType: const FullType.nullable(String),
    );
    yield r'courseThemeId';
    yield serializers.serialize(
      object.courseThemeId,
      specifiedType: const FullType(int),
    );
    yield r'teacherId';
    yield serializers.serialize(
      object.teacherId,
      specifiedType: const FullType(int),
    );
    yield r'price';
    yield serializers.serialize(
      object.price,
      specifiedType: const FullType(int),
    );
    yield r'maxLessons';
    yield serializers.serialize(
      object.maxLessons,
      specifiedType: const FullType(int),
    );
    yield r'freeLessons';
    yield serializers.serialize(
      object.freeLessons,
      specifiedType: const FullType(int),
    );
    yield r'teacherAbout';
    yield object.teacherAbout == null ? null : serializers.serialize(
      object.teacherAbout,
      specifiedType: const FullType.nullable(String),
    );
    yield r'isActive';
    yield serializers.serialize(
      object.isActive,
      specifiedType: const FullType(bool),
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
    yield r'approveStatus';
    yield serializers.serialize(
      object.approveStatus,
      specifiedType: const FullType(ApproveStatus),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    CourseDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CourseDtoBuilder result,
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
        case r'name':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.name = valueDes;
          break;
        case r'teacherName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.teacherName = valueDes;
          break;
        case r'courseThemeName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.courseThemeName = valueDes;
          break;
        case r'about':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.about = valueDes;
          break;
        case r'courseThemeId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.courseThemeId = valueDes;
          break;
        case r'teacherId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.teacherId = valueDes;
          break;
        case r'price':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.price = valueDes;
          break;
        case r'maxLessons':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.maxLessons = valueDes;
          break;
        case r'freeLessons':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.freeLessons = valueDes;
          break;
        case r'teacherAbout':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.teacherAbout = valueDes;
          break;
        case r'isActive':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(bool),
          ) as bool;
          result.isActive = valueDes;
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
        case r'approveStatus':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(ApproveStatus),
          ) as ApproveStatus;
          result.approveStatus = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  CourseDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CourseDtoBuilder();
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

