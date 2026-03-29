// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_rank_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseRankDtoPageResult extends CourseRankDtoPageResult {
  @override
  final BuiltList<CourseRankDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$CourseRankDtoPageResult(
          [void Function(CourseRankDtoPageResultBuilder)? updates]) =>
      (CourseRankDtoPageResultBuilder()..update(updates))._build();

  _$CourseRankDtoPageResult._(
      {this.items, this.totalItemsCount, this.totalPages, this.currentPage})
      : super._();
  @override
  CourseRankDtoPageResult rebuild(
          void Function(CourseRankDtoPageResultBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  CourseRankDtoPageResultBuilder toBuilder() =>
      CourseRankDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseRankDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'CourseRankDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class CourseRankDtoPageResultBuilder
    implements
        Builder<CourseRankDtoPageResult, CourseRankDtoPageResultBuilder> {
  _$CourseRankDtoPageResult? _$v;

  ListBuilder<CourseRankDto>? _items;
  ListBuilder<CourseRankDto> get items =>
      _$this._items ??= ListBuilder<CourseRankDto>();
  set items(ListBuilder<CourseRankDto>? items) => _$this._items = items;

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

  CourseRankDtoPageResultBuilder() {
    CourseRankDtoPageResult._defaults(this);
  }

  CourseRankDtoPageResultBuilder get _$this {
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
  void replace(CourseRankDtoPageResult other) {
    _$v = other as _$CourseRankDtoPageResult;
  }

  @override
  void update(void Function(CourseRankDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseRankDtoPageResult build() => _build();

  _$CourseRankDtoPageResult _build() {
    _$CourseRankDtoPageResult _$result;
    try {
      _$result = _$v ??
          _$CourseRankDtoPageResult._(
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
            r'CourseRankDtoPageResult', _$failedField, e.toString());
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
