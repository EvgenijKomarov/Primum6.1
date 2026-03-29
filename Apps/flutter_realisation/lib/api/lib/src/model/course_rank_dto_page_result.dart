//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/course_rank_dto.dart';
import 'package:built_collection/built_collection.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'course_rank_dto_page_result.g.dart';

/// CourseRankDtoPageResult
///
/// Properties:
/// * [items] 
/// * [totalItemsCount] 
/// * [totalPages] 
/// * [currentPage] 
@BuiltValue()
abstract class CourseRankDtoPageResult implements Built<CourseRankDtoPageResult, CourseRankDtoPageResultBuilder> {
  @BuiltValueField(wireName: r'items')
  BuiltList<CourseRankDto>? get items;

  @BuiltValueField(wireName: r'totalItemsCount')
  int? get totalItemsCount;

  @BuiltValueField(wireName: r'totalPages')
  int? get totalPages;

  @BuiltValueField(wireName: r'currentPage')
  int? get currentPage;

  CourseRankDtoPageResult._();

  factory CourseRankDtoPageResult([void updates(CourseRankDtoPageResultBuilder b)]) = _$CourseRankDtoPageResult;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(CourseRankDtoPageResultBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<CourseRankDtoPageResult> get serializer => _$CourseRankDtoPageResultSerializer();
}

class _$CourseRankDtoPageResultSerializer implements PrimitiveSerializer<CourseRankDtoPageResult> {
  @override
  final Iterable<Type> types = const [CourseRankDtoPageResult, _$CourseRankDtoPageResult];

  @override
  final String wireName = r'CourseRankDtoPageResult';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    CourseRankDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.items != null) {
      yield r'items';
      yield serializers.serialize(
        object.items,
        specifiedType: const FullType.nullable(BuiltList, [FullType(CourseRankDto)]),
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
    CourseRankDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required CourseRankDtoPageResultBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'items':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(CourseRankDto)]),
          ) as BuiltList<CourseRankDto>?;
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
  CourseRankDtoPageResult deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = CourseRankDtoPageResultBuilder();
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

