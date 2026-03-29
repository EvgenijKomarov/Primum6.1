//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/incident_meaning.dart';
import 'package:my_api/src/model/incident_decision.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'incident_decision_input_dto.g.dart';

/// IncidentDecisionInputDto
///
/// Properties:
/// * [objectId] 
/// * [meaning] 
/// * [decision] 
/// * [decisionExplanation] 
@BuiltValue()
abstract class IncidentDecisionInputDto implements Built<IncidentDecisionInputDto, IncidentDecisionInputDtoBuilder> {
  @BuiltValueField(wireName: r'objectId')
  int? get objectId;

  @BuiltValueField(wireName: r'meaning')
  IncidentMeaning? get meaning;
  // enum meaningEnum {  0,  1,  2,  3,  4,  };

  @BuiltValueField(wireName: r'decision')
  IncidentDecision? get decision;
  // enum decisionEnum {  0,  1,  2,  3,  4,  5,  };

  @BuiltValueField(wireName: r'decisionExplanation')
  String? get decisionExplanation;

  IncidentDecisionInputDto._();

  factory IncidentDecisionInputDto([void updates(IncidentDecisionInputDtoBuilder b)]) = _$IncidentDecisionInputDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(IncidentDecisionInputDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<IncidentDecisionInputDto> get serializer => _$IncidentDecisionInputDtoSerializer();
}

class _$IncidentDecisionInputDtoSerializer implements PrimitiveSerializer<IncidentDecisionInputDto> {
  @override
  final Iterable<Type> types = const [IncidentDecisionInputDto, _$IncidentDecisionInputDto];

  @override
  final String wireName = r'IncidentDecisionInputDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    IncidentDecisionInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.objectId != null) {
      yield r'objectId';
      yield serializers.serialize(
        object.objectId,
        specifiedType: const FullType(int),
      );
    }
    if (object.meaning != null) {
      yield r'meaning';
      yield serializers.serialize(
        object.meaning,
        specifiedType: const FullType(IncidentMeaning),
      );
    }
    if (object.decision != null) {
      yield r'decision';
      yield serializers.serialize(
        object.decision,
        specifiedType: const FullType(IncidentDecision),
      );
    }
    if (object.decisionExplanation != null) {
      yield r'decisionExplanation';
      yield serializers.serialize(
        object.decisionExplanation,
        specifiedType: const FullType.nullable(String),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    IncidentDecisionInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required IncidentDecisionInputDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'objectId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.objectId = valueDes;
          break;
        case r'meaning':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(IncidentMeaning),
          ) as IncidentMeaning;
          result.meaning = valueDes;
          break;
        case r'decision':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(IncidentDecision),
          ) as IncidentDecision;
          result.decision = valueDes;
          break;
        case r'decisionExplanation':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.decisionExplanation = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  IncidentDecisionInputDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = IncidentDecisionInputDtoBuilder();
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

