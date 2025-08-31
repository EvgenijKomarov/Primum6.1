using CoreConnection.DTOs;
using DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreConnection
{
    public class CoreConnector
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CoreConnector(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<List<LessonDto>> GetStudentLessonsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/student/{userId}/lessons");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<LessonDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<LessonDto>();
        }

        public async Task<List<AbonementDto>> GetStudentAbonementsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/student/{userId}/abonements");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AbonementDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<AbonementDto>();
        }

        public async Task<bool> SubscribeToCourseAsync(int userId, int courseId, SheduleDto teacherSheduleDto)
        {
            var json = JsonSerializer.Serialize(teacherSheduleDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{_baseUrl}/api/student/{userId}/subscribe-to-course/{courseId}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<LessonDto>> GetTeacherLessonsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/teacher/{userId}/lessons");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<LessonDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<LessonDto>();
        }

        public async Task<List<CourseDto>> GetTeacherCoursesAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/teacher/{userId}/courses");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CourseDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<CourseDto>();
        }

        public async Task<int?> RegisterCourseAsync(int userId, CourseDto newCourse)
        {
            var json = JsonSerializer.Serialize(newCourse);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{_baseUrl}/api/teacher/{userId}/courses/register", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return int.Parse(responseContent);
            }

            return null;
        }

        public async Task<List<SheduleDto>> GetTeacherShedulesAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/teacher/{userId}/shedules");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SheduleDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<SheduleDto>();
        }

        public async Task<int?> RegisterSheduleAsync(int userId, SheduleDto newShedule)
        {
            var json = JsonSerializer.Serialize(newShedule);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{_baseUrl}/api/teacher/{userId}/shedules/register", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return int.Parse(responseContent);
            }

            return null;
        }

        public async Task<UserDTO?> GetUserAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/user/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<int?> RegisterTeacherAsync(UserDTO dto, string about)
        {
            var requestData = new
            {
                dto = dto,
                about = about
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/user/reg-teacher", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return int.Parse(responseContent);
            }

            return null;
        }

        public async Task<int?> RegisterStudentAsync(UserDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/user/reg-user", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return int.Parse(responseContent);
            }

            return null;
        }
    }
}
