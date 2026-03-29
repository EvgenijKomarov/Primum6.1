# my_api.api.TeacherLessonApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiTeacherLessonsFutureGet**](TeacherLessonApi.md#apiteacherlessonsfutureget) | **GET** /api/teacher/lessons/future | Только будущие занятия
[**apiTeacherLessonsGet**](TeacherLessonApi.md#apiteacherlessonsget) | **GET** /api/teacher/lessons | Все занятия, включая прошедшие и будущие
[**apiTeacherLessonsLessonIdGet**](TeacherLessonApi.md#apiteacherlessonslessonidget) | **GET** /api/teacher/lessons/{lessonId} | Конкретное занятие
[**apiTeacherLessonsLessonIdGradePost**](TeacherLessonApi.md#apiteacherlessonslessonidgradepost) | **POST** /api/teacher/lessons/{lessonId}/grade | Выставить оценку занятию


# **apiTeacherLessonsFutureGet**
> LessonDtoPageResult apiTeacherLessonsFutureGet(page, pageSize)

Только будущие занятия

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherLessonApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiTeacherLessonsFutureGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherLessonApi->apiTeacherLessonsFutureGet: $e\n');
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

# **apiTeacherLessonsGet**
> LessonDtoPageResult apiTeacherLessonsGet(page, pageSize)

Все занятия, включая прошедшие и будущие

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherLessonApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiTeacherLessonsGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherLessonApi->apiTeacherLessonsGet: $e\n');
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

# **apiTeacherLessonsLessonIdGet**
> LessonDto apiTeacherLessonsLessonIdGet(lessonId)

Конкретное занятие

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherLessonApi();
final int lessonId = 56; // int | 

try {
    final response = api.apiTeacherLessonsLessonIdGet(lessonId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherLessonApi->apiTeacherLessonsLessonIdGet: $e\n');
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

# **apiTeacherLessonsLessonIdGradePost**
> int apiTeacherLessonsLessonIdGradePost(lessonId, gradingInputDto)

Выставить оценку занятию

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherLessonApi();
final int lessonId = 56; // int | Id занятия
final GradingInputDto gradingInputDto = ; // GradingInputDto | Дто с оценками

try {
    final response = api.apiTeacherLessonsLessonIdGradePost(lessonId, gradingInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherLessonApi->apiTeacherLessonsLessonIdGradePost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **lessonId** | **int**| Id занятия | 
 **gradingInputDto** | [**GradingInputDto**](GradingInputDto.md)| Дто с оценками | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

