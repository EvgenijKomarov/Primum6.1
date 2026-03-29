//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/grading.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'grading_input_dto.g.dart';

/// GradingInputDto
///
/// Properties:
/// * [homeworkGrade] 
/// * [lessonActivityGrade] 
/// * [repetitionOfMaterialGrade] 
/// * [studyInitiativeGrade] 
@BuiltValue()
abstract class GradingInputDto implements Built<GradingInputDto, GradingInputDtoBuilder> {
  @BuiltValueField(wireName: r'homeworkGrade')
  Grading? get homeworkGrade;
  // enum homeworkGradeEnum {  0,  1,  2,  3,  4,  5,  };

  @BuiltValueField(wireName: r'lessonActivityGrade')
  Grading? get lessonActivityGrade;
  // enum lessonActivityGradeEnum {  0,  1,  2,  3,  4,  5,  };

  @BuiltValueField(wireName: r'repetitionOfMaterialGrade')
  Grading? get repetitionOfMaterialGrade;
  // enum repetitionOfMaterialGradeEnum {  0,  1,  2,  3,  4,  5,  };

  @BuiltValueField(wireName: r'studyInitiativeGrade')
  Grading? get studyInitiativeGrade;
  // enum studyInitiativeGradeEnum {  0,  1,  2,  3,  4,  5,  };

  GradingInputDto._();

  factory GradingInputDto([void updates(GradingInputDtoBuilder b)]) = _$GradingInputDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(GradingInputDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<GradingInputDto> get serializer => _$GradingInputDtoSerializer();
}

class _$GradingInputDtoSerializer implements PrimitiveSerializer<GradingInputDto> {
  @override
  final Iterable<Type> types = const [GradingInputDto, _$GradingInputDto];

  @override
  final String wireName = r'GradingInputDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    GradingInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.homeworkGrade != null) {
      yield r'homeworkGrade';
      yield serializers.serialize(
        object.homeworkGrade,
        specifiedType: const FullType(Grading),
      );
    }
    if (object.lessonActivityGrade != null) {
      yield r'lessonActivityGrade';
      yield serializers.serialize(
        object.lessonActivityGrade,
        specifiedType: const FullType(Grading),
      );
    }
    if (object.repetitionOfMaterialGrade != null) {
      yield r'repetitionOfMaterialGrade';
      yield serializers.serialize(
        object.repetitionOfMaterialGrade,
        specifiedType: const FullType(Grading),
      );
    }
    if (object.studyInitiativeGrade != null) {
      yield r'studyInitiativeGrade';
      yield serializers.serialize(
        object.studyInitiativeGrade,
        specifiedType: const FullType(Grading),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    GradingInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required GradingInputDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'homeworkGrade':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(Grading),
          ) as Grading;
          result.homeworkGrade = valueDes;
          break;
        case r'lessonActivityGrade':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(Grading),
          ) as Grading;
          result.lessonActivityGrade = valueDes;
          break;
        case r'repetitionOfMaterialGrade':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(Grading),
          ) as Grading;
          result.repetitionOfMaterialGrade = valueDes;
          break;
        case r'studyInitiativeGrade':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(Grading),
          ) as Grading;
          result.studyInitiativeGrade = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  GradingInputDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = GradingInputDtoBuilder();
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

