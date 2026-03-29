//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'incident_log_dto.g.dart';

/// IncidentLogDto
///
/// Properties:
/// * [id] 
/// * [adminUserId] 
/// * [dateTime] 
/// * [adminDisplayName] 
/// * [description] 
@BuiltValue()
abstract class IncidentLogDto implements Built<IncidentLogDto, IncidentLogDtoBuilder> {
  @BuiltValueField(wireName: r'id')
  int get id;

  @BuiltValueField(wireName: r'adminUserId')
  int get adminUserId;

  @BuiltValueField(wireName: r'dateTime')
  DateTime get dateTime;

  @BuiltValueField(wireName: r'adminDisplayName')
  String? get adminDisplayName;

  @BuiltValueField(wireName: r'description')
  String? get description;

  IncidentLogDto._();

  factory IncidentLogDto([void updates(IncidentLogDtoBuilder b)]) = _$IncidentLogDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(IncidentLogDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<IncidentLogDto> get serializer => _$IncidentLogDtoSerializer();
}

class _$IncidentLogDtoSerializer implements PrimitiveSerializer<IncidentLogDto> {
  @override
  final Iterable<Type> types = const [IncidentLogDto, _$IncidentLogDto];

  @override
  final String wireName = r'IncidentLogDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    IncidentLogDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    yield r'id';
    yield serializers.serialize(
      object.id,
      specifiedType: const FullType(int),
    );
    yield r'adminUserId';
    yield serializers.serialize(
      object.adminUserId,
      specifiedType: const FullType(int),
    );
    yield r'dateTime';
    yield serializers.serialize(
      object.dateTime,
      specifiedType: const FullType(DateTime),
    );
    yield r'adminDisplayName';
    yield object.adminDisplayName == null ? null : serializers.serialize(
      object.adminDisplayName,
      specifiedType: const FullType.nullable(String),
    );
    yield r'description';
    yield object.description == null ? null : serializers.serialize(
      object.description,
      specifiedType: const FullType.nullable(String),
    );
  }

  @override
  Object serialize(
    Serializers serializers,
    IncidentLogDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required IncidentLogDtoBuilder result,
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
        case r'adminUserId':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.adminUserId = valueDes;
          break;
        case r'dateTime':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DateTime),
          ) as DateTime;
          result.dateTime = valueDes;
          break;
        case r'adminDisplayName':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.adminDisplayName = valueDes;
          break;
        case r'description':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(String),
          ) as String?;
          if (valueDes == null) continue;
          result.description = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  IncidentLogDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = IncidentLogDtoBuilder();
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

