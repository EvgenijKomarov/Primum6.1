using LinqKit;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;
using System.Linq.Expressions;

namespace PrimumCore.Constants
{
    public static class AvailabilityExpressions
    {
        public static Expression<Func<User, bool>> IsUserAvailable =>
            IsUserAvailableBase;

        public static Expression<Func<Promocode, bool>> IsPromocodeAvailable =>
            IsPromocodeAvailableBase;

        public static Expression<Func<TeacherShedule, bool>> IsTeacherSheduleAvailable =>
            IsTeacherSheduleAvailableBase.AndWithProperty(s => s.Teacher.User, IsTeacherAvailable);

        public static Expression<Func<User, bool>> IsTeacherAvailable =>
            IsUserAvailable.And(IsTeacherAvailableBase);

        public static Expression<Func<Course, bool>> IsCourseAvailable =>
            IsCourseAvailableBase.AndWithProperty(s => s.Teacher.User, IsTeacherAvailable);

        public static Expression<Func<Abonement, bool>> IsAbonementAvailable =>
            IsAbonementAvailableBase;

        public static Expression<Func<Abonement, bool>> IsAbonementAlive =>
            IsAbonementAliveBase;


        private static Expression<Func<User, bool>> IsUserAvailableBase =>
            u => !u.IsBanned && u.IsMailChecked;

        private static Expression<Func<Promocode, bool>> IsPromocodeAvailableBase =>
            u => u.Student == null;

        private static Expression<Func<TeacherShedule, bool>> IsTeacherSheduleAvailableBase =>
            u => u.AbonementShedule == null;

        private static Expression<Func<User, bool>> IsTeacherAvailableBase =>
            u => u.TeacherProfile != null && u.TeacherProfile.ApproveStatus == ApproveStatus.Approved;

        private static Expression<Func<Course, bool>> IsCourseAvailableBase =>
            u => u.ApproveStatus == ApproveStatus.Approved && u.IsActive;

        private static Expression<Func<Abonement, bool>> IsAbonementAvailableBase =>
            u => u.AbonementStatus == AbonementStatus.Active;

        private static Expression<Func<Abonement, bool>> IsAbonementAliveBase =>
            u => u.AbonementStatus != AbonementStatus.Deleted;
    }
}
