import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for AdminIncidentApi
void main() {
  final instance = MyApi().getAdminIncidentApi();

  group(AdminIncidentApi, () {
    // Список всех инцидентов. Доступно всем
    //
    //Future<IncidentDtoPageResult> apiAdminIncidentsGet({ int page, int pageSize }) async
    test('test apiAdminIncidentsGet', () async {
      // TODO
    });

    // Посмотреть логи действий всех админов. Только для админов с правом InspectIncidentLogs
    //
    //Future<IncidentLogDtoPageResult> apiAdminIncidentsLogsGet({ bool onlyUnrevisioned, int page, int pageSize }) async
    test('test apiAdminIncidentsLogsGet', () async {
      // TODO
    });

    // Конкретный лог действия админа. Только для админов с правом InspectIncidentLogs
    //
    //Future<IncidentLogDto> apiAdminIncidentsLogsLogIdGet(int logId) async
    test('test apiAdminIncidentsLogsLogIdGet', () async {
      // TODO
    });

    // Отметить лог просмотренным (предполагается, что это делается кнопкой под карточкой лога)
    //
    //Future<int> apiAdminIncidentsLogsLogIdPatch(int logId) async
    test('test apiAdminIncidentsLogsLogIdPatch', () async {
      // TODO
    });

    // Решение инцидента. Доступно всем
    //
    //Future<int> apiAdminIncidentsPatch({ IncidentDecisionInputDto incidentDecisionInputDto }) async
    test('test apiAdminIncidentsPatch', () async {
      // TODO
    });

  });
}
