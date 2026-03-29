# my_api.api.PublicUserApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiPublicUserUserIdGet**](PublicUserApi.md#apipublicuseruseridget) | **GET** /api/public/user/{userId} | Информация о ЛЮБОМ пользователе


# **apiPublicUserUserIdGet**
> UserDtoLite apiPublicUserUserIdGet(userId)

Информация о ЛЮБОМ пользователе

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicUserApi();
final int userId = 56; // int | Id пользователя

try {
    final response = api.apiPublicUserUserIdGet(userId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicUserApi->apiPublicUserUserIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **userId** | **int**| Id пользователя | 

### Return type

[**UserDtoLite**](UserDtoLite.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

