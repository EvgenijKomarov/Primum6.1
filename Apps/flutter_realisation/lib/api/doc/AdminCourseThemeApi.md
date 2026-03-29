# my_api.api.AdminCourseThemeApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiAdminThemesPost**](AdminCourseThemeApi.md#apiadminthemespost) | **POST** /api/admin/themes | Создать тему. Только для админов с правом EditCourseThemes
[**apiAdminThemesThemeIdPatch**](AdminCourseThemeApi.md#apiadminthemesthemeidpatch) | **PATCH** /api/admin/themes/{themeId} | Реадктирование темы курсов. Только для админов с правом EditCourseThemes


# **apiAdminThemesPost**
> int apiAdminThemesPost(courseThemeInputDto)

Создать тему. Только для админов с правом EditCourseThemes

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminCourseThemeApi();
final CourseThemeInputDto courseThemeInputDto = ; // CourseThemeInputDto | 

try {
    final response = api.apiAdminThemesPost(courseThemeInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminCourseThemeApi->apiAdminThemesPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **courseThemeInputDto** | [**CourseThemeInputDto**](CourseThemeInputDto.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminThemesThemeIdPatch**
> int apiAdminThemesThemeIdPatch(themeId, courseThemeInputDto)

Реадктирование темы курсов. Только для админов с правом EditCourseThemes

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminCourseThemeApi();
final int themeId = 56; // int | 
final CourseThemeInputDto courseThemeInputDto = ; // CourseThemeInputDto | 

try {
    final response = api.apiAdminThemesThemeIdPatch(themeId, courseThemeInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminCourseThemeApi->apiAdminThemesThemeIdPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **themeId** | **int**|  | 
 **courseThemeInputDto** | [**CourseThemeInputDto**](CourseThemeInputDto.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

