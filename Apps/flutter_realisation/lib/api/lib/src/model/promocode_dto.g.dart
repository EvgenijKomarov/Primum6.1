// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'promocode_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$PromocodeDto extends PromocodeDto {
  @override
  final int id;
  @override
  final int? studentId;
  @override
  final String? code;
  @override
  final int coinsPrice;
  @override
  final String? title;
  @override
  final String? description;
  @override
  final bool isAvailable;

  factory _$PromocodeDto([void Function(PromocodeDtoBuilder)? updates]) =>
      (PromocodeDtoBuilder()..update(updates))._build();

  _$PromocodeDto._({
    required this.id,
    this.studentId,
    this.code,
    required this.coinsPrice,
    this.title,
    this.description,
    required this.isAvailable,
  }) : super._();
  @override
  PromocodeDto rebuild(void Function(PromocodeDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  PromocodeDtoBuilder toBuilder() => PromocodeDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is PromocodeDto &&
        id == other.id &&
        studentId == other.studentId &&
        code == other.code &&
        coinsPrice == other.coinsPrice &&
        title == other.title &&
        description == other.description &&
        isAvailable == other.isAvailable;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, id.hashCode);
    _$hash = $jc(_$hash, studentId.hashCode);
    _$hash = $jc(_$hash, code.hashCode);
    _$hash = $jc(_$hash, coinsPrice.hashCode);
    _$hash = $jc(_$hash, title.hashCode);
    _$hash = $jc(_$hash, description.hashCode);
    _$hash = $jc(_$hash, isAvailable.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'PromocodeDto')
          ..add('id', id)
          ..add('studentId', studentId)
          ..add('code', code)
          ..add('coinsPrice', coinsPrice)
          ..add('title', title)
          ..add('description', description)
          ..add('isAvailable', isAvailable))
        .toString();
  }
}

class PromocodeDtoBuilder
    implements Builder<PromocodeDto, PromocodeDtoBuilder> {
  _$PromocodeDto? _$v;

  int? _id;
  int? get id => _$this._id;
  set id(int? id) => _$this._id = id;

  int? _studentId;
  int? get studentId => _$this._studentId;
  set studentId(int? studentId) => _$this._studentId = studentId;

  String? _code;
  String? get code => _$this._code;
  set code(String? code) => _$this._code = code;

  int? _coinsPrice;
  int? get coinsPrice => _$this._coinsPrice;
  set coinsPrice(int? coinsPrice) => _$this._coinsPrice = coinsPrice;

  String? _title;
  String? get title => _$this._title;
  set title(String? title) => _$this._title = title;

  String? _description;
  String? get description => _$this._description;
  set description(String? description) => _$this._description = description;

  bool? _isAvailable;
  bool? get isAvailable => _$this._isAvailable;
  set isAvailable(bool? isAvailable) => _$this._isAvailable = isAvailable;

  PromocodeDtoBuilder() {
    PromocodeDto._defaults(this);
  }

  PromocodeDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _id = $v.id;
      _studentId = $v.studentId;
      _code = $v.code;
      _coinsPrice = $v.coinsPrice;
      _title = $v.title;
      _description = $v.description;
      _isAvailable = $v.isAvailable;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(PromocodeDto other) {
    _$v = other as _$PromocodeDto;
  }

  @override
  void update(void Function(PromocodeDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  PromocodeDto build() => _build();

  _$PromocodeDto _build() {
    final _$result =
        _$v ??
        _$PromocodeDto._(
          id: BuiltValueNullFieldError.checkNotNull(id, r'PromocodeDto', 'id'),
          studentId: studentId,
          code: code,
          coinsPrice: BuiltValueNullFieldError.checkNotNull(
            coinsPrice,
            r'PromocodeDto',
            'coinsPrice',
          ),
          title: title,
          description: description,
          isAvailable: BuiltValueNullFieldError.checkNotNull(
            isAvailable,
            r'PromocodeDto',
            'isAvailable',
          ),
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
