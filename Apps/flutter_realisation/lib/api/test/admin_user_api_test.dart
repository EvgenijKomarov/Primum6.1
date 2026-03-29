import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for AdminUserApi
void main() {
  final instance = MyApi().getAdminUserApi();

  group(AdminUserApi, () {
    // Список всех пользователей
    //
    //Future<UserDtoPageResult> apiAdminUsersGet({ int page, int pageSize }) async
    test('test apiAdminUsersGet', () async {
      // TODO
    });

    // Создать профиль админа пользователю. Только для админов с правом CreateAdminProfiles
    //
    //Future<int> apiAdminUsersObjectUserIdAdminProfilePost(int objectUserId, { String status }) async
    test('test apiAdminUsersObjectUserIdAdminProfilePost', () async {
      // TODO
    });

    // Забанить/разбанить пользователя. Только для админов с правом ChangeBanStatus
    //
    //Future<int> apiAdminUsersObjectUserIdBanStatusPatch(int objectUserId, { bool body }) async
    test('test apiAdminUsersObjectUserIdBanStatusPatch', () async {
      // TODO
    });

    // Добавить (отнять при отрицательном значении cash) деньги у пользователя. Только для админов с правом AddCash
    //
    //Future<int> apiAdminUsersObjectUserIdCashPatch(int objectUserId, { int body }) async
    test('test apiAdminUsersObjectUserIdCashPatch', () async {
      // TODO
    });

    // Информация о конкретном пользователе
    //
    //Future<UserDto> apiAdminUsersObjectUserIdGet(int objectUserId) async
    test('test apiAdminUsersObjectUserIdGet', () async {
      // TODO
    });

  });
}
