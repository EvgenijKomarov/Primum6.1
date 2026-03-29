# my_api.api.StudentPromocodeApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiStudentPromocodesAvailableGet**](StudentPromocodeApi.md#apistudentpromocodesavailableget) | **GET** /api/student/promocodes/available | Посмотреть доступные ученику к покупке промокоды
[**apiStudentPromocodesBuyPromocodeIdPost**](StudentPromocodeApi.md#apistudentpromocodesbuypromocodeidpost) | **POST** /api/student/promocodes/buy/{promocodeId} | Купить промокод
[**apiStudentPromocodesGet**](StudentPromocodeApi.md#apistudentpromocodesget) | **GET** /api/student/promocodes | Все купленные учеником промокоды
[**apiStudentPromocodesPromocodeIdGet**](StudentPromocodeApi.md#apistudentpromocodespromocodeidget) | **GET** /api/student/promocodes/{promocodeId} | Посмотреть конкретный купленный промокод


# **apiStudentPromocodesAvailableGet**
> PromocodeDtoPageResult apiStudentPromocodesAvailableGet(page, pageSize)

Посмотреть доступные ученику к покупке промокоды

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentPromocodeApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentPromocodesAvailableGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentPromocodeApi->apiStudentPromocodesAvailableGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**PromocodeDtoPageResult**](PromocodeDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentPromocodesBuyPromocodeIdPost**
> PromocodeDto apiStudentPromocodesBuyPromocodeIdPost(promocodeId)

Купить промокод

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentPromocodeApi();
final int promocodeId = 56; // int | 

try {
    final response = api.apiStudentPromocodesBuyPromocodeIdPost(promocodeId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentPromocodeApi->apiStudentPromocodesBuyPromocodeIdPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **promocodeId** | **int**|  | 

### Return type

[**PromocodeDto**](PromocodeDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentPromocodesGet**
> PromocodeDtoPageResult apiStudentPromocodesGet(page, pageSize)

Все купленные учеником промокоды

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentPromocodeApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentPromocodesGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentPromocodeApi->apiStudentPromocodesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**PromocodeDtoPageResult**](PromocodeDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentPromocodesPromocodeIdGet**
> PromocodeDto apiStudentPromocodesPromocodeIdGet(promocodeId)

Посмотреть конкретный купленный промокод

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentPromocodeApi();
final int promocodeId = 56; // int | 

try {
    final response = api.apiStudentPromocodesPromocodeIdGet(promocodeId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentPromocodeApi->apiStudentPromocodesPromocodeIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **promocodeId** | **int**|  | 

### Return type

[**PromocodeDto**](PromocodeDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

