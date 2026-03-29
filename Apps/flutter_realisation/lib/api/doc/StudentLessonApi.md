# my_api.api.StudentLessonApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiStudentLessonsFutureGet**](StudentLessonApi.md#apistudentlessonsfutureget) | **GET** /api/student/lessons/future | Только будущие занятия
[**apiStudentLessonsGet**](StudentLessonApi.md#apistudentlessonsget) | **GET** /api/student/lessons | Все занятия ученика, включая прошедшие и будущие
[**apiStudentLessonsLessonIdGet**](StudentLessonApi.md#apistudentlessonslessonidget) | **GET** /api/student/lessons/{lessonId} | Конкретное занятие


# **apiStudentLessonsFutureGet**
> LessonDtoPageResult apiStudentLessonsFutureGet(page, pageSize)

Только будущие занятия

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentLessonApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentLessonsFutureGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentLessonApi->apiStudentLessonsFutureGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
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

# **apiStudentLessonsGet**
> LessonDtoPageResult apiStudentLessonsGet(page, pageSize)

Все занятия ученика, включая прошедшие и будущие

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentLessonApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiStudentLessonsGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentLessonApi->apiStudentLessonsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
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

# **apiStudentLessonsLessonIdGet**
> LessonDto apiStudentLessonsLessonIdGet(lessonId)

Конкретное занятие

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getStudentLessonApi();
final int lessonId = 56; // int | 

try {
    final response = api.apiStudentLessonsLessonIdGet(lessonId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling StudentLessonApi->apiStudentLessonsLessonIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **lessonId** | **int**|  | 

### Return type

[**LessonDto**](LessonDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

