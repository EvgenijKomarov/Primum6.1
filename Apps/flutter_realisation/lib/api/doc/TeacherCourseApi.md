# my_api.api.TeacherCourseApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiTeacherCoursesCourseIdActivityPatch**](TeacherCourseApi.md#apiteachercoursescourseidactivitypatch) | **PATCH** /api/teacher/courses/{courseId}/activity | Активировать/деактивировать курс, чтобы он отображался в общем списке и на него могли подписываться ученики, либо скрыть его от учеников и не дать им на него подписаться
[**apiTeacherCoursesCourseIdGet**](TeacherCourseApi.md#apiteachercoursescourseidget) | **GET** /api/teacher/courses/{courseId} | Конкретный курс преподавателя
[**apiTeacherCoursesCourseIdPut**](TeacherCourseApi.md#apiteachercoursescourseidput) | **PUT** /api/teacher/courses/{courseId} | Реадктирование курса. При изменении названия и описания, курс отправляется заново на процедуру утверждения и пропадает из видимости у остальных пользователей
[**apiTeacherCoursesGet**](TeacherCourseApi.md#apiteachercoursesget) | **GET** /api/teacher/courses | Все курсы преподавателя
[**apiTeacherCoursesPost**](TeacherCourseApi.md#apiteachercoursespost) | **POST** /api/teacher/courses | Создать курс


# **apiTeacherCoursesCourseIdActivityPatch**
> int apiTeacherCoursesCourseIdActivityPatch(courseId, body)

Активировать/деактивировать курс, чтобы он отображался в общем списке и на него могли подписываться ученики, либо скрыть его от учеников и не дать им на него подписаться

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherCourseApi();
final int courseId = 56; // int | 
final bool body = true; // bool | Статус активности (active/non-active)

try {
    final response = api.apiTeacherCoursesCourseIdActivityPatch(courseId, body);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherCourseApi->apiTeacherCoursesCourseIdActivityPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **courseId** | **int**|  | 
 **body** | **bool**| Статус активности (active/non-active) | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiTeacherCoursesCourseIdGet**
> CourseDto apiTeacherCoursesCourseIdGet(courseId)

Конкретный курс преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherCourseApi();
final int courseId = 56; // int | 

try {
    final response = api.apiTeacherCoursesCourseIdGet(courseId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherCourseApi->apiTeacherCoursesCourseIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **courseId** | **int**|  | 

### Return type

[**CourseDto**](CourseDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiTeacherCoursesCourseIdPut**
> int apiTeacherCoursesCourseIdPut(courseId, courseInputDto)

Реадктирование курса. При изменении названия и описания, курс отправляется заново на процедуру утверждения и пропадает из видимости у остальных пользователей

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherCourseApi();
final int courseId = 56; // int | 
final CourseInputDto courseInputDto = ; // CourseInputDto | 

try {
    final response = api.apiTeacherCoursesCourseIdPut(courseId, courseInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherCourseApi->apiTeacherCoursesCourseIdPut: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **courseId** | **int**|  | 
 **courseInputDto** | [**CourseInputDto**](CourseInputDto.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiTeacherCoursesGet**
> CourseDtoPageResult apiTeacherCoursesGet(page, pageSize)

Все курсы преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherCourseApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiTeacherCoursesGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherCourseApi->apiTeacherCoursesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**CourseDtoPageResult**](CourseDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiTeacherCoursesPost**
> int apiTeacherCoursesPost(courseInputDto)

Создать курс

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherCourseApi();
final CourseInputDto courseInputDto = ; // CourseInputDto | 

try {
    final response = api.apiTeacherCoursesPost(courseInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherCourseApi->apiTeacherCoursesPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **courseInputDto** | [**CourseInputDto**](CourseInputDto.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

