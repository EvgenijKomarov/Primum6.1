using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Constants;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;

namespace PrimumCore.Services.Iterators
{
    public class SheduleIterator(PrimumContext context, PublisherService publisher)
    {
        public async Task<IEnumerable<TeacherSheduleDto>> GetTeacherShedules(int teacherId, bool isOnlyAvailable)
        {
            var shedules = await context.Set<TeacherShedule>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .Where(x => x.Teacher.User.Id == teacherId)
                .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherSheduleAvailable)
                .ToArrayAsync();

            return shedules
                .Select(x => new TeacherSheduleDto
                {
                    TeacherSheduleId = x.TeacherSheduleId,
                    DayOfWeek = x.DayOfWeek,
                    Time = x.Time,
                    IsAvailable = AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(x),
                    StudentName = x.AbonementShedule?.Abonement?.Student?.User?.DisplayName,
                    StudentId = x.AbonementShedule?.Abonement?.Student?.User?.Id,
                    CourseName = x.AbonementShedule?.Abonement?.Course?.Name,
                    CourseId = x.AbonementShedule?.Abonement?.Course?.CourseId,
                }
                )
                .ToArray();
        }

        public async Task<TeacherSheduleDto> GetTeacherShedule(int teacherId, int sheduleId, bool isOnlyAvailable)
        {
            var shedule = (await GetTeacherShedules(teacherId, isOnlyAvailable))
                .FirstOrDefault(x => x.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new NotFoundException("Shedule"); }

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
            if (abonement is null) { throw new NotFoundException("Abonement"); }

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
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) { throw new NotAvailableException("Teacher"); }

            if (user.TeacherProfile.TeacherShedules.Any(s => s.DayOfWeek == sheduleDto.DayOfWeek && s.Time == sheduleDto.Time)) 
                { throw new BusinessLogicException("Shedule already exists"); }

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
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) { throw new BusinessLogicException("Teacher"); }

            var shedule = user.TeacherProfile.TeacherShedules.FirstOrDefault(s => s.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new NotFoundException("Shedule"); }
            if (!AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(shedule)) { throw new BusinessLogicException("Shedule already busy"); }

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
            if (user is null || user.StudentProfile is null) { throw new NotFoundException("Student"); }

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
            if (shedule is null) { throw new NotFoundException("Shedule"); }

            return shedule;
        }

        public async Task<int> DeleteStudentShedule(int studentId, int abonementSheduleId)
        {
            var abonementShedule = await context.Set<AbonementShedule>()
                .Include(x => x.TeacherShedule)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.AbonementSheduleId == abonementSheduleId);
            if (abonementShedule is null) { throw new NotFoundException("Shedule"); }
            if (abonementShedule.Abonement.Student.User.Id != studentId) { throw new BusinessLogicException("Only owner can delete shedule"); }

            var notification = new DeleteAbonementSheduleEvent
            {
                StudentName = abonementShedule.Abonement.Student.User.DisplayName,
                StudentUserId = abonementShedule.Abonement.Student.User.Id,
                TeacherName = abonementShedule.TeacherShedule.Teacher.User.DisplayName,
                TeacherUserId = abonementShedule.TeacherShedule.Teacher.User.Id,
                CourseName = abonementShedule.Abonement.Course.Name,
                AbonementId = abonementShedule.Abonement.AbonementId,
                AbonementSheduleId = abonementShedule.AbonementSheduleId,
                DayOfWeek = abonementShedule.TeacherShedule.DayOfWeek.ToString(),
                Time = abonementShedule.TeacherShedule.Time
            };
            abonementShedule.Abonement.AbonementShedules.Remove(abonementShedule);
            await context.SaveChangesAsync();

            await publisher.Push(notification);
            return abonementShedule.AbonementSheduleId;
        }
    }
}
