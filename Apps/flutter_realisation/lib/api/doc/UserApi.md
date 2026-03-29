# my_api.api.UserApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiUserConfirmChatPost**](UserApi.md#apiuserconfirmchatpost) | **POST** /api/user/confirm-chat | Подтвердить чат, отправив токен из него
[**apiUserConfirmEmailPost**](UserApi.md#apiuserconfirmemailpost) | **POST** /api/user/confirm-email | Подтвердить почту, отправив пришедший в письме токен
[**apiUserCreateStudentProfilePost**](UserApi.md#apiusercreatestudentprofilepost) | **POST** /api/user/create-student-profile | Создать профиль ученика
[**apiUserCreateTeacherProfilePost**](UserApi.md#apiusercreateteacherprofilepost) | **POST** /api/user/create-teacher-profile | Создать профиль преподавателя
[**apiUserGet**](UserApi.md#apiuserget) | **GET** /api/user | Полный профиль пользователя, включая информацию о том, является ли он учеником или преподавателем, подтверждена ли почта и т.д.
[**apiUserSendEmailVerificationPost**](UserApi.md#apiusersendemailverificationpost) | **POST** /api/user/send-email-verification | Отправить письмо с подтверждением почты (не сработает если почта уже подтверждена)


# **apiUserConfirmChatPost**
> int apiUserConfirmChatPost(body)

Подтвердить чат, отправив токен из него

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getUserApi();
final String body = body_example; // String | Токен из чата

try {
    final response = api.apiUserConfirmChatPost(body);
    print(response);
} on DioException catch (e) {
    print('Exception when calling UserApi->apiUserConfirmChatPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**| Токен из чата | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUserConfirmEmailPost**
> int apiUserConfirmEmailPost(body)

Подтвердить почту, отправив пришедший в письме токен

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getUserApi();
final String body = body_example; // String | Токен из письма

try {
    final response = api.apiUserConfirmEmailPost(body);
    print(response);
} on DioException catch (e) {
    print('Exception when calling UserApi->apiUserConfirmEmailPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**| Токен из письма | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUserCreateStudentProfilePost**
> int apiUserCreateStudentProfilePost()

Создать профиль ученика

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getUserApi();

try {
    final response = api.apiUserCreateStudentProfilePost();
    print(response);
} on DioException catch (e) {
    print('Exception when calling UserApi->apiUserCreateStudentProfilePost: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUserCreateTeacherProfilePost**
> int apiUserCreateTeacherProfilePost(body)

Создать профиль преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getUserApi();
final String body = body_example; // String | О преподавателе (обязательно)

try {
    final response = api.apiUserCreateTeacherProfilePost(body);
    print(response);
} on DioException catch (e) {
    print('Exception when calling UserApi->apiUserCreateTeacherProfilePost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**| О преподавателе (обязательно) | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUserGet**
> UserDto apiUserGet()

Полный профиль пользователя, включая информацию о том, является ли он учеником или преподавателем, подтверждена ли почта и т.д.

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getUserApi();

try {
    final response = api.apiUserGet();
    print(response);
} on DioException catch (e) {
    print('Exception when calling UserApi->apiUserGet: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**UserDto**](UserDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiUserSendEmailVerificationPost**
> int apiUserSendEmailVerificationPost(correctiveMail)

Отправить письмо с подтверждением почты (не сработает если почта уже подтверждена)

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getUserApi();
final String correctiveMail = correctiveMail_example; // String | Если нужно внести изменения в адрес почты, можно передать correctiveMail

try {
    final response = api.apiUserSendEmailVerificationPost(correctiveMail);
    print(response);
} on DioException catch (e) {
    print('Exception when calling UserApi->apiUserSendEmailVerificationPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **correctiveMail** | **String**| Если нужно внести изменения в адрес почты, можно передать correctiveMail | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

