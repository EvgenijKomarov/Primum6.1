// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'lesson_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$LessonDtoPageResult extends LessonDtoPageResult {
  @override
  final BuiltList<LessonDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$LessonDtoPageResult(
          [void Function(LessonDtoPageResultBuilder)? updates]) =>
      (LessonDtoPageResultBuilder()..update(updates))._build();

  _$LessonDtoPageResult._(
      {this.items, this.totalItemsCount, this.totalPages, this.currentPage})
      : super._();
  @override
  LessonDtoPageResult rebuild(
          void Function(LessonDtoPageResultBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  LessonDtoPageResultBuilder toBuilder() =>
      LessonDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is LessonDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'LessonDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class LessonDtoPageResultBuilder
    implements Builder<LessonDtoPageResult, LessonDtoPageResultBuilder> {
  _$LessonDtoPageResult? _$v;

  ListBuilder<LessonDto>? _items;
  ListBuilder<LessonDto> get items =>
      _$this._items ??= ListBuilder<LessonDto>();
  set items(ListBuilder<LessonDto>? items) => _$this._items = items;

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

  LessonDtoPageResultBuilder() {
    LessonDtoPageResult._defaults(this);
  }

  LessonDtoPageResultBuilder get _$this {
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
  void replace(LessonDtoPageResult other) {
    _$v = other as _$LessonDtoPageResult;
  }

  @override
  void update(void Function(LessonDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  LessonDtoPageResult build() => _build();

  _$LessonDtoPageResult _build() {
    _$LessonDtoPageResult _$result;
    try {
      _$result = _$v ??
          _$LessonDtoPageResult._(
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
            r'LessonDtoPageResult', _$failedField, e.toString());
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
