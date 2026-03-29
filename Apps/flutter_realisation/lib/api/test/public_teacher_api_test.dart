import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for PublicTeacherApi
void main() {
  final instance = MyApi().getPublicTeacherApi();

  group(PublicTeacherApi, () {
    // Все доступные преподаватели
    //
    //Future<TeacherProfileDtoPageResult> apiPublicTeachersGet({ int page, int pageSize }) async
    test('test apiPublicTeachersGet', () async {
      // TODO
    });

    // Все курсы преподавателя
    //
    //Future<CourseDtoPageResult> apiPublicTeachersTeacherIdCoursesGet(int teacherId, { int page, int pageSize }) async
    test('test apiPublicTeachersTeacherIdCoursesGet', () async {
      // TODO
    });

    // Информация о преподавателе
    //
    //Future<TeacherProfileDto> apiPublicTeachersTeacherIdGet(int teacherId) async
    test('test apiPublicTeachersTeacherIdGet', () async {
      // TODO
    });

    // Все расписания преподавателя
    //
    //Future<TeacherSheduleDtoPageResult> apiPublicTeachersTeacherIdShedulesGet(int teacherId, { int page, int pageSize }) async
    test('test apiPublicTeachersTeacherIdShedulesGet', () async {
      // TODO
    });

  });
}
