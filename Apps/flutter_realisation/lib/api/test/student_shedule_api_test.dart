import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for StudentSheduleApi
void main() {
  final instance = MyApi().getStudentSheduleApi();

  group(StudentSheduleApi, () {
    // Все расписания ученика, на которые генерируются занятия
    //
    //Future<AbonementSheduleDtoPageResult> apiStudentShedulesGet({ int page, int pageSize }) async
    test('test apiStudentShedulesGet', () async {
      // TODO
    });

    // Удалить расписание
    //
    //Future<int> apiStudentShedulesSheduleIdDelete(int abonementSheduleId, String sheduleId) async
    test('test apiStudentShedulesSheduleIdDelete', () async {
      // TODO
    });

    // Конкретное расписание ученика
    //
    //Future<AbonementSheduleDto> apiStudentShedulesSheduleIdGet(int sheduleId) async
    test('test apiStudentShedulesSheduleIdGet', () async {
      // TODO
    });

  });
}
