import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for PublicUserApi
void main() {
  final instance = MyApi().getPublicUserApi();

  group(PublicUserApi, () {
    // Информация о ЛЮБОМ пользователе
    //
    //Future<UserDtoLite> apiPublicUserUserIdGet(int userId) async
    test('test apiPublicUserUserIdGet', () async {
      // TODO
    });

  });
}
