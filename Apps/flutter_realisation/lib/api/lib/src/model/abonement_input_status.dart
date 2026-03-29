//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_element
import 'package:built_collection/built_collection.dart';
import 'package:built_value/built_value.dart';
import 'package:built_value/serializer.dart';

part 'abonement_input_status.g.dart';

class AbonementInputStatus extends EnumClass {

  @BuiltValueEnumConst(wireNumber: 0)
  static const AbonementInputStatus number0 = _$number0;
  @BuiltValueEnumConst(wireNumber: 1)
  static const AbonementInputStatus number1 = _$number1;

  static Serializer<AbonementInputStatus> get serializer => _$abonementInputStatusSerializer;

  const AbonementInputStatus._(String name): super(name);

  static BuiltSet<AbonementInputStatus> get values => _$values;
  static AbonementInputStatus valueOf(String name) => _$valueOf(name);
}

/// Optionally, enum_class can generate a mixin to go with your enum for use
/// with Angular. It exposes your enum constants as getters. So, if you mix it
/// in to your Dart component class, the values become available to the
/// corresponding Angular template.
///
/// Trigger mixin generation by writing a line like this one next to your enum.
abstract class AbonementInputStatusMixin = Object with _$AbonementInputStatusMixin;

