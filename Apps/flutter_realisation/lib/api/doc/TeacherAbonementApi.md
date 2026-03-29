# my_api.api.TeacherAbonementApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiTeacherAbonementsAbonementIdGet**](TeacherAbonementApi.md#apiteacherabonementsabonementidget) | **GET** /api/teacher/abonements/{abonementId} | Конкретный абонемент ученика, подписанного на курс преподавателя
[**apiTeacherAbonementsAbonementIdLessonsGet**](TeacherAbonementApi.md#apiteacherabonementsabonementidlessonsget) | **GET** /api/teacher/abonements/{abonementId}/lessons | Все занятия абонемента ученика, подписанного на курс преподавателя, и прошедшие и будущие
[**apiTeacherAbonementsAbonementIdShedulesGet**](TeacherAbonementApi.md#apiteacherabonementsabonementidshedulesget) | **GET** /api/teacher/abonements/{abonementId}/shedules | Расписания абонемента ученика, подписанного на курс преподавателя
[**apiTeacherAbonementsGet**](TeacherAbonementApi.md#apiteacherabonementsget) | **GET** /api/teacher/abonements | Все абонементы учеников, подписанные на курсы преподавателя


# **apiTeacherAbonementsAbonementIdGet**
> AbonementDto apiTeacherAbonementsAbonementIdGet(abonementId)

Конкретный абонемент ученика, подписанного на курс преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherAbonementApi();
final int abonementId = 56; // int | 

try {
    final response = api.apiTeacherAbonementsAbonementIdGet(abonementId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherAbonementApi->apiTeacherAbonementsAbonementIdGet: $e\n');
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

# **apiTeacherAbonementsAbonementIdLessonsGet**
> LessonDtoPageResult apiTeacherAbonementsAbonementIdLessonsGet(abonementId, page, pageSize)

Все занятия абонемента ученика, подписанного на курс преподавателя, и прошедшие и будущие

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherAbonementApi();
final int abonementId = 56; // int | 
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiTeacherAbonementsAbonementIdLessonsGet(abonementId, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherAbonementApi->apiTeacherAbonementsAbonementIdLessonsGet: $e\n');
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

# **apiTeacherAbonementsAbonementIdShedulesGet**
> AbonementSheduleDtoPageResult apiTeacherAbonementsAbonementIdShedulesGet(abonementId, page, pageSize)

Расписания абонемента ученика, подписанного на курс преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherAbonementApi();
final int abonementId = 56; // int | 
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiTeacherAbonementsAbonementIdShedulesGet(abonementId, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherAbonementApi->apiTeacherAbonementsAbonementIdShedulesGet: $e\n');
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

# **apiTeacherAbonementsGet**
> AbonementDtoPageResult apiTeacherAbonementsGet(page, pageSize)

Все абонементы учеников, подписанные на курсы преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherAbonementApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiTeacherAbonementsGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherAbonementApi->apiTeacherAbonementsGet: $e\n');
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

