// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'grading_input_dto.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

class _$GradingInputDto extends GradingInputDto {
  @override
  final Grading? homeworkGrade;
  @override
  final Grading? lessonActivityGrade;
  @override
  final Grading? repetitionOfMaterialGrade;
  @override
  final Grading? studyInitiativeGrade;

  factory _$GradingInputDto([void Function(GradingInputDtoBuilder)? updates]) =>
      (GradingInputDtoBuilder()..update(updates))._build();

  _$GradingInputDto._(
      {this.homeworkGrade,
      this.lessonActivityGrade,
      this.repetitionOfMaterialGrade,
      this.studyInitiativeGrade})
      : super._();
  @override
  GradingInputDto rebuild(void Function(GradingInputDtoBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  GradingInputDtoBuilder toBuilder() => GradingInputDtoBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is GradingInputDto &&
        homeworkGrade == other.homeworkGrade &&
        lessonActivityGrade == other.lessonActivityGrade &&
        repetitionOfMaterialGrade == other.repetitionOfMaterialGrade &&
        studyInitiativeGrade == other.studyInitiativeGrade;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, homeworkGrade.hashCode);
    _$hash = $jc(_$hash, lessonActivityGrade.hashCode);
    _$hash = $jc(_$hash, repetitionOfMaterialGrade.hashCode);
    _$hash = $jc(_$hash, studyInitiativeGrade.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'GradingInputDto')
          ..add('homeworkGrade', homeworkGrade)
          ..add('lessonActivityGrade', lessonActivityGrade)
          ..add('repetitionOfMaterialGrade', repetitionOfMaterialGrade)
          ..add('studyInitiativeGrade', studyInitiativeGrade))
        .toString();
  }
}

class GradingInputDtoBuilder
    implements Builder<GradingInputDto, GradingInputDtoBuilder> {
  _$GradingInputDto? _$v;

  Grading? _homeworkGrade;
  Grading? get homeworkGrade => _$this._homeworkGrade;
  set homeworkGrade(Grading? homeworkGrade) =>
      _$this._homeworkGrade = homeworkGrade;

  Grading? _lessonActivityGrade;
  Grading? get lessonActivityGrade => _$this._lessonActivityGrade;
  set lessonActivityGrade(Grading? lessonActivityGrade) =>
      _$this._lessonActivityGrade = lessonActivityGrade;

  Grading? _repetitionOfMaterialGrade;
  Grading? get repetitionOfMaterialGrade => _$this._repetitionOfMaterialGrade;
  set repetitionOfMaterialGrade(Grading? repetitionOfMaterialGrade) =>
      _$this._repetitionOfMaterialGrade = repetitionOfMaterialGrade;

  Grading? _studyInitiativeGrade;
  Grading? get studyInitiativeGrade => _$this._studyInitiativeGrade;
  set studyInitiativeGrade(Grading? studyInitiativeGrade) =>
      _$this._studyInitiativeGrade = studyInitiativeGrade;

  GradingInputDtoBuilder() {
    GradingInputDto._defaults(this);
  }

  GradingInputDtoBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _homeworkGrade = $v.homeworkGrade;
      _lessonActivityGrade = $v.lessonActivityGrade;
      _repetitionOfMaterialGrade = $v.repetitionOfMaterialGrade;
      _studyInitiativeGrade = $v.studyInitiativeGrade;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(GradingInputDto other) {
    _$v = other as _$GradingInputDto;
  }

  @override
  void update(void Function(GradingInputDtoBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  GradingInputDto build() => _build();

  _$GradingInputDto _build() {
    final _$result = _$v ??
        _$GradingInputDto._(
          homeworkGrade: homeworkGrade,
          lessonActivityGrade: lessonActivityGrade,
          repetitionOfMaterialGrade: repetitionOfMaterialGrade,
          studyInitiativeGrade: studyInitiativeGrade,
        );
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
