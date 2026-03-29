# my_api.api.AdminApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiAdminGet**](AdminApi.md#apiadminget) | **GET** /api/admin | Профиль админа


# **apiAdminGet**
> AdminProfileDto apiAdminGet()

Профиль админа

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminApi();

try {
    final response = api.apiAdminGet();
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminApi->apiAdminGet: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**AdminProfileDto**](AdminProfileDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

