// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'abonement_shedule_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$AbonementSheduleDtoPageResult extends AbonementSheduleDtoPageResult {
  @override
  final BuiltList<AbonementSheduleDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$AbonementSheduleDtoPageResult([
    void Function(AbonementSheduleDtoPageResultBuilder)? updates,
  ]) => (AbonementSheduleDtoPageResultBuilder()..update(updates))._build();

  _$AbonementSheduleDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  AbonementSheduleDtoPageResult rebuild(
    void Function(AbonementSheduleDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  AbonementSheduleDtoPageResultBuilder toBuilder() =>
      AbonementSheduleDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is AbonementSheduleDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'AbonementSheduleDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class AbonementSheduleDtoPageResultBuilder
    implements
        Builder<
          AbonementSheduleDtoPageResult,
          AbonementSheduleDtoPageResultBuilder
        > {
  _$AbonementSheduleDtoPageResult? _$v;

  ListBuilder<AbonementSheduleDto>? _items;
  ListBuilder<AbonementSheduleDto> get items =>
      _$this._items ??= ListBuilder<AbonementSheduleDto>();
  set items(ListBuilder<AbonementSheduleDto>? items) => _$this._items = items;

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

  AbonementSheduleDtoPageResultBuilder() {
    AbonementSheduleDtoPageResult._defaults(this);
  }

  AbonementSheduleDtoPageResultBuilder get _$this {
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
  void replace(AbonementSheduleDtoPageResult other) {
    _$v = other as _$AbonementSheduleDtoPageResult;
  }

  @override
  void update(void Function(AbonementSheduleDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  AbonementSheduleDtoPageResult build() => _build();

  _$AbonementSheduleDtoPageResult _build() {
    _$AbonementSheduleDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$AbonementSheduleDtoPageResult._(
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
          r'AbonementSheduleDtoPageResult',
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
