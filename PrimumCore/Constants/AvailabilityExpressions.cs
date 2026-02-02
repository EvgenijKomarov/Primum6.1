using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;
using System.Linq.Expressions;

namespace PrimumCore.Constants
{
    public static class AvailabilityExpressions
    {
        public static Expression<Func<User, bool>> IsUserAvailable =>
            u => !u.IsBanned && u.IsMailChecked;

        public static Expression<Func<Promocode, bool>> IsPromocodeAvailable =>
            u => u.Student == null;

        public static Expression<Func<TeacherShedule, bool>> IsTeacherSheduleAvailable =>
            u => u.AbonementShedule == null;

        public static Expression<Func<User, bool>> IsTeacherAvailable =>
            u => u.TeacherProfile != null && u.TeacherProfile.ApproveStatus == ApproveStatus.Approved;

        public static Expression<Func<Course, bool>> IsCourseAvailable =>
            u => u.ApproveStatus == ApproveStatus.Approved && u.IsActive;

        public static Expression<Func<Abonement, bool>> IsAbonementAvailable =>
            u => u.AbonementStatus == AbonementStatus.Active;

        public static Expression<Func<Abonement, bool>> IsAbonementAlive =>
            u => u.AbonementStatus != AbonementStatus.Deleted;
    }
}
