using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;

namespace PrimumCore.Services.Iterators
{
    public class SheduleIterator(IPrimumContext context, IPublisher publisher)
    {
        public async Task<IEnumerable<TeacherSheduleDto>> GetTeacherShedules(int teacherId, bool isOnlyAvailable)
        {
            var user = await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .ThenInclude(x => x.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.TeacherProfile)
                .ThenInclude(x => x.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user.TeacherProfile
                .TeacherShedules
                .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherSheduleAvailable)
                .Select(x => new TeacherSheduleDto
                {
                    TeacherSheduleId = x.TeacherSheduleId,
                    DayOfWeek = x.DayOfWeek,
                    Time = x.Time,
                    IsBusy = !AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(x),
                    StudentName = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Student.User.DisplayName : null,
                    StudentId = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Student.User.Id : null,
                    CourseName = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Course.Name : null,
                    CourseId = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Course.CourseId : null,
                }
                )
                .ToArray();
        }

        public async Task<TeacherSheduleDto> GetTeacherShedule(int teacherId, int sheduleId, bool isOnlyAvailable)
        {
            var shedule = (await GetTeacherShedules(teacherId, isOnlyAvailable))
                .FirstOrDefault(x => x.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new Exception("Shedule not found"); }

            return shedule;
        }

        public async Task<IEnumerable<StudentSheduleDto>> GetAbonementShedules(int abonementId)
        {
            var abonement = await context.Set<Abonement>()
                .Include(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .Include(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new Exception("Abonement not found"); }

            return abonement.AbonementShedules.Select(x => new StudentSheduleDto
            {
                DayOfWeek = x.TeacherShedule.DayOfWeek,
                Time = x.TeacherShedule.Time,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.CourseId,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                AbonementSheduleId = x.AbonementSheduleId
            });
        }

        public async Task<int> CreateTeacherShedule(int teacherId, TeacherSheduleInputDto sheduleDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) { throw new Exception("Teacher is not approved"); }

            if (user.TeacherProfile.TeacherShedules.Any(s => s.DayOfWeek == sheduleDto.DayOfWeek && s.Time == sheduleDto.Time)) 
                { throw new Exception("Shedule already exists"); }

            var shedule = new TeacherShedule
            {
                Time = sheduleDto.Time,
                DayOfWeek = sheduleDto.DayOfWeek,
            };

            user.TeacherProfile.TeacherShedules.Add(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }

        public async Task<int> DeleteTeacherShedule(int teacherId, int sheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .ThenInclude(e => e.AbonementShedule)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) { throw new Exception("Teacher is not approved"); }

            var shedule = user.TeacherProfile.TeacherShedules.FirstOrDefault(s => s.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new Exception("Shedule not found"); }
            if (!AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(shedule)) { throw new Exception("Shedule already busy"); }

            user.TeacherProfile.TeacherShedules.Remove(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }

        public async Task<IEnumerable<StudentSheduleDto>> GetStudentShedules(int studentId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Course)
                .ThenInclude(s => s.Teacher)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            return user
                .StudentProfile
                .Abonements
                .SelectMany(a => a.AbonementShedules)
                .Select(x => new StudentSheduleDto
                {
                    DayOfWeek = x.TeacherShedule.DayOfWeek,
                    Time = x.TeacherShedule.Time,
                    TeacherId = x.Abonement.Course.Teacher.User.Id,
                    CourseId = x.Abonement.Course.CourseId,
                    TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                    CourseName = x.Abonement.Course.Name,
                    AbonementSheduleId = x.AbonementSheduleId
                })
                .ToArray();
        }

        public async Task<StudentSheduleDto> GetStudentShedule(int studentId, int sheduleId)
        {
            var shedule = (await GetStudentShedules(studentId))
                .FirstOrDefault(x => x.AbonementSheduleId == sheduleId);
            if (shedule is null) { throw new Exception("Shedule not found"); }

            return shedule;
        }

        public async Task<int> DeleteStudentShedule(int studentId, int abonementSheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Course)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.AbonementShedules)
                .ThenInclude(s => s.TeacherShedule)
                .ThenInclude(s => s.Teacher)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var abonementShedule = user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .FirstOrDefault(x => x.AbonementSheduleId == abonementSheduleId);

            abonementShedule.Abonement.AbonementShedules.Remove(abonementShedule);
            await context.SaveChangesAsync();

            await publisher.PublishAsync(new DeleteAbonementSheduleNotification
            {
                StudentName = user.DisplayName,
                StudentUserId = user.Id,
                TeacherName = abonementShedule.TeacherShedule.Teacher.User.DisplayName,
                TeacherUserId = abonementShedule.TeacherShedule.Teacher.User.Id,
                CourseName = abonementShedule.Abonement.Course.Name,
                AbonementId = abonementShedule.Abonement.AbonementId,
                AbonementSheduleId = abonementShedule.AbonementSheduleId,
                DayOfWeek = abonementShedule.TeacherShedule.DayOfWeek.ToString(),
                Time = abonementShedule.TeacherShedule.Time
            });

            return abonementShedule.AbonementSheduleId;
        }
    }
}
