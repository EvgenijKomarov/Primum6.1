import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for TeacherCourseApi
void main() {
  final instance = MyApi().getTeacherCourseApi();

  group(TeacherCourseApi, () {
    // Активировать/деактивировать курс, чтобы он отображался в общем списке и на него могли подписываться ученики, либо скрыть его от учеников и не дать им на него подписаться
    //
    //Future<int> apiTeacherCoursesCourseIdActivityPatch(int courseId, { bool body }) async
    test('test apiTeacherCoursesCourseIdActivityPatch', () async {
      // TODO
    });

    // Конкретный курс преподавателя
    //
    //Future<CourseDto> apiTeacherCoursesCourseIdGet(int courseId) async
    test('test apiTeacherCoursesCourseIdGet', () async {
      // TODO
    });

    // Реадктирование курса. При изменении названия и описания, курс отправляется заново на процедуру утверждения и пропадает из видимости у остальных пользователей
    //
    //Future<int> apiTeacherCoursesCourseIdPut(int courseId, { CourseInputDto courseInputDto }) async
    test('test apiTeacherCoursesCourseIdPut', () async {
      // TODO
    });

    // Все курсы преподавателя
    //
    //Future<CourseDtoPageResult> apiTeacherCoursesGet({ int page, int pageSize }) async
    test('test apiTeacherCoursesGet', () async {
      // TODO
    });

    // Создать курс
    //
    //Future<int> apiTeacherCoursesPost({ CourseInputDto courseInputDto }) async
    test('test apiTeacherCoursesPost', () async {
      // TODO
    });

  });
}
