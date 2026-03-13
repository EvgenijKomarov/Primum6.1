using CoreConnection.DTOs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Extentions
{
    internal static class ModelExtensions
    {
        public static IQueryable<AbonementDto> ToDto(this IQueryable<Abonement> queryable) => queryable.Select(x => 
            new AbonementDto
            {
                StudentId = x.Student.User.Id,
                StudentDisplayName = x.Student.User.DisplayName,
                TeacherId = x.Course.Teacher.User.Id,
                TeacherDisplayName = x.Course.Teacher.User.DisplayName,
                AbonementId = x.AbonementId,
                CourseName = x.Course.Name,
                CourseId = x.Course.CourseId,
                CourseThemeName = x.Course.CourseTheme.ThemeName,
                CourseThemeId = x.Course.CourseTheme.CourseThemeId,
                PricePerLesson = x.PricePerLesson,
                AbonementStatus = x.AbonementStatus
            });

        public static IQueryable<CourseDto> ToDto(this IQueryable<Course> queryable) => queryable.Select(x => 
            new CourseDto
            {
                CourseId = x.CourseId,
                Name = x.Name,
                TeacherName = x.Teacher.User.DisplayName,
                CourseThemeName = x.CourseTheme.ThemeName,
                About = x.About,
                CourseThemeId = x.CourseThemeId,
                TeacherId = x.TeacherId,
                Price = x.Price,
                MaxLessons = x.MaxLessons,
                FreeLessons = x.FreeLessons,
                TeacherAbout = x.Teacher.About,
                IsActive = x.IsActive,
                ApproveStatus = x.ApproveStatus
            });

        public static IQueryable<AdminProfileDto> ToDto(this IQueryable<AdminProfile> queryable, AdminProfileHelper helper) => queryable.Select(x => 
            new AdminProfileDto
            {
                DisplayName = x.User.DisplayName,
                UserId = x.User.Id,
                Status = x.Status ?? string.Empty,
                Permissions = helper.GetAllPermissions(x)
            });

        public static IQueryable<CourseThemeDto> ToDto(this IQueryable<CourseTheme> queryable) => queryable.Select(x => 
            new CourseThemeDto
            {
                CourseThemeId = x.CourseThemeId,
                ThemeName = x.ThemeName,
                IsActive = x.IsActive
            });

        public static IQueryable<IncidentLogDto> ToDto(this IQueryable<IncidentLog> queryable) => queryable.Select(x => 
            new IncidentLogDto
            {
                LogId = x.LogId,
                AdminUserId = x.AdminProfile.User.Id,
                AdminDisplayName = x.AdminProfile.User.DisplayName,
                DateTime = x.DecisionDate,
                Description = x.Description
            });

        public static IQueryable<LessonDto> ToDto(this IQueryable<Lesson> queryable, bool isStudentLink) => queryable.Select(x => 
            new LessonDto
            {
                DateTime = x.DateTime,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.CourseId,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                StudentDisplayName = x.Abonement.Student.User.DisplayName,
                StudentId = x.Abonement.Student.User.Id,
                LessonLink = isStudentLink ? (x.StudentLink ?? string.Empty) : (x.TeacherLink ?? string.Empty),
                AbonementId = x.Abonement.AbonementId,
                Price = x.Price,
                LessonId = x.LessonId,
                LessonStatus = x.Status,
                Grade = x.Grading == null ? null : x.Grading.GetFinalGrade()
            });

        public static IQueryable<PromocodeDto> ToDto(this IQueryable<Promocode> queryable, bool isCodeSecured) => queryable.Select(x => 
            new PromocodeDto
            {
                PromocodeId = x.PromocodeId,
                StudentId = x.Student!.StudentId,
                Code = isCodeSecured == true ? null : x.Code,
                CoinsPrice = x.CoinsPrice,
                Title = x.Title,
                Description = x.Description,
                IsAvailable = AvailabilityExpressions.IsPromocodeAvailable.Compile()(x)
            });

        public static IQueryable<TeacherProfileDto> ToDto(this IQueryable<TeacherProfile> queryable) => queryable.Select(x => 
            new TeacherProfileDto
            {
                DisplayName = x.User.DisplayName,
                About = x.About,
                UserId = x.User.Id,
                IsAvailable = AvailabilityExpressions.IsTeacherAvailable.Compile()(x.User)
            });

        public static IQueryable<StudentProfileDto> ToDto(this IQueryable<StudentProfile> queryable) => queryable.Select(x => 
            new StudentProfileDto
            {
                DisplayName = x.User.DisplayName,
                UserId = x.User.Id,
                Coins = x.Coins
            });

        public static IQueryable<StudentSheduleDto> ToDto(this IQueryable<AbonementShedule> queryable) => queryable.Select(x => 
            new StudentSheduleDto
            {
                DayOfWeek = x.TeacherShedule.DayOfWeek,
                Time = x.TeacherShedule.Time,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.CourseId,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                AbonementSheduleId = x.AbonementSheduleId
            });

        public static IQueryable<TeacherSheduleDto> ToDto(this IQueryable<TeacherShedule> queryable) => queryable.Select(x => 
            new TeacherSheduleDto
            {
                TeacherSheduleId = x.TeacherSheduleId,
                DayOfWeek = x.DayOfWeek,
                Time = x.Time,
                IsAvailable = AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(x),
                StudentName = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Student.User.DisplayName,
                StudentId = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Student.User.Id,
                CourseName = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Course.Name,
                CourseId = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Course.CourseId,
            });

        public static IQueryable<UserDto> ToDto(this IQueryable<User> queryable) => queryable.Select(x => 
            new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Patronymic = x.Patronymic,
                DisplayName = x.DisplayName,
                Cash = x.Cash,
                IsApprovedStudent = x.StudentProfile != null ?
                            x.StudentProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsApprovedTeacher = x.TeacherProfile != null ?
                            x.TeacherProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsAdmin = x.AdminProfile != null,
                IsBanned = x.IsBanned,
                MailConfirmed = x.IsMailChecked,
                IsAvailable = AvailabilityExpressions.IsUserAvailable.Compile()(x)
            });

        public static IQueryable<UserDtoLite> ToDtoLite(this IQueryable<User> queryable) => queryable.Select(x => 
            new UserDtoLite
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Patronymic = x.Patronymic,
                DisplayName = x.DisplayName,
                IsApprovedStudent = x.StudentProfile != null ?
                            x.StudentProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsApprovedTeacher = x.TeacherProfile != null ?
                            x.TeacherProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsAdmin = x.AdminProfile != null,
                IsAvailable = AvailabilityExpressions.IsUserAvailable.Compile()(x)
            });
    }
}
