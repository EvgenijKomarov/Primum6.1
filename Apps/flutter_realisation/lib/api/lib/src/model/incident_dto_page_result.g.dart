// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$IncidentDtoPageResult extends IncidentDtoPageResult {
  @override
  final BuiltList<IncidentDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$IncidentDtoPageResult(
          [void Function(IncidentDtoPageResultBuilder)? updates]) =>
      (IncidentDtoPageResultBuilder()..update(updates))._build();

  _$IncidentDtoPageResult._(
      {this.items, this.totalItemsCount, this.totalPages, this.currentPage})
      : super._();
  @override
  IncidentDtoPageResult rebuild(
          void Function(IncidentDtoPageResultBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  IncidentDtoPageResultBuilder toBuilder() =>
      IncidentDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is IncidentDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'IncidentDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class IncidentDtoPageResultBuilder
    implements Builder<IncidentDtoPageResult, IncidentDtoPageResultBuilder> {
  _$IncidentDtoPageResult? _$v;

  ListBuilder<IncidentDto>? _items;
  ListBuilder<IncidentDto> get items =>
      _$this._items ??= ListBuilder<IncidentDto>();
  set items(ListBuilder<IncidentDto>? items) => _$this._items = items;

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

  IncidentDtoPageResultBuilder() {
    IncidentDtoPageResult._defaults(this);
  }

  IncidentDtoPageResultBuilder get _$this {
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
  void replace(IncidentDtoPageResult other) {
    _$v = other as _$IncidentDtoPageResult;
  }

  @override
  void update(void Function(IncidentDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  IncidentDtoPageResult build() => _build();

  _$IncidentDtoPageResult _build() {
    _$IncidentDtoPageResult _$result;
    try {
      _$result = _$v ??
          _$IncidentDtoPageResult._(
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
            r'IncidentDtoPageResult', _$failedField, e.toString());
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
