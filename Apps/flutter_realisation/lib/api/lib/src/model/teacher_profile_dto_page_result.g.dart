// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'teacher_profile_dto_page_result.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$TeacherProfileDtoPageResult extends TeacherProfileDtoPageResult {
  @override
  final BuiltList<TeacherProfileDto>? items;
  @override
  final int? totalItemsCount;
  @override
  final int? totalPages;
  @override
  final int? currentPage;

  factory _$TeacherProfileDtoPageResult([
    void Function(TeacherProfileDtoPageResultBuilder)? updates,
  ]) => (TeacherProfileDtoPageResultBuilder()..update(updates))._build();

  _$TeacherProfileDtoPageResult._({
    this.items,
    this.totalItemsCount,
    this.totalPages,
    this.currentPage,
  }) : super._();
  @override
  TeacherProfileDtoPageResult rebuild(
    void Function(TeacherProfileDtoPageResultBuilder) updates,
  ) => (toBuilder()..update(updates)).build();

  @override
  TeacherProfileDtoPageResultBuilder toBuilder() =>
      TeacherProfileDtoPageResultBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is TeacherProfileDtoPageResult &&
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
    return (newBuiltValueToStringHelper(r'TeacherProfileDtoPageResult')
          ..add('items', items)
          ..add('totalItemsCount', totalItemsCount)
          ..add('totalPages', totalPages)
          ..add('currentPage', currentPage))
        .toString();
  }
}

class TeacherProfileDtoPageResultBuilder
    implements
        Builder<
          TeacherProfileDtoPageResult,
          TeacherProfileDtoPageResultBuilder
        > {
  _$TeacherProfileDtoPageResult? _$v;

  ListBuilder<TeacherProfileDto>? _items;
  ListBuilder<TeacherProfileDto> get items =>
      _$this._items ??= ListBuilder<TeacherProfileDto>();
  set items(ListBuilder<TeacherProfileDto>? items) => _$this._items = items;

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

  TeacherProfileDtoPageResultBuilder() {
    TeacherProfileDtoPageResult._defaults(this);
  }

  TeacherProfileDtoPageResultBuilder get _$this {
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
  void replace(TeacherProfileDtoPageResult other) {
    _$v = other as _$TeacherProfileDtoPageResult;
  }

  @override
  void update(void Function(TeacherProfileDtoPageResultBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  TeacherProfileDtoPageResult build() => _build();

  _$TeacherProfileDtoPageResult _build() {
    _$TeacherProfileDtoPageResult _$result;
    try {
      _$result =
          _$v ??
          _$TeacherProfileDtoPageResult._(
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
          r'TeacherProfileDtoPageResult',
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
