# my_api.api.PublicCourseApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiPublicCoursesByThemeThemeIdGet**](PublicCourseApi.md#apipubliccoursesbythemethemeidget) | **GET** /api/public/courses/by-theme/{themeId} | Курсы по теме
[**apiPublicCoursesCourseIdGet**](PublicCourseApi.md#apipubliccoursescourseidget) | **GET** /api/public/courses/{courseId} | Конкретный курс
[**apiPublicCoursesGet**](PublicCourseApi.md#apipubliccoursesget) | **GET** /api/public/courses | Все курсы


# **apiPublicCoursesByThemeThemeIdGet**
> CourseDtoPageResult apiPublicCoursesByThemeThemeIdGet(themeId, page, pageSize)

Курсы по теме

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicCourseApi();
final int themeId = 56; // int | Id темы
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicCoursesByThemeThemeIdGet(themeId, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicCourseApi->apiPublicCoursesByThemeThemeIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **themeId** | **int**| Id темы | 
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

# **apiPublicCoursesCourseIdGet**
> CourseDto apiPublicCoursesCourseIdGet(courseId)

Конкретный курс

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicCourseApi();
final int courseId = 56; // int | 

try {
    final response = api.apiPublicCoursesCourseIdGet(courseId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicCourseApi->apiPublicCoursesCourseIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **courseId** | **int**|  | 

### Return type

[**CourseDto**](CourseDto.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicCoursesGet**
> CourseDtoPageResult apiPublicCoursesGet(page, pageSize)

Все курсы

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicCourseApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicCoursesGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicCourseApi->apiPublicCoursesGet: $e\n');
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

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

