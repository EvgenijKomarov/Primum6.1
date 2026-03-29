# my_api.api.AdminOtherAdminsApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiAdminOtherAdminsGet**](AdminOtherAdminsApi.md#apiadminotheradminsget) | **GET** /api/admin/other-admins | Список всех админов
[**apiAdminOtherAdminsObjectUserIdDelete**](AdminOtherAdminsApi.md#apiadminotheradminsobjectuseriddelete) | **DELETE** /api/admin/other-admins/{objectUserId} | Удалить профиль админа у пользователя. Только для админов с правом CreateAdminProfiles
[**apiAdminOtherAdminsObjectUserIdGet**](AdminOtherAdminsApi.md#apiadminotheradminsobjectuseridget) | **GET** /api/admin/other-admins/{objectUserId} | Конкретный админ
[**apiAdminOtherAdminsObjectUserIdPermissionsPatch**](AdminOtherAdminsApi.md#apiadminotheradminsobjectuseridpermissionspatch) | **PATCH** /api/admin/other-admins/{objectUserId}/permissions | Редактирование прав админа. Только для админов с правом EditPermissions


# **apiAdminOtherAdminsGet**
> AdminProfileDtoPageResult apiAdminOtherAdminsGet(page, pageSize)

Список всех админов

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminOtherAdminsApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiAdminOtherAdminsGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminOtherAdminsApi->apiAdminOtherAdminsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**AdminProfileDtoPageResult**](AdminProfileDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminOtherAdminsObjectUserIdDelete**
> int apiAdminOtherAdminsObjectUserIdDelete(objectUserId)

Удалить профиль админа у пользователя. Только для админов с правом CreateAdminProfiles

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminOtherAdminsApi();
final int objectUserId = 56; // int | Id пользователя

try {
    final response = api.apiAdminOtherAdminsObjectUserIdDelete(objectUserId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminOtherAdminsApi->apiAdminOtherAdminsObjectUserIdDelete: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **objectUserId** | **int**| Id пользователя | 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminOtherAdminsObjectUserIdGet**
> AdminProfileDto apiAdminOtherAdminsObjectUserIdGet(objectUserId)

Конкретный админ

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminOtherAdminsApi();
final int objectUserId = 56; // int | 

try {
    final response = api.apiAdminOtherAdminsObjectUserIdGet(objectUserId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminOtherAdminsApi->apiAdminOtherAdminsObjectUserIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **objectUserId** | **int**|  | 

### Return type

[**AdminProfileDto**](AdminProfileDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminOtherAdminsObjectUserIdPermissionsPatch**
> int apiAdminOtherAdminsObjectUserIdPermissionsPatch(objectUserId, requestBody)

Редактирование прав админа. Только для админов с правом EditPermissions

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminOtherAdminsApi();
final int objectUserId = 56; // int | 
final BuiltMap<String, bool> requestBody = ; // BuiltMap<String, bool> | 

try {
    final response = api.apiAdminOtherAdminsObjectUserIdPermissionsPatch(objectUserId, requestBody);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminOtherAdminsApi->apiAdminOtherAdminsObjectUserIdPermissionsPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **objectUserId** | **int**|  | 
 **requestBody** | [**BuiltMap&lt;String, bool&gt;**](bool.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

