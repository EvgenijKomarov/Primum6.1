// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'teacher_shedule_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$TeacherSheduleDtoPageResult extends TeacherSheduleDtoPageResult {
  @override
  final BuiltList<TeacherSheduleDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$TeacherSheduleDtoPageResult([
    void Function(TeacherSheduleDtoPageResultBuilder)? updates,
  ]) => (TeacherSheduleDtoPageResultBuilder()..update(updates))._build();

  _$TeacherSheduleDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  TeacherSheduleDtoPageResult rebuild(
    void Function(TeacherSheduleDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  TeacherSheduleDtoPageResultBuilder toBuilder() =>
      TeacherSheduleDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is TeacherSheduleDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'TeacherSheduleDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class TeacherSheduleDtoPageResultBuilder
    implements
        Builder<
          TeacherSheduleDtoPageResult,
          TeacherSheduleDtoPageResultBuilder
        > {
  _$TeacherSheduleDtoPageResult? _$v;

  ListBuilder<TeacherSheduleDto>? _items;
  ListBuilder<TeacherSheduleDto> get items =>
      _$this._items ??= ListBuilder<TeacherSheduleDto>();
  set items(ListBuilder<TeacherSheduleDto>? items) => _$this._items = items;

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

  TeacherSheduleDtoPageResultBuilder() {
    TeacherSheduleDtoPageResult._defaults(this);
  }

  TeacherSheduleDtoPageResultBuilder get _$this {
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
  void replace(TeacherSheduleDtoPageResult other) {
    _$v = other as _$TeacherSheduleDtoPageResult;
  }

  @override
  void update(void Function(TeacherSheduleDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  TeacherSheduleDtoPageResult build() => _build();

  _$TeacherSheduleDtoPageResult _build() {
    _$TeacherSheduleDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$TeacherSheduleDtoPageResult._(
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
          r'TeacherSheduleDtoPageResult',
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
