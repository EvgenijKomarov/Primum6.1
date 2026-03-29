# my_api.api.PublicThemeApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiPublicThemesGet**](PublicThemeApi.md#apipublicthemesget) | **GET** /api/public/themes | Темы курсов
[**apiPublicThemesThemeIdGet**](PublicThemeApi.md#apipublicthemesthemeidget) | **GET** /api/public/themes/{themeId} | Конкретная тема


# **apiPublicThemesGet**
> CourseThemeDtoPageResult apiPublicThemesGet(page, pageSize)

Темы курсов

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicThemeApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicThemesGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicThemeApi->apiPublicThemesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**CourseThemeDtoPageResult**](CourseThemeDtoPageResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicThemesThemeIdGet**
> CourseThemeDto apiPublicThemesThemeIdGet(themeId)

Конкретная тема

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicThemeApi();
final int themeId = 56; // int | 

try {
    final response = api.apiPublicThemesThemeIdGet(themeId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicThemeApi->apiPublicThemesThemeIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **themeId** | **int**|  | 

### Return type

[**CourseThemeDto**](CourseThemeDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

