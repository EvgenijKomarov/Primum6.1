//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:my_api/src/model/user_dto.dart';
import 'package:built_collection/built_collection.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'user_dto_page_result.g.dart';

/// UserDtoPageResult
///
/// Properties:
/// * [items] 
/// * [totalItemsCount] 
/// * [totalPages] 
/// * [currentPage] 
@BuiltValue()
abstract class UserDtoPageResult implements Built<UserDtoPageResult, UserDtoPageResultBuilder> {
  @BuiltValueField(wireName: r'items')
  BuiltList<UserDto>? get items;

  @BuiltValueField(wireName: r'totalItemsCount')
  int? get totalItemsCount;

  @BuiltValueField(wireName: r'totalPages')
  int? get totalPages;

  @BuiltValueField(wireName: r'currentPage')
  int? get currentPage;

  UserDtoPageResult._();

  factory UserDtoPageResult([void updates(UserDtoPageResultBuilder b)]) = _$UserDtoPageResult;

  @BuiltValueHook(initializeBuilder: true)
  static void _defaults(UserDtoPageResultBuilder b) => b;

  @BuiltValueSerializer(custom: true)
  static Serializer<UserDtoPageResult> get serializer => _$UserDtoPageResultSerializer();
}

class _$UserDtoPageResultSerializer implements PrimitiveSerializer<UserDtoPageResult> {
  @override
  final Iterable<Type> types = const [UserDtoPageResult, _$UserDtoPageResult];

  @override
  final String wireName = r'UserDtoPageResult';

  Iterable<Object?> _serializeProperties(
    Serializers serializers,
    UserDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) sync* {
    if (object.items != null) {
      yield r'items';
      yield serializers.serialize(
        object.items,
        specifiedType: const FullType.nullable(BuiltList, [FullType(UserDto)]),
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
    UserDtoPageResult object, {
    FullType specifiedType = FullType.unspecified,
  }) {
    return _serializeProperties(serializers, object, specifiedType: specifiedType).toList();
  }

  void _deserializeProperties(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
    required List<Object?> serializedList,
    required UserDtoPageResultBuilder result,
    required List<Object?> unhandled,
  }) {
    for (var i = 0; i < serializedList.length; i += 2) {
      final key = serializedList[i] as String;
      final value = serializedList[i + 1];
      switch (key) {
        case r'items':
          final valueDes = serializers.deserialize(
            value,
            specifiedType: const FullType.nullable(BuiltList, [FullType(UserDto)]),
          ) as BuiltList<UserDto>?;
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
  UserDtoPageResult deserialize(
    Serializers serializers,
    Object serialized, {
    FullType specifiedType = FullType.unspecified,
  }) {
    final result = UserDtoPageResultBuilder();
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

