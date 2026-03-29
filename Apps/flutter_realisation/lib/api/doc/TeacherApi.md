# my_api.api.TeacherApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiTeacherGet**](TeacherApi.md#apiteacherget) | **GET** /api/teacher | Профиль преподавателя


# **apiTeacherGet**
> TeacherProfileDto apiTeacherGet()

Профиль преподавателя

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getTeacherApi();

try {
    final response = api.apiTeacherGet();
    print(response);
} on DioException catch (e) {
    print('Exception when calling TeacherApi->apiTeacherGet: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**TeacherProfileDto**](TeacherProfileDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

