// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'abonement_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$AbonementDtoPageResult extends AbonementDtoPageResult {
  @override
  final BuiltList<AbonementDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$AbonementDtoPageResult([
    void Function(AbonementDtoPageResultBuilder)? updates,
  ]) => (AbonementDtoPageResultBuilder()..update(updates))._build();

  _$AbonementDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  AbonementDtoPageResult rebuild(
    void Function(AbonementDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  AbonementDtoPageResultBuilder toBuilder() =>
      AbonementDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is AbonementDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'AbonementDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class AbonementDtoPageResultBuilder
    implements Builder<AbonementDtoPageResult, AbonementDtoPageResultBuilder> {
  _$AbonementDtoPageResult? _$v;

  ListBuilder<AbonementDto>? _items;
  ListBuilder<AbonementDto> get items =>
      _$this._items ??= ListBuilder<AbonementDto>();
  set items(ListBuilder<AbonementDto>? items) => _$this._items = items;

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

  AbonementDtoPageResultBuilder() {
    AbonementDtoPageResult._defaults(this);
  }

  AbonementDtoPageResultBuilder get _$this {
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
  void replace(AbonementDtoPageResult other) {
    _$v = other as _$AbonementDtoPageResult;
  }

  @override
  void update(void Function(AbonementDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  AbonementDtoPageResult build() => _build();

  _$AbonementDtoPageResult _build() {
    _$AbonementDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$AbonementDtoPageResult._(
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
          r'AbonementDtoPageResult',
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
