import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for PublicThemeApi
void main() {
  final instance = MyApi().getPublicThemeApi();

  group(PublicThemeApi, () {
    // Темы курсов
    //
    //Future<CourseThemeDtoPageResult> apiPublicThemesGet({ int page, int pageSize }) async
    test('test apiPublicThemesGet', () async {
      // TODO
    });

    // Конкретная тема
    //
    //Future<CourseThemeDto> apiPublicThemesThemeIdGet(int themeId) async
    test('test apiPublicThemesThemeIdGet', () async {
      // TODO
    });

  });
}
