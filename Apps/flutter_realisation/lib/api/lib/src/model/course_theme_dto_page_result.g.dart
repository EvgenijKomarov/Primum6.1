// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'course_theme_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$CourseThemeDtoPageResult extends CourseThemeDtoPageResult {
  @override
  final BuiltList<CourseThemeDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$CourseThemeDtoPageResult([
    void Function(CourseThemeDtoPageResultBuilder)? updates,
  ]) => (CourseThemeDtoPageResultBuilder()..update(updates))._build();

  _$CourseThemeDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  CourseThemeDtoPageResult rebuild(
    void Function(CourseThemeDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  CourseThemeDtoPageResultBuilder toBuilder() =>
      CourseThemeDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CourseThemeDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'CourseThemeDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class CourseThemeDtoPageResultBuilder
    implements
        Builder<CourseThemeDtoPageResult, CourseThemeDtoPageResultBuilder> {
  _$CourseThemeDtoPageResult? _$v;

  ListBuilder<CourseThemeDto>? _items;
  ListBuilder<CourseThemeDto> get items =>
      _$this._items ??= ListBuilder<CourseThemeDto>();
  set items(ListBuilder<CourseThemeDto>? items) => _$this._items = items;

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

  CourseThemeDtoPageResultBuilder() {
    CourseThemeDtoPageResult._defaults(this);
  }

  CourseThemeDtoPageResultBuilder get _$this {
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
  void replace(CourseThemeDtoPageResult other) {
    _$v = other as _$CourseThemeDtoPageResult;
  }

  @override
  void update(void Function(CourseThemeDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CourseThemeDtoPageResult build() => _build();

  _$CourseThemeDtoPageResult _build() {
    _$CourseThemeDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$CourseThemeDtoPageResult._(
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
          r'CourseThemeDtoPageResult',
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
