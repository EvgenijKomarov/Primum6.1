import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for PublicCourseApi
void main() {
  final instance = MyApi().getPublicCourseApi();

  group(PublicCourseApi, () {
    // Курсы по теме
    //
    //Future<CourseDtoPageResult> apiPublicCoursesByThemeThemeIdGet(int themeId, { int page, int pageSize }) async
    test('test apiPublicCoursesByThemeThemeIdGet', () async {
      // TODO
    });

    // Конкретный курс
    //
    //Future<CourseDto> apiPublicCoursesCourseIdGet(int courseId) async
    test('test apiPublicCoursesCourseIdGet', () async {
      // TODO
    });

    // Все курсы
    //
    //Future<CourseDtoPageResult> apiPublicCoursesGet({ int page, int pageSize }) async
    test('test apiPublicCoursesGet', () async {
      // TODO
    });

  });
}
