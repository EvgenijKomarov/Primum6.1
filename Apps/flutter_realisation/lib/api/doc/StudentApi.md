# my_api.api.StudentApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiStudentGet**](StudentApi.md#apistudentget) | **GET** /api/student | Профиль ученика, включая имя, количество монет и id пользователя
[**apiStudentSubscribeCourseIdTeacherSheduleIdPost**](StudentApi.md#apistudentsubscribecourseidteachersheduleidpost) | **POST** /api/student/subscribe/{courseId}/{teacherSheduleId} | Подписаться на курс


# **apiStudentGet**
> StudentProfileDto apiStudentGet()

Профиль ученика, включая имя, количество монет и id пользователя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentApi();

try {
    final response = api.apiStudentGet();
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentApi->apiStudentGet: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**StudentProfileDto**](StudentProfileDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiStudentSubscribeCourseIdTeacherSheduleIdPost**
> int apiStudentSubscribeCourseIdTeacherSheduleIdPost(courseId, teacherSheduleId)

Подписаться на курс

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentApi();
final int courseId = 56; // int | Id курса
final int teacherSheduleId = 56; // int | Id расписания преподавателя

try {
    final response = api.apiStudentSubscribeCourseIdTeacherSheduleIdPost(courseId, teacherSheduleId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentApi->apiStudentSubscribeCourseIdTeacherSheduleIdPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **courseId** | **int**| Id курса | 
 **teacherSheduleId** | **int**| Id расписания преподавателя | 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

