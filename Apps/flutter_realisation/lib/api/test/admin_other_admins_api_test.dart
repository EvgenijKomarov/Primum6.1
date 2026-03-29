import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for AdminOtherAdminsApi
void main() {
  final instance = MyApi().getAdminOtherAdminsApi();

  group(AdminOtherAdminsApi, () {
    // Список всех админов
    //
    //Future<AdminProfileDtoPageResult> apiAdminOtherAdminsGet({ int page, int pageSize }) async
    test('test apiAdminOtherAdminsGet', () async {
      // TODO
    });

    // Удалить профиль админа у пользователя. Только для админов с правом CreateAdminProfiles
    //
    //Future<int> apiAdminOtherAdminsObjectUserIdDelete(int objectUserId) async
    test('test apiAdminOtherAdminsObjectUserIdDelete', () async {
      // TODO
    });

    // Конкретный админ
    //
    //Future<AdminProfileDto> apiAdminOtherAdminsObjectUserIdGet(int objectUserId) async
    test('test apiAdminOtherAdminsObjectUserIdGet', () async {
      // TODO
    });

    // Редактирование прав админа. Только для админов с правом EditPermissions
    //
    //Future<int> apiAdminOtherAdminsObjectUserIdPermissionsPatch(int objectUserId, { BuiltMap<String, bool> requestBody }) async
    test('test apiAdminOtherAdminsObjectUserIdPermissionsPatch', () async {
      // TODO
    });

  });
}
