import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for TeacherAbonementApi
void main() {
  final instance = MyApi().getTeacherAbonementApi();

  group(TeacherAbonementApi, () {
    // Конкретный абонемент ученика, подписанного на курс преподавателя
    //
    //Future<AbonementDto> apiTeacherAbonementsAbonementIdGet(int abonementId) async
    test('test apiTeacherAbonementsAbonementIdGet', () async {
      // TODO
    });

    // Все занятия абонемента ученика, подписанного на курс преподавателя, и прошедшие и будущие
    //
    //Future<LessonDtoPageResult> apiTeacherAbonementsAbonementIdLessonsGet(int abonementId, { int page, int pageSize }) async
    test('test apiTeacherAbonementsAbonementIdLessonsGet', () async {
      // TODO
    });

    // Расписания абонемента ученика, подписанного на курс преподавателя
    //
    //Future<AbonementSheduleDtoPageResult> apiTeacherAbonementsAbonementIdShedulesGet(int abonementId, { int page, int pageSize }) async
    test('test apiTeacherAbonementsAbonementIdShedulesGet', () async {
      // TODO
    });

    // Все абонементы учеников, подписанные на курсы преподавателя
    //
    //Future<AbonementDtoPageResult> apiTeacherAbonementsGet({ int page, int pageSize }) async
    test('test apiTeacherAbonementsGet', () async {
      // TODO
    });

  });
}
