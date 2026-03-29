//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_collection/built_collection.dart';
import 'package:my_api/src/model/teacher_rank_dto.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'teacher_rank_dto_page_result.g.dart';

/// TeacherRankDtoPageResult
///
/// Properties:
/// * [items] 
/// * [totalItemsCount] 
/// * [totalPages] 
/// * [currentPage] 
@BuiltValue()
abstract class TeacherRankDtoPageResult implements Built<TeacherRankDtoPageResult, TeacherRankDtoPageResultBuilder> {
  @BuiltValueField(wireName: r'items')
  BuiltList<TeacherRankDto>? get items;

  @BuiltValueField(wireName: r'totalItemsCount')
  int? get totalItemsCount;

  @BuiltValueField(wireName: r'totalPages')
  int? get totalPages;

  @BuiltValueField(wireName: r'currentPage')
  int? get currentPage;

  TeacherRankDtoPageResult._();

  factory TeacherRankDtoPageResult([void updates(TeacherRankDtoPageResultBuilder b)]) = _$TeacherRankDtoPageResult;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(TeacherRankDtoPageResultBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<TeacherRankDtoPageResult> get serializer => _$TeacherRankDtoPageResultSerializer();
}

class _$TeacherRankDtoPageResultSerializer implements PrimitiveSerializer<TeacherRankDtoPageResult> {
  @override
  final Iterable<Type> types = const [TeacherRankDtoPageResult, _$TeacherRankDtoPageResult];

  @override
  final String wireName = r'TeacherRankDtoPageResult';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    TeacherRankDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.items != null) {
      yield r'items';
      yield serializers.serialize(
        object.items,
        specifiedType: const FullType.nullable(BuiltList, [FullType(TeacherRankDto)]),
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
    TeacherRankDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required TeacherRankDtoPageResultBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'items':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(TeacherRankDto)]),
          ) as BuiltList<TeacherRankDto>?;
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
  TeacherRankDtoPageResult deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = TeacherRankDtoPageResultBuilder();
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

