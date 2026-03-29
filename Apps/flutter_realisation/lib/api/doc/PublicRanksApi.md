# my_api.api.PublicRanksApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiPublicRanksCourseGet**](PublicRanksApi.md#apipublicrankscourseget) | **GET** /api/public/ranks/course | Все ранги курсов
[**apiPublicRanksStudentGet**](PublicRanksApi.md#apipublicranksstudentget) | **GET** /api/public/ranks/student | Все ранги учеников
[**apiPublicRanksTeacherGet**](PublicRanksApi.md#apipublicranksteacherget) | **GET** /api/public/ranks/teacher | Все ранги преподавателей


# **apiPublicRanksCourseGet**
> CourseRankDtoPageResult apiPublicRanksCourseGet(page, pageSize)

Все ранги курсов

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicRanksApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicRanksCourseGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicRanksApi->apiPublicRanksCourseGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**CourseRankDtoPageResult**](CourseRankDtoPageResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicRanksStudentGet**
> StudentRankDtoPageResult apiPublicRanksStudentGet(page, pageSize)

Все ранги учеников

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicRanksApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicRanksStudentGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicRanksApi->apiPublicRanksStudentGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**StudentRankDtoPageResult**](StudentRankDtoPageResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiPublicRanksTeacherGet**
> TeacherRankDtoPageResult apiPublicRanksTeacherGet(page, pageSize)

Все ранги преподавателей

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getPublicRanksApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiPublicRanksTeacherGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling PublicRanksApi->apiPublicRanksTeacherGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**TeacherRankDtoPageResult**](TeacherRankDtoPageResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

