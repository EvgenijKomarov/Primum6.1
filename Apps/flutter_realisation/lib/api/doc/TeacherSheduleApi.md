# my_api.api.TeacherSheduleApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiTeacherShedulesGet**](TeacherSheduleApi.md#apiteachershedulesget) | **GET** /api/teacher/shedules | Все расписания преподавателя
[**apiTeacherShedulesPost**](TeacherSheduleApi.md#apiteachershedulespost) | **POST** /api/teacher/shedules | Создать расписание преподавателя
[**apiTeacherShedulesSheduleIdDelete**](TeacherSheduleApi.md#apiteachershedulessheduleiddelete) | **DELETE** /api/teacher/shedules/{sheduleId} | Удалить расписание. Оно не удалится, если к расписанию преподавателя привязано расписание ученика
[**apiTeacherShedulesSheduleIdGet**](TeacherSheduleApi.md#apiteachershedulessheduleidget) | **GET** /api/teacher/shedules/{sheduleId} | Конкретное расписание


# **apiTeacherShedulesGet**
> TeacherSheduleDtoPageResult apiTeacherShedulesGet(page, pageSize)

Все расписания преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherSheduleApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiTeacherShedulesGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherSheduleApi->apiTeacherShedulesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**TeacherSheduleDtoPageResult**](TeacherSheduleDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiTeacherShedulesPost**
> int apiTeacherShedulesPost(teacherSheduleInputDto)

Создать расписание преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherSheduleApi();
final TeacherSheduleInputDto teacherSheduleInputDto = ; // TeacherSheduleInputDto | 

try {
    final response = api.apiTeacherShedulesPost(teacherSheduleInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherSheduleApi->apiTeacherShedulesPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **teacherSheduleInputDto** | [**TeacherSheduleInputDto**](TeacherSheduleInputDto.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiTeacherShedulesSheduleIdDelete**
> int apiTeacherShedulesSheduleIdDelete(sheduleId)

Удалить расписание. Оно не удалится, если к расписанию преподавателя привязано расписание ученика

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherSheduleApi();
final int sheduleId = 56; // int | 

try {
    final response = api.apiTeacherShedulesSheduleIdDelete(sheduleId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherSheduleApi->apiTeacherShedulesSheduleIdDelete: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **sheduleId** | **int**|  | 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiTeacherShedulesSheduleIdGet**
> TeacherSheduleDto apiTeacherShedulesSheduleIdGet(sheduleId)

Конкретное расписание

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherSheduleApi();
final int sheduleId = 56; // int | 

try {
    final response = api.apiTeacherShedulesSheduleIdGet(sheduleId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherSheduleApi->apiTeacherShedulesSheduleIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **sheduleId** | **int**|  | 

### Return type

[**TeacherSheduleDto**](TeacherSheduleDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

