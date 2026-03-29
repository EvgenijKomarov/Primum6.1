import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for StudentPromocodeApi
void main() {
  final instance = MyApi().getStudentPromocodeApi();

  group(StudentPromocodeApi, () {
    // Посмотреть доступные ученику к покупке промокоды
    //
    //Future<PromocodeDtoPageResult> apiStudentPromocodesAvailableGet({ int page, int pageSize }) async
    test('test apiStudentPromocodesAvailableGet', () async {
      // TODO
    });

    // Купить промокод
    //
    //Future<PromocodeDto> apiStudentPromocodesBuyPromocodeIdPost(int promocodeId) async
    test('test apiStudentPromocodesBuyPromocodeIdPost', () async {
      // TODO
    });

    // Все купленные учеником промокоды
    //
    //Future<PromocodeDtoPageResult> apiStudentPromocodesGet({ int page, int pageSize }) async
    test('test apiStudentPromocodesGet', () async {
      // TODO
    });

    // Посмотреть конкретный купленный промокод
    //
    //Future<PromocodeDto> apiStudentPromocodesPromocodeIdGet(int promocodeId) async
    test('test apiStudentPromocodesPromocodeIdGet', () async {
      // TODO
    });

  });
}
