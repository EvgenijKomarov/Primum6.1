import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for StudentAbonementApi
void main() {
  final instance = MyApi().getStudentAbonementApi();

  group(StudentAbonementApi, () {
    // Удалить абонемент
    //
    //Future<int> apiStudentAbonementsAbonementIdDelete(int abonementId) async
    test('test apiStudentAbonementsAbonementIdDelete', () async {
      // TODO
    });

    // Конкретный абонемент ученика
    //
    //Future<AbonementDto> apiStudentAbonementsAbonementIdGet(int abonementId) async
    test('test apiStudentAbonementsAbonementIdGet', () async {
      // TODO
    });

    // Все занятия по абонементу, включая прошедшие и будущие
    //
    //Future<LessonDtoPageResult> apiStudentAbonementsAbonementIdLessonsGet(int abonementId, { int page, int pageSize }) async
    test('test apiStudentAbonementsAbonementIdLessonsGet', () async {
      // TODO
    });

    // Расписания ученика, привязанного к конкретному абонементу
    //
    //Future<AbonementSheduleDtoPageResult> apiStudentAbonementsAbonementIdShedulesGet(int abonementId, { int page, int pageSize }) async
    test('test apiStudentAbonementsAbonementIdShedulesGet', () async {
      // TODO
    });

    // Активировать абонемент или заморозить, чтобы занятия по нему генерировались, но не происходили
    //
    //Future<int> apiStudentAbonementsAbonementIdStatusPatch(int abonementId, { int body }) async
    test('test apiStudentAbonementsAbonementIdStatusPatch', () async {
      // TODO
    });

    // Все абонементы ученика
    //
    //Future<AbonementDtoPageResult> apiStudentAbonementsGet({ int page, int pageSize }) async
    test('test apiStudentAbonementsGet', () async {
      // TODO
    });

  });
}
