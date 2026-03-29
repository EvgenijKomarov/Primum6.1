import 'package:test/test.dart';
import 'package:my_api/my_api.dart';


/// tests for AdminPromocodeApi
void main() {
  final instance = MyApi().getAdminPromocodeApi();

  group(AdminPromocodeApi, () {
    // Посмотреть все промокоды (даже купленные)
    //
    //Future<PromocodeDtoPageResult> apiAdminPromocodesGet({ int page, int pageSize }) async
    test('test apiAdminPromocodesGet', () async {
      // TODO
    });

    // Добавить промокод. Только для админов с правом AddPromocodes
    //
    //Future<int> apiAdminPromocodesPost({ PromocodeInputDto promocodeInputDto }) async
    test('test apiAdminPromocodesPost', () async {
      // TODO
    });

    // Удалить промокод. Только для админов с правом DeletePromocodes
    //
    //Future<int> apiAdminPromocodesPromocodeIdDelete(int promocodeId) async
    test('test apiAdminPromocodesPromocodeIdDelete', () async {
      // TODO
    });

    // Конкретный промокод
    //
    //Future<PromocodeDto> apiAdminPromocodesPromocodeIdGet(int promocodeId) async
    test('test apiAdminPromocodesPromocodeIdGet', () async {
      // TODO
    });

  });
}
