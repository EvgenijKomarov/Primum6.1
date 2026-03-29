import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for PublicApi
void main() {
  final instance = MyApi().getPublicApi();

  group(PublicApi, () {
    // Контроллер для авторизации. Возвращает JWT токен
    //
    //Future<String> apiPublicLoginPost({ LoggingInputDto loggingInputDto }) async
    test('test apiPublicLoginPost', () async {
      // TODO
    });

    // Регистрация. Возвращает при успехе JWT токен
    //
    //Future<String> apiPublicRegisterPost({ RegistrationInputDto registrationInputDto }) async
    test('test apiPublicRegisterPost', () async {
      // TODO
    });

  });
}
