//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

// ignore_for_file: unused_import

import 'package:one_of_serializer/any_of_serializer.dart';
import 'package:one_of_serializer/one_of_serializer.dart';
import 'package:built_collection/built_collection.dart';
import 'package:built_value/json_object.dart';
import 'package:built_value/serializer.dart';
import 'package:built_value/standard_json_plugin.dart';
import 'package:built_value/iso_8601_date_time_serializer.dart';
import 'package:my_api/src/date_serializer.dart';
import 'package:my_api/src/model/date.dart';

import 'package:my_api/src/model/abonement_dto.dart';
import 'package:my_api/src/model/abonement_dto_page_result.dart';
import 'package:my_api/src/model/abonement_input_status.dart';
import 'package:my_api/src/model/abonement_shedule_dto.dart';
import 'package:my_api/src/model/abonement_shedule_dto_page_result.dart';
import 'package:my_api/src/model/abonement_status.dart';
import 'package:my_api/src/model/admin_profile_dto.dart';
import 'package:my_api/src/model/admin_profile_dto_page_result.dart';
import 'package:my_api/src/model/approve_status.dart';
import 'package:my_api/src/model/course_dto.dart';
import 'package:my_api/src/model/course_dto_page_result.dart';
import 'package:my_api/src/model/course_input_dto.dart';
import 'package:my_api/src/model/course_rank_dto.dart';
import 'package:my_api/src/model/course_rank_dto_page_result.dart';
import 'package:my_api/src/model/course_theme_dto.dart';
import 'package:my_api/src/model/course_theme_dto_page_result.dart';
import 'package:my_api/src/model/course_theme_input_dto.dart';
import 'package:my_api/src/model/day_of_week.dart';
import 'package:my_api/src/model/grading.dart';
import 'package:my_api/src/model/grading_input_dto.dart';
import 'package:my_api/src/model/incident_decision.dart';
import 'package:my_api/src/model/incident_decision_input_dto.dart';
import 'package:my_api/src/model/incident_dto.dart';
import 'package:my_api/src/model/incident_dto_page_result.dart';
import 'package:my_api/src/model/incident_log_dto.dart';
import 'package:my_api/src/model/incident_log_dto_page_result.dart';
import 'package:my_api/src/model/incident_meaning.dart';
import 'package:my_api/src/model/incident_status.dart';
import 'package:my_api/src/model/lesson_dto.dart';
import 'package:my_api/src/model/lesson_dto_page_result.dart';
import 'package:my_api/src/model/lesson_status.dart';
import 'package:my_api/src/model/logging_input_dto.dart';
import 'package:my_api/src/model/permission.dart';
import 'package:my_api/src/model/promocode_dto.dart';
import 'package:my_api/src/model/promocode_dto_page_result.dart';
import 'package:my_api/src/model/promocode_input_dto.dart';
import 'package:my_api/src/model/registration_input_dto.dart';
import 'package:my_api/src/model/student_profile_dto.dart';
import 'package:my_api/src/model/student_rank_dto.dart';
import 'package:my_api/src/model/student_rank_dto_page_result.dart';
import 'package:my_api/src/model/teacher_profile_dto.dart';
import 'package:my_api/src/model/teacher_profile_dto_page_result.dart';
import 'package:my_api/src/model/teacher_rank_dto.dart';
import 'package:my_api/src/model/teacher_rank_dto_page_result.dart';
import 'package:my_api/src/model/teacher_shedule_dto.dart';
import 'package:my_api/src/model/teacher_shedule_dto_page_result.dart';
import 'package:my_api/src/model/teacher_shedule_input_dto.dart';
import 'package:my_api/src/model/user_dto.dart';
import 'package:my_api/src/model/user_dto_lite.dart';
import 'package:my_api/src/model/user_dto_page_result.dart';

part 'serializers.g.dart';

@SerializersFor([
  AbonementDto,
  AbonementDtoPageResult,
  AbonementInputStatus,
  AbonementSheduleDto,
  AbonementSheduleDtoPageResult,
  AbonementStatus,
  AdminProfileDto,
  AdminProfileDtoPageResult,
  ApproveStatus,
  CourseDto,
  CourseDtoPageResult,
  CourseInputDto,
  CourseRankDto,
  CourseRankDtoPageResult,
  CourseThemeDto,
  CourseThemeDtoPageResult,
  CourseThemeInputDto,
  DayOfWeek,
  Grading,
  GradingInputDto,
  IncidentDecision,
  IncidentDecisionInputDto,
  IncidentDto,
  IncidentDtoPageResult,
  IncidentLogDto,
  IncidentLogDtoPageResult,
  IncidentMeaning,
  IncidentStatus,
  LessonDto,
  LessonDtoPageResult,
  LessonStatus,
  LoggingInputDto,
  Permission,
  PromocodeDto,
  PromocodeDtoPageResult,
  PromocodeInputDto,
  RegistrationInputDto,
  StudentProfileDto,
  StudentRankDto,
  StudentRankDtoPageResult,
  TeacherProfileDto,
  TeacherProfileDtoPageResult,
  TeacherRankDto,
  TeacherRankDtoPageResult,
  TeacherSheduleDto,
  TeacherSheduleDtoPageResult,
  TeacherSheduleInputDto,
  UserDto,
  UserDtoLite,
  UserDtoPageResult,
])
Serializers serializers = (_$serializers.toBuilder()
      ..addBuilderFactory(
        const FullType(BuiltMap, [FullType(String), FullType(bool)]),
        () => MapBuilder<String, bool>(),
      )
      ..add(const OneOfSerializer())
      ..add(const AnyOfSerializer())
      ..add(const DateSerializer())
      ..add(Iso8601DateTimeSerializer())
    ).build();

Serializers standardSerializers =
    (serializers.toBuilder()..addPlugin(StandardJsonPlugin())).build();
