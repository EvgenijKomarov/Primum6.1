# my_api.api.StudentSheduleApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiStudentShedulesGet**](StudentSheduleApi.md#apistudentshedulesget) | **GET** /api/student/shedules | Все расписания ученика, на которые генерируются занятия
[**apiStudentShedulesSheduleIdDelete**](StudentSheduleApi.md#apistudentshedulessheduleiddelete) | **DELETE** /api/student/shedules/{sheduleId} | Удалить расписание
[**apiStudentShedulesSheduleIdGet**](StudentSheduleApi.md#apistudentshedulessheduleidget) | **GET** /api/student/shedules/{sheduleId} | Конкретное расписание ученика


# **apiStudentShedulesGet**
> AbonementSheduleDtoPageResult apiStudentShedulesGet(page, pageSize)

Все расписания ученика, на которые генерируются занятия

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentSheduleApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentShedulesGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentSheduleApi->apiStudentShedulesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
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

# **apiStudentShedulesSheduleIdDelete**
> int apiStudentShedulesSheduleIdDelete(abonementSheduleId, sheduleId)

Удалить расписание

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentSheduleApi();
final int abonementSheduleId = 56; // int | 
final String sheduleId = sheduleId_example; // String | 

try {
    final response = api.apiStudentShedulesSheduleIdDelete(abonementSheduleId, sheduleId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentSheduleApi->apiStudentShedulesSheduleIdDelete: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **abonementSheduleId** | **int**|  | 
 **sheduleId** | **String**|  | 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentShedulesSheduleIdGet**
> AbonementSheduleDto apiStudentShedulesSheduleIdGet(sheduleId)

Конкретное расписание ученика

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentSheduleApi();
final int sheduleId = 56; // int | 

try {
    final response = api.apiStudentShedulesSheduleIdGet(sheduleId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentSheduleApi->apiStudentShedulesSheduleIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **sheduleId** | **int**|  | 

### Return type

[**AbonementSheduleDto**](AbonementSheduleDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

