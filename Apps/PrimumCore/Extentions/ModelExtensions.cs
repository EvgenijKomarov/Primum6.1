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
                Id = x.Id,
                CourseName = x.Course.Name,
                CourseId = x.Course.Id,
                CourseThemeName = x.Course.CourseTheme.ThemeName,
                CourseThemeId = x.Course.CourseTheme.Id,
                PricePerLesson = x.PricePerLesson,
                AbonementStatus = x.AbonementStatus,
                Rating = x.Rating,
                IsReferal = x.IsReferal,
            });

        public static IQueryable<CourseDtoLite> ToDtoLite(this IQueryable<Course> queryable) => queryable.Select(x =>
            new CourseDtoLite
            {
                Id = x.Id,
                Name = x.Name,
                TeacherName = x.Teacher.User.DisplayName,
                CourseThemeName = x.CourseTheme.ThemeName,
                About = x.About,
                CourseThemeId = x.CourseTheme.Id,
                TeacherId = x.Teacher.User.Id,
                Price = x.Price,
                MaxLessons = x.MaxLessons,
                FreeLessons = x.FreeLessons,
                TeacherAbout = x.Teacher.About,
                IsActive = x.IsActive,
                IsAvailable = AvailabilityExpressions.IsCourseAvailable.Compile()(x),
                Rank = x.Rank.Rank,
                Level = x.Rank.Level
            });

        public static IQueryable<CourseDto> ToDto(this IQueryable<Course> queryable, string gatewayUrl) => queryable.Select(x =>
            new CourseDto
            {
                Id = x.Id,
                Name = x.Name,
                TeacherName = x.Teacher.User.DisplayName,
                CourseThemeName = x.CourseTheme.ThemeName,
                About = x.About,
                CourseThemeId = x.CourseTheme.Id,
                TeacherId = x.Teacher.User.Id,
                Price = x.Price,
                MaxLessons = x.MaxLessons,
                FreeLessons = x.FreeLessons,
                TeacherAbout = x.Teacher.About,
                IsActive = x.IsActive,
                IsAvailable = AvailabilityExpressions.IsCourseAvailable.Compile()(x),
                Rank = x.Rank.Rank,
                Level = x.Rank.Level,
                Experience = x.Experience,
                OnCheck = x.ApproveStatus != ApproveStatus.Approved,
                ReferalLink = $"{gatewayUrl}/referal?token={x.ReferalToken}",
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
                Id = x.Id,
                ThemeName = x.ThemeName,
                IsActive = x.IsActive
            });

        public static IQueryable<IncidentLogDto> ToDto(this IQueryable<IncidentLog> queryable) => queryable.Select(x => 
            new IncidentLogDto
            {
                Id = x.Id,
                AdminUserId = x.AdminProfile.User.Id,
                AdminDisplayName = x.AdminProfile.User.DisplayName,
                DateTime = x.CreatedAt,
                Description = x.Description
            });

        public static IQueryable<LessonDto> ToDto(this IQueryable<Lesson> queryable, bool isStudentLink) => queryable
            .Select(x => new LessonDto
            {
                DateTime = x.DateTime,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.Id,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                StudentDisplayName = x.Abonement.Student.User.DisplayName,
                StudentId = x.Abonement.Student.User.Id,
                LessonLink = isStudentLink ? (x.StudentLink ?? string.Empty) : (x.TeacherLink ?? string.Empty),
                AbonementId = x.Abonement.Id,
                Price = x.Price,
                Id = x.Id,
                LessonStatus = x.Status,
                HomeworkGrade = x.Grading == null ? null : (int?)x.Grading.HomeworkGrade,
                LessonActivityGrade = x.Grading == null ? null : (int?)x.Grading.LessonActivityGrade,
                RepetitionOfMaterialGrade = x.Grading == null ? null : (int?)x.Grading.RepetitionOfMaterialGrade,
                StudyInitiativeGrade = x.Grading == null ? null : (int?)x.Grading.StudyInitiativeGrade,
                FinalGrade = x.Grading == null ? null : x.Grading.GetFinalGrade()
            });

        public static IQueryable<PromocodeDto> ToDto(this IQueryable<Promocode> queryable, bool isCodeSecured) => queryable.Select(x => 
            new PromocodeDto
            {
                Id = x.Id,
                StudentId = x.Student!.Id,
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
                IsAvailable = AvailabilityExpressions.IsTeacherAvailable.Compile()(x),
                Rank = x.Rank.Rank,
                Level = x.Rank.Level,
                Experience = x.Experience,
                ConvertionIndex = x.ConvertionIndex
            });

        public static IQueryable<StudentProfileDto> ToDto(this IQueryable<StudentProfile> queryable) => queryable.Select(x => 
            new StudentProfileDto
            {
                DisplayName = x.User.DisplayName,
                UserId = x.User.Id,
                Coins = x.Coins,
                Rank = x.Rank.Rank,
                Level = x.Rank.Level,
                Rating = x.Rating,
                Cash = x.Cash,
                Experience = x.Experience,
            });

        public static IQueryable<AbonementSheduleDto> ToDto(this IQueryable<AbonementShedule> queryable) => queryable.Select(x => 
            new AbonementSheduleDto
            {
                DayOfWeek = x.TeacherShedule.DayOfWeek,
                Time = x.TeacherShedule.Time,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.Id,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                Id = x.Id
            });

        public static IQueryable<TeacherSheduleDto> ToDto(this IQueryable<TeacherShedule> queryable) => queryable.Select(x => 
            new TeacherSheduleDto
            {
                Id = x.Id,
                DayOfWeek = x.DayOfWeek,
                Time = x.Time,
                IsAvailable = AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(x),
                StudentName = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Student.User.DisplayName,
                StudentId = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Student.User.Id,
                CourseName = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Course.Name,
                CourseId = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Course.Id,
                AbonementId = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Id
            });

        public static IQueryable<UserDto> ToDto(this IQueryable<User> queryable) => queryable.Select(x => 
            new UserDto
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
                Email = x.MailAdress,
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

        public static IQueryable<StudentRankDto> ToDto(this IQueryable<StudentRank> queryable) => queryable.Select(x =>
            new StudentRankDto
            {
                Id = x.Id,
                Level = x.Level,
                Rank = x.Rank,
                RequiredExperience = x.RequiredExperience,
                CoinDiscount = x.CoinDiscount,
            });

        public static IQueryable<CourseRankDto> ToDto(this IQueryable<CourseRank> queryable) => queryable.Select(x =>
            new CourseRankDto
            {
                Id = x.Id,
                Level = x.Level,
                Rank = x.Rank,
                RequiredExperience = x.RequiredExperience,
            });

        public static IQueryable<TeacherRankDto> ToDto(this IQueryable<TeacherRank> queryable) => queryable.Select(x =>
            new TeacherRankDto
            {
                Id = x.Id,
                Level = x.Level,
                Rank = x.Rank,
                RequiredExperience = x.RequiredExperience,
                EarningMultiplier = x.EarningMultiplier,
            });

        public static IQueryable<LessonsByDateDto> ToByDateDto(this IQueryable<Lesson> queryable, bool isStudentLink) => queryable
            .OrderBy(x => x.DateTime)
            .GroupBy(x => x.DateTime)
            .Select(x => new LessonsByDateDto
            {
                Date = DateOnly.FromDateTime(x.Key),
                DayOfWeek = x.Key.DayOfWeek,
                Lessons = x.Select(x => new FutureLessonDto
                {
                    AbonementId = x.AbonementId,
                    Time = x.DateTime.TimeOfDay,
                    TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                    TeacherId = x.Abonement.Course.Teacher.User.Id,
                    StudentId = x.Abonement.Student.User.Id,
                    StudentDisplayName = x.Abonement.Student.User.DisplayName,
                    Price = x.Price,
                    LessonStatus = x.Status,
                    Id = x.Id,
                    CourseName = x.Abonement.Course.Name,
                    CourseId = x.Abonement.Course.Id
                }).ToList()
            });
    }
}
