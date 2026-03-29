// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'promocode_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$PromocodeDtoPageResult extends PromocodeDtoPageResult {
  @override
  final BuiltList<PromocodeDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$PromocodeDtoPageResult(
          [void Function(PromocodeDtoPageResultBuilder)? updates]) =>
      (PromocodeDtoPageResultBuilder()..update(updates))._build();

  _$PromocodeDtoPageResult._(
      {this.items, this.totalItemsCount, this.totalPages, this.currentPage})
      : super._();
  @override
  PromocodeDtoPageResult rebuild(
          void Function(PromocodeDtoPageResultBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  PromocodeDtoPageResultBuilder toBuilder() =>
      PromocodeDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is PromocodeDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'PromocodeDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class PromocodeDtoPageResultBuilder
    implements Builder<PromocodeDtoPageResult, PromocodeDtoPageResultBuilder> {
  _$PromocodeDtoPageResult? _$v;

  ListBuilder<PromocodeDto>? _items;
  ListBuilder<PromocodeDto> get items =>
      _$this._items ??= ListBuilder<PromocodeDto>();
  set items(ListBuilder<PromocodeDto>? items) => _$this._items = items;

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

  PromocodeDtoPageResultBuilder() {
    PromocodeDtoPageResult._defaults(this);
  }

  PromocodeDtoPageResultBuilder get _$this {
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
  void replace(PromocodeDtoPageResult other) {
    _$v = other as _$PromocodeDtoPageResult;
  }

  @override
  void update(void Function(PromocodeDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  PromocodeDtoPageResult build() => _build();

  _$PromocodeDtoPageResult _build() {
    _$PromocodeDtoPageResult _$result;
    try {
      _$result = _$v ??
          _$PromocodeDtoPageResult._(
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
            r'PromocodeDtoPageResult', _$failedField, e.toString());
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
