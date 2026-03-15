using CoreDBModel.Extensions;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using LinqKit;
using System.Linq.Expressions;

namespace CoreDBModel.Constants
{
    public static class AvailabilityExpressions
    {
        public static Expression<Func<User, bool>> IsUserAvailable =>
            IsUserAvailableBase;

        public static Expression<Func<Promocode, bool>> IsPromocodeAvailable =>
            IsPromocodeAvailableBase;

        public static Expression<Func<TeacherShedule, bool>> IsTeacherSheduleAvailable =>
            IsTeacherSheduleAvailableBase.AndWithProperty(s => s.Teacher, IsTeacherAvailable);

        public static Expression<Func<TeacherProfile, bool>> IsTeacherAvailable =>
            IsTeacherAvailableBase.AndWithProperty(t => t.User, IsUserAvailable);

        public static Expression<Func<Course, bool>> IsCourseAvailable =>
            IsCourseAvailableBase
            .AndWithProperty(s => s.Teacher, IsTeacherAvailable)
            .AndWithProperty(s => s.CourseTheme, IsThemeAvailable);

        public static Expression<Func<Abonement, bool>> IsAbonementAvailable =>
            IsAbonementAvailableBase;

        public static Expression<Func<Abonement, bool>> IsAbonementAlive =>
            IsAbonementAliveBase;

        public static Expression<Func<CourseTheme, bool>> IsThemeAvailable =>
            IsThemeAvailableBase;


        private static Expression<Func<User, bool>> IsUserAvailableBase =>
            u => !u.IsBanned && u.IsMailChecked;

        private static Expression<Func<Promocode, bool>> IsPromocodeAvailableBase =>
            u => u.Student == null;

        private static Expression<Func<TeacherShedule, bool>> IsTeacherSheduleAvailableBase =>
            u => u.AbonementShedule == null;

        private static Expression<Func<TeacherProfile, bool>> IsTeacherAvailableBase =>
            t => t.ApproveStatus == ApproveStatus.Approved;

        private static Expression<Func<Course, bool>> IsCourseAvailableBase =>
            u => u.ApproveStatus == ApproveStatus.Approved && u.IsActive;

        private static Expression<Func<Abonement, bool>> IsAbonementAvailableBase =>
            u => u.AbonementStatus == AbonementStatus.Active;

        private static Expression<Func<Abonement, bool>> IsAbonementAliveBase =>
            u => u.AbonementStatus != AbonementStatus.Deleted;

        private static Expression<Func<CourseTheme, bool>> IsThemeAvailableBase =>
            u => u.IsActive == true;
    }
}
