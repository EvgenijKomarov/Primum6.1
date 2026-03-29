import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for AdminApi
void main() {
  final instance = MyApi().getAdminApi();

  group(AdminApi, () {
    // Профиль админа
    //
    //Future<AdminProfileDto> apiAdminGet() async
    test('test apiAdminGet', () async {
      // TODO
    });

  });
}
