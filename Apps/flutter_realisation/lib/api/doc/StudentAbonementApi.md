# my_api.api.StudentAbonementApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiStudentAbonementsAbonementIdDelete**](StudentAbonementApi.md#apistudentabonementsabonementiddelete) | **DELETE** /api/student/abonements/{abonementId} | Удалить абонемент
[**apiStudentAbonementsAbonementIdGet**](StudentAbonementApi.md#apistudentabonementsabonementidget) | **GET** /api/student/abonements/{abonementId} | Конкретный абонемент ученика
[**apiStudentAbonementsAbonementIdLessonsGet**](StudentAbonementApi.md#apistudentabonementsabonementidlessonsget) | **GET** /api/student/abonements/{abonementId}/lessons | Все занятия по абонементу, включая прошедшие и будущие
[**apiStudentAbonementsAbonementIdShedulesGet**](StudentAbonementApi.md#apistudentabonementsabonementidshedulesget) | **GET** /api/student/abonements/{abonementId}/shedules | Расписания ученика, привязанного к конкретному абонементу
[**apiStudentAbonementsAbonementIdStatusPatch**](StudentAbonementApi.md#apistudentabonementsabonementidstatuspatch) | **PATCH** /api/student/abonements/{abonementId}/status | Активировать абонемент или заморозить, чтобы занятия по нему генерировались, но не происходили
[**apiStudentAbonementsGet**](StudentAbonementApi.md#apistudentabonementsget) | **GET** /api/student/abonements | Все абонементы ученика


# **apiStudentAbonementsAbonementIdDelete**
> int apiStudentAbonementsAbonementIdDelete(abonementId)

Удалить абонемент

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentAbonementApi();
final int abonementId = 56; // int | 

try {
    final response = api.apiStudentAbonementsAbonementIdDelete(abonementId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentAbonementApi->apiStudentAbonementsAbonementIdDelete: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **abonementId** | **int**|  | 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentAbonementsAbonementIdGet**
> AbonementDto apiStudentAbonementsAbonementIdGet(abonementId)

Конкретный абонемент ученика

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentAbonementApi();
final int abonementId = 56; // int | 

try {
    final response = api.apiStudentAbonementsAbonementIdGet(abonementId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentAbonementApi->apiStudentAbonementsAbonementIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **abonementId** | **int**|  | 

### Return type

[**AbonementDto**](AbonementDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentAbonementsAbonementIdLessonsGet**
> LessonDtoPageResult apiStudentAbonementsAbonementIdLessonsGet(abonementId, page, pageSize)

Все занятия по абонементу, включая прошедшие и будущие

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentAbonementApi();
final int abonementId = 56; // int | 
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentAbonementsAbonementIdLessonsGet(abonementId, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentAbonementApi->apiStudentAbonementsAbonementIdLessonsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **abonementId** | **int**|  | 
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**LessonDtoPageResult**](LessonDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentAbonementsAbonementIdShedulesGet**
> AbonementSheduleDtoPageResult apiStudentAbonementsAbonementIdShedulesGet(abonementId, page, pageSize)

Расписания ученика, привязанного к конкретному абонементу

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentAbonementApi();
final int abonementId = 56; // int | 
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentAbonementsAbonementIdShedulesGet(abonementId, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentAbonementApi->apiStudentAbonementsAbonementIdShedulesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **abonementId** | **int**|  | 
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**AbonementSheduleDtoPageResult**](AbonementSheduleDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentAbonementsAbonementIdStatusPatch**
> int apiStudentAbonementsAbonementIdStatusPatch(abonementId, body)

Активировать абонемент или заморозить, чтобы занятия по нему генерировались, но не происходили

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentAbonementApi();
final int abonementId = 56; // int | 
final int body = 56; // int | 

try {
    final response = api.apiStudentAbonementsAbonementIdStatusPatch(abonementId, body);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentAbonementApi->apiStudentAbonementsAbonementIdStatusPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **abonementId** | **int**|  | 
 **body** | **int**|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentAbonementsGet**
> AbonementDtoPageResult apiStudentAbonementsGet(page, pageSize)

Все абонементы ученика

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentAbonementApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentAbonementsGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentAbonementApi->apiStudentAbonementsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**AbonementDtoPageResult**](AbonementDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

