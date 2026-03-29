# my_api.api.PublicApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiPublicLoginPost**](PublicApi.md#apipublicloginpost) | **POST** /api/public/login | Контроллер для авторизации. Возвращает JWT токен
[**apiPublicRegisterPost**](PublicApi.md#apipublicregisterpost) | **POST** /api/public/register | Регистрация. Возвращает при успехе JWT токен


# **apiPublicLoginPost**
> String apiPublicLoginPost(loggingInputDto)

Контроллер для авторизации. Возвращает JWT токен

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicApi();
final LoggingInputDto loggingInputDto = ; // LoggingInputDto | 

try {
    final response = api.apiPublicLoginPost(loggingInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicApi->apiPublicLoginPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **loggingInputDto** | [**LoggingInputDto**](LoggingInputDto.md)|  | [optional] 

### Return type

**String**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicRegisterPost**
> String apiPublicRegisterPost(registrationInputDto)

Регистрация. Возвращает при успехе JWT токен

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicApi();
final RegistrationInputDto registrationInputDto = ; // RegistrationInputDto | 

try {
    final response = api.apiPublicRegisterPost(registrationInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicApi->apiPublicRegisterPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **registrationInputDto** | [**RegistrationInputDto**](RegistrationInputDto.md)|  | [optional] 

### Return type

**String**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

