//
// AUTO-GENERATED FILE, DO NOT MODIFY!
//

import 'dart:io';
import 'package:dio/dio.dart';
import 'package:dio/io.dart';
import 'package:built_value/serializer.dart';
import 'package:my_api/src/serializers.dart';
import 'package:my_api/src/auth/api_key_auth.dart';
import 'package:my_api/src/auth/basic_auth.dart';
import 'package:my_api/src/auth/bearer_auth.dart';
import 'package:my_api/src/auth/oauth.dart';
import 'package:my_api/src/api/admin_api.dart';
import 'package:my_api/src/api/admin_course_theme_api.dart';
import 'package:my_api/src/api/admin_incident_api.dart';
import 'package:my_api/src/api/admin_other_admins_api.dart';
import 'package:my_api/src/api/admin_promocode_api.dart';
import 'package:my_api/src/api/admin_user_api.dart';
import 'package:my_api/src/api/public_api.dart';
import 'package:my_api/src/api/public_course_api.dart';
import 'package:my_api/src/api/public_ranks_api.dart';
import 'package:my_api/src/api/public_teacher_api.dart';
import 'package:my_api/src/api/public_theme_api.dart';
import 'package:my_api/src/api/public_user_api.dart';
import 'package:my_api/src/api/student_api.dart';
import 'package:my_api/src/api/student_abonement_api.dart';
import 'package:my_api/src/api/student_lesson_api.dart';
import 'package:my_api/src/api/student_promocode_api.dart';
import 'package:my_api/src/api/student_shedule_api.dart';
import 'package:my_api/src/api/teacher_api.dart';
import 'package:my_api/src/api/teacher_abonement_api.dart';
import 'package:my_api/src/api/teacher_course_api.dart';
import 'package:my_api/src/api/teacher_lesson_api.dart';
import 'package:my_api/src/api/teacher_shedule_api.dart';
import 'package:my_api/src/api/user_api.dart';

class MyApi {
  static const String basePath = r'https://localhost:5002';

  final Dio dio;
  final Serializers serializers;

  MyApi({
    Dio? dio,
    Serializers? serializers,
    String? basePathOverride,
    List<Interceptor>? interceptors,
  }) : this.serializers = serializers ?? standardSerializers,
       this.dio =
           dio ??
           Dio(
             BaseOptions(
               // Always use the fixed base path for generated clients
               baseUrl: basePath,
               connectTimeout: const Duration(milliseconds: 5000),
               receiveTimeout: const Duration(milliseconds: 3000),
             ),
           ) {
    // Configure Dio to accept self-signed certificates for localhost HTTPS
    if (this.dio.httpClientAdapter is IOHttpClientAdapter) {
      (this.dio.httpClientAdapter as IOHttpClientAdapter).createHttpClient =
          () {
            final client = HttpClient();
            client.badCertificateCallback =
                (X509Certificate cert, String host, int port) => true;
            return client;
          };
    }

    if (interceptors == null) {
      this.dio.interceptors.addAll([
        OAuthInterceptor(),
        BasicAuthInterceptor(),
        BearerAuthInterceptor(),
        ApiKeyAuthInterceptor(),
      ]);
    } else {
      this.dio.interceptors.addAll(interceptors);
    }
  }

  void setOAuthToken(String name, String token) {
    if (this.dio.interceptors.any((i) => i is OAuthInterceptor)) {
      (this.dio.interceptors.firstWhere((i) => i is OAuthInterceptor)
                  as OAuthInterceptor)
              .tokens[name] =
          token;
    }
  }

  void setBearerAuth(String name, String token) {
    if (this.dio.interceptors.any((i) => i is BearerAuthInterceptor)) {
      (this.dio.interceptors.firstWhere((i) => i is BearerAuthInterceptor)
                  as BearerAuthInterceptor)
              .tokens[name] =
          token;
    }
  }

  void setBasicAuth(String name, String username, String password) {
    if (this.dio.interceptors.any((i) => i is BasicAuthInterceptor)) {
      (this.dio.interceptors.firstWhere((i) => i is BasicAuthInterceptor)
              as BasicAuthInterceptor)
          .authInfo[name] = BasicAuthInfo(
        username,
        password,
      );
    }
  }

  void setApiKey(String name, String apiKey) {
    if (this.dio.interceptors.any((i) => i is ApiKeyAuthInterceptor)) {
      (this.dio.interceptors.firstWhere(
                    (element) => element is ApiKeyAuthInterceptor,
                  )
                  as ApiKeyAuthInterceptor)
              .apiKeys[name] =
          apiKey;
    }
  }

  /// Get AdminApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  AdminApi getAdminApi() {
    return AdminApi(dio, serializers);
  }

  /// Get AdminCourseThemeApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  AdminCourseThemeApi getAdminCourseThemeApi() {
    return AdminCourseThemeApi(dio, serializers);
  }

  /// Get AdminIncidentApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  AdminIncidentApi getAdminIncidentApi() {
    return AdminIncidentApi(dio, serializers);
  }

  /// Get AdminOtherAdminsApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  AdminOtherAdminsApi getAdminOtherAdminsApi() {
    return AdminOtherAdminsApi(dio, serializers);
  }

  /// Get AdminPromocodeApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  AdminPromocodeApi getAdminPromocodeApi() {
    return AdminPromocodeApi(dio, serializers);
  }

  /// Get AdminUserApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  AdminUserApi getAdminUserApi() {
    return AdminUserApi(dio, serializers);
  }

  /// Get PublicApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  PublicApi getPublicApi() {
    return PublicApi(dio, serializers);
  }

  /// Get PublicCourseApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  PublicCourseApi getPublicCourseApi() {
    return PublicCourseApi(dio, serializers);
  }

  /// Get PublicRanksApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  PublicRanksApi getPublicRanksApi() {
    return PublicRanksApi(dio, serializers);
  }

  /// Get PublicTeacherApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  PublicTeacherApi getPublicTeacherApi() {
    return PublicTeacherApi(dio, serializers);
  }

  /// Get PublicThemeApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  PublicThemeApi getPublicThemeApi() {
    return PublicThemeApi(dio, serializers);
  }

  /// Get PublicUserApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  PublicUserApi getPublicUserApi() {
    return PublicUserApi(dio, serializers);
  }

  /// Get StudentApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  StudentApi getStudentApi() {
    return StudentApi(dio, serializers);
  }

  /// Get StudentAbonementApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  StudentAbonementApi getStudentAbonementApi() {
    return StudentAbonementApi(dio, serializers);
  }

  /// Get StudentLessonApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  StudentLessonApi getStudentLessonApi() {
    return StudentLessonApi(dio, serializers);
  }

  /// Get StudentPromocodeApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  StudentPromocodeApi getStudentPromocodeApi() {
    return StudentPromocodeApi(dio, serializers);
  }

  /// Get StudentSheduleApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  StudentSheduleApi getStudentSheduleApi() {
    return StudentSheduleApi(dio, serializers);
  }

  /// Get TeacherApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  TeacherApi getTeacherApi() {
    return TeacherApi(dio, serializers);
  }

  /// Get TeacherAbonementApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  TeacherAbonementApi getTeacherAbonementApi() {
    return TeacherAbonementApi(dio, serializers);
  }

  /// Get TeacherCourseApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  TeacherCourseApi getTeacherCourseApi() {
    return TeacherCourseApi(dio, serializers);
  }

  /// Get TeacherLessonApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  TeacherLessonApi getTeacherLessonApi() {
    return TeacherLessonApi(dio, serializers);
  }

  /// Get TeacherSheduleApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  TeacherSheduleApi getTeacherSheduleApi() {
    return TeacherSheduleApi(dio, serializers);
  }

  /// Get UserApi instance, base route and serializer can be overridden by a given but be careful,
  /// by doing that all interceptors will not be executed
  UserApi getUserApi() {
    return UserApi(dio, serializers);
  }
}
