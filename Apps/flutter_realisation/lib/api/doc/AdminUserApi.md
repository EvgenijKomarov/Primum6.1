# my_api.api.AdminUserApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiAdminUsersGet**](AdminUserApi.md#apiadminusersget) | **GET** /api/admin/users | Список всех пользователей
[**apiAdminUsersObjectUserIdAdminProfilePost**](AdminUserApi.md#apiadminusersobjectuseridadminprofilepost) | **POST** /api/admin/users/{objectUserId}/admin-profile | Создать профиль админа пользователю. Только для админов с правом CreateAdminProfiles
[**apiAdminUsersObjectUserIdBanStatusPatch**](AdminUserApi.md#apiadminusersobjectuseridbanstatuspatch) | **PATCH** /api/admin/users/{objectUserId}/ban-status | Забанить/разбанить пользователя. Только для админов с правом ChangeBanStatus
[**apiAdminUsersObjectUserIdCashPatch**](AdminUserApi.md#apiadminusersobjectuseridcashpatch) | **PATCH** /api/admin/users/{objectUserId}/cash | Добавить (отнять при отрицательном значении cash) деньги у пользователя. Только для админов с правом AddCash
[**apiAdminUsersObjectUserIdGet**](AdminUserApi.md#apiadminusersobjectuseridget) | **GET** /api/admin/users/{objectUserId} | Информация о конкретном пользователе


# **apiAdminUsersGet**
> UserDtoPageResult apiAdminUsersGet(page, pageSize)

Список всех пользователей

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminUserApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiAdminUsersGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminUserApi->apiAdminUsersGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**UserDtoPageResult**](UserDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminUsersObjectUserIdAdminProfilePost**
> int apiAdminUsersObjectUserIdAdminProfilePost(objectUserId, status)

Создать профиль админа пользователю. Только для админов с правом CreateAdminProfiles

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminUserApi();
final int objectUserId = 56; // int | 
final String status = status_example; // String | Статус (просто приписка, ни на что не влияющая)

try {
    final response = api.apiAdminUsersObjectUserIdAdminProfilePost(objectUserId, status);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminUserApi->apiAdminUsersObjectUserIdAdminProfilePost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **objectUserId** | **int**|  | 
 **status** | **String**| Статус (просто приписка, ни на что не влияющая) | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminUsersObjectUserIdBanStatusPatch**
> int apiAdminUsersObjectUserIdBanStatusPatch(objectUserId, body)

Забанить/разбанить пользователя. Только для админов с правом ChangeBanStatus

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminUserApi();
final int objectUserId = 56; // int | 
final bool body = true; // bool | Статус бана

try {
    final response = api.apiAdminUsersObjectUserIdBanStatusPatch(objectUserId, body);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminUserApi->apiAdminUsersObjectUserIdBanStatusPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **objectUserId** | **int**|  | 
 **body** | **bool**| Статус бана | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminUsersObjectUserIdCashPatch**
> int apiAdminUsersObjectUserIdCashPatch(objectUserId, body)

Добавить (отнять при отрицательном значении cash) деньги у пользователя. Только для админов с правом AddCash

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminUserApi();
final int objectUserId = 56; // int | 
final int body = 56; // int | 

try {
    final response = api.apiAdminUsersObjectUserIdCashPatch(objectUserId, body);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminUserApi->apiAdminUsersObjectUserIdCashPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **objectUserId** | **int**|  | 
 **body** | **int**|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminUsersObjectUserIdGet**
> UserDto apiAdminUsersObjectUserIdGet(objectUserId)

Информация о конкретном пользователе

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminUserApi();
final int objectUserId = 56; // int | 

try {
    final response = api.apiAdminUsersObjectUserIdGet(objectUserId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminUserApi->apiAdminUsersObjectUserIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **objectUserId** | **int**|  | 

### Return type

[**UserDto**](UserDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

