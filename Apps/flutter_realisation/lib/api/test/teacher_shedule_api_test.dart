import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for TeacherSheduleApi
void main() {
  final instance = MyApi().getTeacherSheduleApi();

  group(TeacherSheduleApi, () {
    // Все расписания преподавателя
    //
    //Future<TeacherSheduleDtoPageResult> apiTeacherShedulesGet({ int page, int pageSize }) async
    test('test apiTeacherShedulesGet', () async {
      // TODO
    });

    // Создать расписание преподавателя
    //
    //Future<int> apiTeacherShedulesPost({ TeacherSheduleInputDto teacherSheduleInputDto }) async
    test('test apiTeacherShedulesPost', () async {
      // TODO
    });

    // Удалить расписание. Оно не удалится, если к расписанию преподавателя привязано расписание ученика
    //
    //Future<int> apiTeacherShedulesSheduleIdDelete(int sheduleId) async
    test('test apiTeacherShedulesSheduleIdDelete', () async {
      // TODO
    });

    // Конкретное расписание
    //
    //Future<TeacherSheduleDto> apiTeacherShedulesSheduleIdGet(int sheduleId) async
    test('test apiTeacherShedulesSheduleIdGet', () async {
      // TODO
    });

  });
}
