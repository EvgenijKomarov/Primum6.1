//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/day_of_week.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'teacher_shedule_input_dto.g.dart';

/// TeacherSheduleInputDto
///
/// Properties:
/// * [time] 
/// * [dayOfWeek] 
@BuiltValue()
abstract class TeacherSheduleInputDto implements Built<TeacherSheduleInputDto, TeacherSheduleInputDtoBuilder> {
  @BuiltValueField(wireName: r'time')
  int? get time;

  @BuiltValueField(wireName: r'dayOfWeek')
  DayOfWeek? get dayOfWeek;
  // enum dayOfWeekEnum {  0,  1,  2,  3,  4,  5,  6,  };

  TeacherSheduleInputDto._();

  factory TeacherSheduleInputDto([void updates(TeacherSheduleInputDtoBuilder b)]) = _$TeacherSheduleInputDto;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(TeacherSheduleInputDtoBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<TeacherSheduleInputDto> get serializer => _$TeacherSheduleInputDtoSerializer();
}

class _$TeacherSheduleInputDtoSerializer implements PrimitiveSerializer<TeacherSheduleInputDto> {
  @override
  final Iterable<Type> types = const [TeacherSheduleInputDto, _$TeacherSheduleInputDto];

  @override
  final String wireName = r'TeacherSheduleInputDto';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    TeacherSheduleInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.time != null) {
      yield r'time';
      yield serializers.serialize(
        object.time,
        specifiedType: const FullType(int),
      );
    }
    if (object.dayOfWeek != null) {
      yield r'dayOfWeek';
      yield serializers.serialize(
        object.dayOfWeek,
        specifiedType: const FullType(DayOfWeek),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    TeacherSheduleInputDto object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required TeacherSheduleInputDtoBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'time':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.time = valueDes;
          break;
        case r'dayOfWeek':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(DayOfWeek),
          ) as DayOfWeek;
          result.dayOfWeek = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  TeacherSheduleInputDto deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = TeacherSheduleInputDtoBuilder();
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

