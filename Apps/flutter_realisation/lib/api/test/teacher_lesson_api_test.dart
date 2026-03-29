import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for TeacherLessonApi
void main() {
  final instance = MyApi().getTeacherLessonApi();

  group(TeacherLessonApi, () {
    // Только будущие занятия
    //
    //Future<LessonDtoPageResult> apiTeacherLessonsFutureGet({ int page, int pageSize }) async
    test('test apiTeacherLessonsFutureGet', () async {
      // TODO
    });

    // Все занятия, включая прошедшие и будущие
    //
    //Future<LessonDtoPageResult> apiTeacherLessonsGet({ int page, int pageSize }) async
    test('test apiTeacherLessonsGet', () async {
      // TODO
    });

    // Конкретное занятие
    //
    //Future<LessonDto> apiTeacherLessonsLessonIdGet(int lessonId) async
    test('test apiTeacherLessonsLessonIdGet', () async {
      // TODO
    });

    // Выставить оценку занятию
    //
    //Future<int> apiTeacherLessonsLessonIdGradePost(int lessonId, { GradingInputDto gradingInputDto }) async
    test('test apiTeacherLessonsLessonIdGradePost', () async {
      // TODO
    });

  });
}
