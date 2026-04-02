// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseDtoPageResult extends CourseDtoPageResult {
  @override
  final BuiltList<CourseDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$CourseDtoPageResult([
    void Function(CourseDtoPageResultBuilder)? updates,
  ]) => (CourseDtoPageResultBuilder()..update(updates))._build();

  _$CourseDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  CourseDtoPageResult rebuild(
    void Function(CourseDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  CourseDtoPageResultBuilder toBuilder() =>
      CourseDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'CourseDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class CourseDtoPageResultBuilder
    implements Builder<CourseDtoPageResult, CourseDtoPageResultBuilder> {
  _$CourseDtoPageResult? _$v;

  ListBuilder<CourseDto>? _items;
  ListBuilder<CourseDto> get items =>
      _$this._items ??= ListBuilder<CourseDto>();
  set items(ListBuilder<CourseDto>? items) => _$this._items = items;

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

  CourseDtoPageResultBuilder() {
    CourseDtoPageResult._defaults(this);
  }

  CourseDtoPageResultBuilder get _$this {
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
  void replace(CourseDtoPageResult other) {
    _$v = other as _$CourseDtoPageResult;
  }

  @override
  void update(void Function(CourseDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseDtoPageResult build() => _build();

  _$CourseDtoPageResult _build() {
    _$CourseDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$CourseDtoPageResult._(
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
          r'CourseDtoPageResult',
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
