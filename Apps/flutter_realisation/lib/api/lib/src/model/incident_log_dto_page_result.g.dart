// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'incident_log_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$IncidentLogDtoPageResult extends IncidentLogDtoPageResult {
  @override
  final BuiltList<IncidentLogDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$IncidentLogDtoPageResult([
    void Function(IncidentLogDtoPageResultBuilder)? updates,
  ]) => (IncidentLogDtoPageResultBuilder()..update(updates))._build();

  _$IncidentLogDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  IncidentLogDtoPageResult rebuild(
    void Function(IncidentLogDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  IncidentLogDtoPageResultBuilder toBuilder() =>
      IncidentLogDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is IncidentLogDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'IncidentLogDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class IncidentLogDtoPageResultBuilder
    implements
        Builder<IncidentLogDtoPageResult, IncidentLogDtoPageResultBuilder> {
  _$IncidentLogDtoPageResult? _$v;

  ListBuilder<IncidentLogDto>? _items;
  ListBuilder<IncidentLogDto> get items =>
      _$this._items ??= ListBuilder<IncidentLogDto>();
  set items(ListBuilder<IncidentLogDto>? items) => _$this._items = items;

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

  IncidentLogDtoPageResultBuilder() {
    IncidentLogDtoPageResult._defaults(this);
  }

  IncidentLogDtoPageResultBuilder get _$this {
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
  void replace(IncidentLogDtoPageResult other) {
    _$v = other as _$IncidentLogDtoPageResult;
  }

  @override
  void update(void Function(IncidentLogDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  IncidentLogDtoPageResult build() => _build();

  _$IncidentLogDtoPageResult _build() {
    _$IncidentLogDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$IncidentLogDtoPageResult._(
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
          r'IncidentLogDtoPageResult',
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
