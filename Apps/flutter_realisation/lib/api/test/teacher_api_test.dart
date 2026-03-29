import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for TeacherApi
void main() {
  final instance = MyApi().getTeacherApi();

  group(TeacherApi, () {
    // Профиль преподавателя
    //
    //Future<TeacherProfileDto> apiTeacherGet() async
    test('test apiTeacherGet', () async {
      // TODO
    });

  });
}
