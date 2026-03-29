// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'promocode_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$PromocodeInputDto extends PromocodeInputDto {
  @override
  final String? code;
  @override
  final int coinsPrice;
  @override
  final String? title;
  @override
  final String? description;

  factory _$PromocodeInputDto(
          [void Function(PromocodeInputDtoBuilder)? updates]) =>
      (PromocodeInputDtoBuilder()..update(updates))._build();

  _$PromocodeInputDto._(
      {this.code, required this.coinsPrice, this.title, this.description})
      : super._();
  @override
  PromocodeInputDto rebuild(void Function(PromocodeInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  PromocodeInputDtoBuilder toBuilder() =>
      PromocodeInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is PromocodeInputDto &&
        code == other.code &&
        coinsPrice == other.coinsPrice &&
        title == other.title &&
        description == other.description;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, code.hashCode);
    _$hash = $jc(_$hash, coinsPrice.hashCode);
    _$hash = $jc(_$hash, title.hashCode);
    _$hash = $jc(_$hash, description.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'PromocodeInputDto')
          ..add('code', code)
          ..add('coinsPrice', coinsPrice)
          ..add('title', title)
          ..add('description', description))
        .toString();
  }
}

class PromocodeInputDtoBuilder
    implements Builder<PromocodeInputDto, PromocodeInputDtoBuilder> {
  _$PromocodeInputDto? _$v;

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

  PromocodeInputDtoBuilder() {
    PromocodeInputDto._defaults(this);
  }

  PromocodeInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _code = $v.code;
      _coinsPrice = $v.coinsPrice;
      _title = $v.title;
      _description = $v.description;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(PromocodeInputDto other) {
    _$v = other as _$PromocodeInputDto;
  }

  @override
  void update(void Function(PromocodeInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  PromocodeInputDto build() => _build();

  _$PromocodeInputDto _build() {
    final _$result = _$v ??
        _$PromocodeInputDto._(
          code: code,
          coinsPrice: BuiltValueNullFieldError.checkNotNull(
              coinsPrice, r'PromocodeInputDto', 'coinsPrice'),
          title: title,
          description: description,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
