//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_collection/built_collection.dart';
import 'package:my_api/src/model/abonement_dto.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'abonement_dto_page_result.g.dart';

/// AbonementDtoPageResult
///
/// Properties:
/// * [items] 
/// * [totalItemsCount] 
/// * [totalPages] 
/// * [currentPage] 
@BuiltValue()
abstract class AbonementDtoPageResult implements Built<AbonementDtoPageResult, AbonementDtoPageResultBuilder> {
  @BuiltValueField(wireName: r'items')
  BuiltList<AbonementDto>? get items;

  @BuiltValueField(wireName: r'totalItemsCount')
  int? get totalItemsCount;

  @BuiltValueField(wireName: r'totalPages')
  int? get totalPages;

  @BuiltValueField(wireName: r'currentPage')
  int? get currentPage;

  AbonementDtoPageResult._();

  factory AbonementDtoPageResult([void updates(AbonementDtoPageResultBuilder b)]) = _$AbonementDtoPageResult;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(AbonementDtoPageResultBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<AbonementDtoPageResult> get serializer => _$AbonementDtoPageResultSerializer();
}

class _$AbonementDtoPageResultSerializer implements PrimitiveSerializer<AbonementDtoPageResult> {
  @override
  final Iterable<Type> types = const [AbonementDtoPageResult, _$AbonementDtoPageResult];

  @override
  final String wireName = r'AbonementDtoPageResult';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    AbonementDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.items != null) {
      yield r'items';
      yield serializers.serialize(
        object.items,
        specifiedType: const FullType.nullable(BuiltList, [FullType(AbonementDto)]),
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
    AbonementDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required AbonementDtoPageResultBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'items':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(AbonementDto)]),
          ) as BuiltList<AbonementDto>?;
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
  AbonementDtoPageResult deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = AbonementDtoPageResultBuilder();
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

