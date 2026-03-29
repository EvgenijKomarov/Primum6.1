# my_api.api.AdminPromocodeApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiAdminPromocodesGet**](AdminPromocodeApi.md#apiadminpromocodesget) | **GET** /api/admin/promocodes | Посмотреть все промокоды (даже купленные)
[**apiAdminPromocodesPost**](AdminPromocodeApi.md#apiadminpromocodespost) | **POST** /api/admin/promocodes | Добавить промокод. Только для админов с правом AddPromocodes
[**apiAdminPromocodesPromocodeIdDelete**](AdminPromocodeApi.md#apiadminpromocodespromocodeiddelete) | **DELETE** /api/admin/promocodes/{promocodeId} | Удалить промокод. Только для админов с правом DeletePromocodes
[**apiAdminPromocodesPromocodeIdGet**](AdminPromocodeApi.md#apiadminpromocodespromocodeidget) | **GET** /api/admin/promocodes/{promocodeId} | Конкретный промокод


# **apiAdminPromocodesGet**
> PromocodeDtoPageResult apiAdminPromocodesGet(page, pageSize)

Посмотреть все промокоды (даже купленные)

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminPromocodeApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiAdminPromocodesGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminPromocodeApi->apiAdminPromocodesGet: $e\n');
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

# **apiAdminPromocodesPost**
> int apiAdminPromocodesPost(promocodeInputDto)

Добавить промокод. Только для админов с правом AddPromocodes

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminPromocodeApi();
final PromocodeInputDto promocodeInputDto = ; // PromocodeInputDto | 

try {
    final response = api.apiAdminPromocodesPost(promocodeInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminPromocodeApi->apiAdminPromocodesPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **promocodeInputDto** | [**PromocodeInputDto**](PromocodeInputDto.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminPromocodesPromocodeIdDelete**
> int apiAdminPromocodesPromocodeIdDelete(promocodeId)

Удалить промокод. Только для админов с правом DeletePromocodes

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminPromocodeApi();
final int promocodeId = 56; // int | 

try {
    final response = api.apiAdminPromocodesPromocodeIdDelete(promocodeId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminPromocodeApi->apiAdminPromocodesPromocodeIdDelete: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **promocodeId** | **int**|  | 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminPromocodesPromocodeIdGet**
> PromocodeDto apiAdminPromocodesPromocodeIdGet(promocodeId)

Конкретный промокод

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminPromocodeApi();
final int promocodeId = 56; // int | 

try {
    final response = api.apiAdminPromocodesPromocodeIdGet(promocodeId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminPromocodeApi->apiAdminPromocodesPromocodeIdGet: $e\n');
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

