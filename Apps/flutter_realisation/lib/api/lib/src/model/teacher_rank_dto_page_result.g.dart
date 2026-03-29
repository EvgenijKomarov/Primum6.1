// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'teacher_rank_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$TeacherRankDtoPageResult extends TeacherRankDtoPageResult {
  @override
  final BuiltList<TeacherRankDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$TeacherRankDtoPageResult(
          [void Function(TeacherRankDtoPageResultBuilder)? updates]) =>
      (TeacherRankDtoPageResultBuilder()..update(updates))._build();

  _$TeacherRankDtoPageResult._(
      {this.items, this.totalItemsCount, this.totalPages, this.currentPage})
      : super._();
  @override
  TeacherRankDtoPageResult rebuild(
          void Function(TeacherRankDtoPageResultBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  TeacherRankDtoPageResultBuilder toBuilder() =>
      TeacherRankDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is TeacherRankDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'TeacherRankDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class TeacherRankDtoPageResultBuilder
    implements
        Builder<TeacherRankDtoPageResult, TeacherRankDtoPageResultBuilder> {
  _$TeacherRankDtoPageResult? _$v;

  ListBuilder<TeacherRankDto>? _items;
  ListBuilder<TeacherRankDto> get items =>
      _$this._items ??= ListBuilder<TeacherRankDto>();
  set items(ListBuilder<TeacherRankDto>? items) => _$this._items = items;

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

  TeacherRankDtoPageResultBuilder() {
    TeacherRankDtoPageResult._defaults(this);
  }

  TeacherRankDtoPageResultBuilder get _$this {
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
  void replace(TeacherRankDtoPageResult other) {
    _$v = other as _$TeacherRankDtoPageResult;
  }

  @override
  void update(void Function(TeacherRankDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  TeacherRankDtoPageResult build() => _build();

  _$TeacherRankDtoPageResult _build() {
    _$TeacherRankDtoPageResult _$result;
    try {
      _$result = _$v ??
          _$TeacherRankDtoPageResult._(
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
            r'TeacherRankDtoPageResult', _$failedField, e.toString());
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
