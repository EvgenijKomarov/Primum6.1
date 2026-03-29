import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for StudentApi
void main() {
  final instance = MyApi().getStudentApi();

  group(StudentApi, () {
    // Профиль ученика, включая имя, количество монет и id пользователя
    //
    //Future<StudentProfileDto> apiStudentGet() async
    test('test apiStudentGet', () async {
      // TODO
    });

    // Подписаться на курс
    //
    //Future<int> apiStudentSubscribeCourseIdTeacherSheduleIdPost(int courseId, int teacherSheduleId) async
    test('test apiStudentSubscribeCourseIdTeacherSheduleIdPost', () async {
      // TODO
    });

  });
}
