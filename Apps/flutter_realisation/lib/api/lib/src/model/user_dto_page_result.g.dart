// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$UserDtoPageResult extends UserDtoPageResult {
  @override
  final BuiltList<UserDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$UserDtoPageResult([
    void Function(UserDtoPageResultBuilder)? updates,
  ]) => (UserDtoPageResultBuilder()..update(updates))._build();

  _$UserDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  UserDtoPageResult rebuild(void Function(UserDtoPageResultBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  UserDtoPageResultBuilder toBuilder() =>
      UserDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is UserDtoPageResult &&
        items == other.items &&
        totalItemsCount == other.totalItemsCount &&
        totalPages == other.totalPages &&
        currentPage == other.currentPage;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, items.hashCode);
    _$hash = $jc(_$hash, totalItemsCount.hashCode);
    _$hash = $jc(_$hash, totalPages.hashCode);
    _$hash = $jc(_$hash, currentPage.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'UserDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class UserDtoPageResultBuilder
    implements Builder<UserDtoPageResult, UserDtoPageResultBuilder> {
  _$UserDtoPageResult? _$v;

  ListBuilder<UserDto>? _items;
  ListBuilder<UserDto> get items => _$this._items ??= ListBuilder<UserDto>();
  set items(ListBuilder<UserDto>? items) => _$this._items = items;

  int? _totalItemsCount;
  int? get totalItemsCount => _$this._totalItemsCount;
  set totalItemsCount(int? totalItemsCount) =>
      _$this._totalItemsCount = totalItemsCount;

  int? _totalPages;
  int? get totalPages => _$this._totalPages;
  set totalPages(int? totalPages) => _$this._totalPages = totalPages;

  int? _currentPage;
  int? get currentPage => _$this._currentPage;
  set currentPage(int? currentPage) => _$this._currentPage = currentPage;

  UserDtoPageResultBuilder() {
    UserDtoPageResult._defaults(this);
  }

  UserDtoPageResultBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _items = $v.items?.toBuilder();
      _totalItemsCount = $v.totalItemsCount;
      _totalPages = $v.totalPages;
      _currentPage = $v.currentPage;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(UserDtoPageResult other) {
    _$v = other as _$UserDtoPageResult;
  }

  @override
  void update(void Function(UserDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  UserDtoPageResult build() => _build();

  _$UserDtoPageResult _build() {
    _$UserDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$UserDtoPageResult._(
            items: _items?.build(),
            totalItemsCount: totalItemsCount,
            totalPages: totalPages,
            currentPage: currentPage,
          );
    } catch (_) {
      late String _$failedField;
      try {
        _$failedField = 'items';
        _items?.build();
      } catch (e) {
        throw BuiltValueNestedFieldError(
          r'UserDtoPageResult',
          _$failedField,
          e.toString(),
        );
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
