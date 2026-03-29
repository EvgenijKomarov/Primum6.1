//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_collection/built_collection.dart';
import 'package:my_api/src/model/permission.dart';
import 'package:my_api/src/model/incident_meaning.dart';
import 'package:my_api/src/model/incident_log_dto.dart';
import 'package:my_api/src/model/incident_decision.dart';
import 'package:my_api/src/model/incident_status.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'incident_dto.g.dart';

/// IncidentDto
///
/// Properties:
/// * [objectId] 
/// * [commonInfo] 
/// * [status] 
/// * [meaning] 
/// * [permissionBy] 
/// * [decisions] 
/// * [linkedLogs] 
@BuiltValue()
abstract class IncidentDto implements Built<IncidentDto, IncidentDtoBuilder> {
  @BuiltValueField(wireName: r'objectId')
  int get objectId;

  @BuiltValueField(wireName: r'commonInfo')
  String? get commonInfo;

  @BuiltValueField(wireName: r'status')
  IncidentStatus get status;
  // enum statusEnum {  0,  1,  2,  3,  4,  };

  @BuiltValueField(wireName: r'meaning')
  IncidentMeaning get meaning;
  // enum meaningEnum {  0,  1,  2,  3,  4,  };

  @BuiltValueField(wireName: r'permissionBy')
  Permission get permissionBy;
  // enum permissionByEnum {  0,  1,  2,  3,  4,  5,  6,  7,  8,  10,  11,  12,  13,  14,  15,  16,  17,  18,  };

  @BuiltValueField(wireName: r'decisions')
  BuiltList<IncidentDecision>? get decisions;

  @BuiltValueField(wireName: r'linkedLogs')
  BuiltList<IncidentLogDto>? get linkedLogs;

  IncidentDto._();

  factory IncidentDto([void updates(IncidentDtoBuilder b)]) = _$IncidentDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(IncidentDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<IncidentDto> get serializer => _$IncidentDtoSerializer();
}

class _$IncidentDtoSerializer implements PrimitiveSerializer<IncidentDto> {
  @override
  final Iterable<Type> types = const [IncidentDto, _$IncidentDto];

  @override
  final String wireName = r'IncidentDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    IncidentDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'objectId';
    yield serializers.serialize(
      object.objectId,
      specifiedType: const FullType(int),
    );
    yield r'commonInfo';
    yield object.commonInfo == null ? null : serializers.serialize(
      object.commonInfo,
      specifiedType: const FullType.nullable(String),
    );
    yield r'status';
    yield serializers.serialize(
      object.status,
      specifiedType: const FullType(IncidentStatus),
    );
    yield r'meaning';
    yield serializers.serialize(
      object.meaning,
      specifiedType: const FullType(IncidentMeaning),
    );
    yield r'permissionBy';
    yield serializers.serialize(
      object.permissionBy,
      specifiedType: const FullType(Permission),
    );
    yield r'decisions';
    yield object.decisions == null ? null : serializers.serialize(
      object.decisions,
      specifiedType: const FullType.nullable(BuiltList, [FullType(IncidentDecision)]),
    );
    yield r'linkedLogs';
    yield object.linkedLogs == null ? null : serializers.serialize(
      object.linkedLogs,
      specifiedType: const FullType.nullable(BuiltList, [FullType(IncidentLogDto)]),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    IncidentDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required IncidentDtoBuilder result,
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
        case r'commonInfo':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.commonInfo = valueDes;
          break;
        case r'status':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(IncidentStatus),
          ) as IncidentStatus;
          result.status = valueDes;
          break;
        case r'meaning':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(IncidentMeaning),
          ) as IncidentMeaning;
          result.meaning = valueDes;
          break;
        case r'permissionBy':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(Permission),
          ) as Permission;
          result.permissionBy = valueDes;
          break;
        case r'decisions':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(IncidentDecision)]),
          ) as BuiltList<IncidentDecision>?;
          if (valueDes == null) continue;
          result.decisions.replace(valueDes);
          break;
        case r'linkedLogs':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(IncidentLogDto)]),
          ) as BuiltList<IncidentLogDto>?;
          if (valueDes == null) continue;
          result.linkedLogs.replace(valueDes);
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  IncidentDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = IncidentDtoBuilder();
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

