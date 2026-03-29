import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for AdminCourseThemeApi
void main() {
  final instance = MyApi().getAdminCourseThemeApi();

  group(AdminCourseThemeApi, () {
    // Создать тему. Только для админов с правом EditCourseThemes
    //
    //Future<int> apiAdminThemesPost({ CourseThemeInputDto courseThemeInputDto }) async
    test('test apiAdminThemesPost', () async {
      // TODO
    });

    // Реадктирование темы курсов. Только для админов с правом EditCourseThemes
    //
    //Future<int> apiAdminThemesThemeIdPatch(int themeId, { CourseThemeInputDto courseThemeInputDto }) async
    test('test apiAdminThemesThemeIdPatch', () async {
      // TODO
    });

  });
}
