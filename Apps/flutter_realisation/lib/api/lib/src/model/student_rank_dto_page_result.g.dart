// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'student_rank_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$StudentRankDtoPageResult extends StudentRankDtoPageResult {
  @override
  final BuiltList<StudentRankDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$StudentRankDtoPageResult([
    void Function(StudentRankDtoPageResultBuilder)? updates,
  ]) => (StudentRankDtoPageResultBuilder()..update(updates))._build();

  _$StudentRankDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  StudentRankDtoPageResult rebuild(
    void Function(StudentRankDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  StudentRankDtoPageResultBuilder toBuilder() =>
      StudentRankDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is StudentRankDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'StudentRankDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class StudentRankDtoPageResultBuilder
    implements
        Builder<StudentRankDtoPageResult, StudentRankDtoPageResultBuilder> {
  _$StudentRankDtoPageResult? _$v;

  ListBuilder<StudentRankDto>? _items;
  ListBuilder<StudentRankDto> get items =>
      _$this._items ??= ListBuilder<StudentRankDto>();
  set items(ListBuilder<StudentRankDto>? items) => _$this._items = items;

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

  StudentRankDtoPageResultBuilder() {
    StudentRankDtoPageResult._defaults(this);
  }

  StudentRankDtoPageResultBuilder get _$this {
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
  void replace(StudentRankDtoPageResult other) {
    _$v = other as _$StudentRankDtoPageResult;
  }

  @override
  void update(void Function(StudentRankDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  StudentRankDtoPageResult build() => _build();

  _$StudentRankDtoPageResult _build() {
    _$StudentRankDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$StudentRankDtoPageResult._(
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
          r'StudentRankDtoPageResult',
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
