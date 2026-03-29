//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/teacher_shedule_dto.dart';
import 'package:built_collection/built_collection.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'teacher_shedule_dto_page_result.g.dart';

/// TeacherSheduleDtoPageResult
///
/// Properties:
/// * [items] 
/// * [totalItemsCount] 
/// * [totalPages] 
/// * [currentPage] 
@BuiltValue()
abstract class TeacherSheduleDtoPageResult implements Built<TeacherSheduleDtoPageResult, TeacherSheduleDtoPageResultBuilder> {
  @BuiltValueField(wireName: r'items')
  BuiltList<TeacherSheduleDto>? get items;

  @BuiltValueField(wireName: r'totalItemsCount')
  int? get totalItemsCount;

  @BuiltValueField(wireName: r'totalPages')
  int? get totalPages;

  @BuiltValueField(wireName: r'currentPage')
  int? get currentPage;

  TeacherSheduleDtoPageResult._();

  factory TeacherSheduleDtoPageResult([void updates(TeacherSheduleDtoPageResultBuilder b)]) = _$TeacherSheduleDtoPageResult;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(TeacherSheduleDtoPageResultBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<TeacherSheduleDtoPageResult> get serializer => _$TeacherSheduleDtoPageResultSerializer();
}

class _$TeacherSheduleDtoPageResultSerializer implements PrimitiveSerializer<TeacherSheduleDtoPageResult> {
  @override
  final Iterable<Type> types = const [TeacherSheduleDtoPageResult, _$TeacherSheduleDtoPageResult];

  @override
  final String wireName = r'TeacherSheduleDtoPageResult';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    TeacherSheduleDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.items != null) {
      yield r'items';
      yield serializers.serialize(
        object.items,
        specifiedType: const FullType.nullable(BuiltList, [FullType(TeacherSheduleDto)]),
      );
    }
    if (object.totalItemsCount != null) {
      yield r'totalItemsCount';
      yield serializers.serialize(
        object.totalItemsCount,
        specifiedType: const FullType(int),
      );
    }
    if (object.totalPages != null) {
      yield r'totalPages';
      yield serializers.serialize(
        object.totalPages,
        specifiedType: const FullType(int),
      );
    }
    if (object.currentPage != null) {
      yield r'currentPage';
      yield serializers.serialize(
        object.currentPage,
        specifiedType: const FullType(int),
      );
    }
  }

  @override
  Object serialize(
    Serializers serializers,
    TeacherSheduleDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required TeacherSheduleDtoPageResultBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'items':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(TeacherSheduleDto)]),
          ) as BuiltList<TeacherSheduleDto>?;
          if (valueDes == null) continue;
          result.items.replace(valueDes);
          break;
        case r'totalItemsCount':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.totalItemsCount = valueDes;
          break;
        case r'totalPages':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.totalPages = valueDes;
          break;
        case r'currentPage':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType(int),
          ) as int;
          result.currentPage = valueDes;
          break;
        default:
          unhandled.add(key);
          unhandled.add(value);
          break;
      }
    }
  }

  @override
  TeacherSheduleDtoPageResult deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = TeacherSheduleDtoPageResultBuilder();
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

