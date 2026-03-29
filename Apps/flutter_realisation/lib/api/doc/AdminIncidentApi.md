# my_api.api.AdminIncidentApi

## Load the API package
```dart
import 'package:my_api/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiAdminIncidentsGet**](AdminIncidentApi.md#apiadminincidentsget) | **GET** /api/admin/incidents | Список всех инцидентов. Доступно всем
[**apiAdminIncidentsLogsGet**](AdminIncidentApi.md#apiadminincidentslogsget) | **GET** /api/admin/incidents/logs | Посмотреть логи действий всех админов. Только для админов с правом InspectIncidentLogs
[**apiAdminIncidentsLogsLogIdGet**](AdminIncidentApi.md#apiadminincidentslogslogidget) | **GET** /api/admin/incidents/logs/{logId} | Конкретный лог действия админа. Только для админов с правом InspectIncidentLogs
[**apiAdminIncidentsLogsLogIdPatch**](AdminIncidentApi.md#apiadminincidentslogslogidpatch) | **PATCH** /api/admin/incidents/logs/{logId} | Отметить лог просмотренным (предполагается, что это делается кнопкой под карточкой лога)
[**apiAdminIncidentsPatch**](AdminIncidentApi.md#apiadminincidentspatch) | **PATCH** /api/admin/incidents | Решение инцидента. Доступно всем


# **apiAdminIncidentsGet**
> IncidentDtoPageResult apiAdminIncidentsGet(page, pageSize)

Список всех инцидентов. Доступно всем

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminIncidentApi();
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiAdminIncidentsGet(page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminIncidentApi->apiAdminIncidentsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**IncidentDtoPageResult**](IncidentDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminIncidentsLogsGet**
> IncidentLogDtoPageResult apiAdminIncidentsLogsGet(onlyUnrevisioned, page, pageSize)

Посмотреть логи действий всех админов. Только для админов с правом InspectIncidentLogs

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminIncidentApi();
final bool onlyUnrevisioned = true; // bool | 
final int page = 56; // int | 
final int pageSize = 56; // int | 

try {
    final response = api.apiAdminIncidentsLogsGet(onlyUnrevisioned, page, pageSize);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminIncidentApi->apiAdminIncidentsLogsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **onlyUnrevisioned** | **bool**|  | [optional] [default to true]
 **page** | **int**|  | [optional] [default to 0]
 **pageSize** | **int**|  | [optional] [default to 10]

### Return type

[**IncidentLogDtoPageResult**](IncidentLogDtoPageResult.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminIncidentsLogsLogIdGet**
> IncidentLogDto apiAdminIncidentsLogsLogIdGet(logId)

Конкретный лог действия админа. Только для админов с правом InspectIncidentLogs

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminIncidentApi();
final int logId = 56; // int | 

try {
    final response = api.apiAdminIncidentsLogsLogIdGet(logId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminIncidentApi->apiAdminIncidentsLogsLogIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **logId** | **int**|  | 

### Return type

[**IncidentLogDto**](IncidentLogDto.md)

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminIncidentsLogsLogIdPatch**
> int apiAdminIncidentsLogsLogIdPatch(logId)

Отметить лог просмотренным (предполагается, что это делается кнопкой под карточкой лога)

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminIncidentApi();
final int logId = 56; // int | 

try {
    final response = api.apiAdminIncidentsLogsLogIdPatch(logId);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminIncidentApi->apiAdminIncidentsLogsLogIdPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **logId** | **int**|  | 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiAdminIncidentsPatch**
> int apiAdminIncidentsPatch(incidentDecisionInputDto)

Решение инцидента. Доступно всем

### Example
```dart
import 'package:my_api/api.dart';

final api = MyApi().getAdminIncidentApi();
final IncidentDecisionInputDto incidentDecisionInputDto = ; // IncidentDecisionInputDto | 

try {
    final response = api.apiAdminIncidentsPatch(incidentDecisionInputDto);
    print(response);
} on DioException catch (e) {
    print('Exception when calling AdminIncidentApi->apiAdminIncidentsPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **incidentDecisionInputDto** | [**IncidentDecisionInputDto**](IncidentDecisionInputDto.md)|  | [optional] 

### Return type

**int**

### Authorization

[bearer](../README.md#bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

