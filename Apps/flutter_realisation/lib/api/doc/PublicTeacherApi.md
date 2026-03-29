# my_api.api.PublicTeacherApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiPublicTeachersGet**](PublicTeacherApi.md#apipublicteachersget) | **GET** /api/public/teachers | Все доступные преподаватели
[**apiPublicTeachersTeacherIdCoursesGet**](PublicTeacherApi.md#apipublicteachersteacheridcoursesget) | **GET** /api/public/teachers/{teacherId}/courses | Все курсы преподавателя
[**apiPublicTeachersTeacherIdGet**](PublicTeacherApi.md#apipublicteachersteacheridget) | **GET** /api/public/teachers/{teacherId} | Информация о преподавателе
[**apiPublicTeachersTeacherIdShedulesGet**](PublicTeacherApi.md#apipublicteachersteacheridshedulesget) | **GET** /api/public/teachers/{teacherId}/shedules | Все расписания преподавателя


# **apiPublicTeachersGet**
> TeacherProfileDtoPageResult apiPublicTeachersGet(page, pageSize)

Все доступные преподаватели

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicTeacherApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicTeachersGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicTeacherApi->apiPublicTeachersGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**TeacherProfileDtoPageResult**](TeacherProfileDtoPageResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicTeachersTeacherIdCoursesGet**
> CourseDtoPageResult apiPublicTeachersTeacherIdCoursesGet(teacherId, page, pageSize)

Все курсы преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicTeacherApi();
final int teacherId = 56; // int | Id преподавателя
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicTeachersTeacherIdCoursesGet(teacherId, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicTeacherApi->apiPublicTeachersTeacherIdCoursesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **teacherId** | **int**| Id преподавателя | 
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**CourseDtoPageResult**](CourseDtoPageResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicTeachersTeacherIdGet**
> TeacherProfileDto apiPublicTeachersTeacherIdGet(teacherId)

Информация о преподавателе

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicTeacherApi();
final int teacherId = 56; // int | Id преподавателя

try {
    final response = api.apiPublicTeachersTeacherIdGet(teacherId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicTeacherApi->apiPublicTeachersTeacherIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **teacherId** | **int**| Id преподавателя | 

### Return type

[**TeacherProfileDto**](TeacherProfileDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicTeachersTeacherIdShedulesGet**
> TeacherSheduleDtoPageResult apiPublicTeachersTeacherIdShedulesGet(teacherId, page, pageSize)

Все расписания преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicTeacherApi();
final int teacherId = 56; // int | Id преподавателя
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicTeachersTeacherIdShedulesGet(teacherId, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicTeacherApi->apiPublicTeachersTeacherIdShedulesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **teacherId** | **int**| Id преподавателя | 
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**TeacherSheduleDtoPageResult**](TeacherSheduleDtoPageResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

