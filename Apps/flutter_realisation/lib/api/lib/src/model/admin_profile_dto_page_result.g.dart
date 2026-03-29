// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'admin_profile_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$AdminProfileDtoPageResult extends AdminProfileDtoPageResult {
  @override
  final BuiltList<AdminProfileDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$AdminProfileDtoPageResult(
          [void Function(AdminProfileDtoPageResultBuilder)? updates]) =>
      (AdminProfileDtoPageResultBuilder()..update(updates))._build();

  _$AdminProfileDtoPageResult._(
      {this.items, this.totalItemsCount, this.totalPages, this.currentPage})
      : super._();
  @override
  AdminProfileDtoPageResult rebuild(
          void Function(AdminProfileDtoPageResultBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  AdminProfileDtoPageResultBuilder toBuilder() =>
      AdminProfileDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is AdminProfileDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'AdminProfileDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class AdminProfileDtoPageResultBuilder
    implements
        Builder<AdminProfileDtoPageResult, AdminProfileDtoPageResultBuilder> {
  _$AdminProfileDtoPageResult? _$v;

  ListBuilder<AdminProfileDto>? _items;
  ListBuilder<AdminProfileDto> get items =>
      _$this._items ??= ListBuilder<AdminProfileDto>();
  set items(ListBuilder<AdminProfileDto>? items) => _$this._items = items;

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

  AdminProfileDtoPageResultBuilder() {
    AdminProfileDtoPageResult._defaults(this);
  }

  AdminProfileDtoPageResultBuilder get _$this {
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
  void replace(AdminProfileDtoPageResult other) {
    _$v = other as _$AdminProfileDtoPageResult;
  }

  @override
  void update(void Function(AdminProfileDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  AdminProfileDtoPageResult build() => _build();

  _$AdminProfileDtoPageResult _build() {
    _$AdminProfileDtoPageResult _$result;
    try {
      _$result = _$v ??
          _$AdminProfileDtoPageResult._(
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
            r'AdminProfileDtoPageResult', _$failedField, e.toString());
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
