import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for PublicRanksApi
void main() {
  final instance = MyApi().getPublicRanksApi();

  group(PublicRanksApi, () {
    // Все ранги курсов
    //
    //Future<CourseRankDtoPageResult> apiPublicRanksCourseGet({ int page, int pageSize }) async
    test('test apiPublicRanksCourseGet', () async {
      // TODO
    });

    // Все ранги учеников
    //
    //Future<StudentRankDtoPageResult> apiPublicRanksStudentGet({ int page, int pageSize }) async
    test('test apiPublicRanksStudentGet', () async {
      // TODO
    });

    // Все ранги преподавателей
    //
    //Future<TeacherRankDtoPageResult> apiPublicRanksTeacherGet({ int page, int pageSize }) async
    test('test apiPublicRanksTeacherGet', () async {
      // TODO
    });

  });
}
