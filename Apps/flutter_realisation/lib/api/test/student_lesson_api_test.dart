import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for StudentLessonApi
void main() {
  final instance = MyApi().getStudentLessonApi();

  group(StudentLessonApi, () {
    // Только будущие занятия
    //
    //Future<LessonDtoPageResult> apiStudentLessonsFutureGet({ int page, int pageSize }) async
    test('test apiStudentLessonsFutureGet', () async {
      // TODO
    });

    // Все занятия ученика, включая прошедшие и будущие
    //
    //Future<LessonDtoPageResult> apiStudentLessonsGet({ int page, int pageSize }) async
    test('test apiStudentLessonsGet', () async {
      // TODO
    });

    // Конкретное занятие
    //
    //Future<LessonDto> apiStudentLessonsLessonIdGet(int lessonId) async
    test('test apiStudentLessonsLessonIdGet', () async {
      // TODO
    });

  });
}
