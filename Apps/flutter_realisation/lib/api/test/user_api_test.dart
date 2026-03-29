import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for UserApi
void main() {
  final instance = MyApi().getUserApi();

  group(UserApi, () {
    // Подтвердить чат, отправив токен из него
    //
    //Future<int> apiUserConfirmChatPost({ String body }) async
    test('test apiUserConfirmChatPost', () async {
      // TODO
    });

    // Подтвердить почту, отправив пришедший в письме токен
    //
    //Future<int> apiUserConfirmEmailPost({ String body }) async
    test('test apiUserConfirmEmailPost', () async {
      // TODO
    });

    // Создать профиль ученика
    //
    //Future<int> apiUserCreateStudentProfilePost() async
    test('test apiUserCreateStudentProfilePost', () async {
      // TODO
    });

    // Создать профиль преподавателя
    //
    //Future<int> apiUserCreateTeacherProfilePost({ String body }) async
    test('test apiUserCreateTeacherProfilePost', () async {
      // TODO
    });

    // Полный профиль пользователя, включая информацию о том, является ли он учеником или преподавателем, подтверждена ли почта и т.д.
    //
    //Future<UserDto> apiUserGet() async
    test('test apiUserGet', () async {
      // TODO
    });

    // Отправить письмо с подтверждением почты (не сработает если почта уже подтверждена)
    //
    //Future<int> apiUserSendEmailVerificationPost({ String correctiveMail }) async
    test('test apiUserSendEmailVerificationPost', () async {
      // TODO
    });

  });
}
