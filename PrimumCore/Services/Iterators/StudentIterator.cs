using CoreConnection.DTOs;
using CoreConnection.Enums;
using CoreConnection.Notifications;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;
using System.Linq;

namespace PrimumCore.Services.Iterators
{
    public class StudentIterator(IPrimumContext context, ConverterToDateTimeService dateTimeService, IPublisher publisher)
    {
        public async Task<StudentProfileDto> GetStudentProfile(int studentId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new NotFoundException("Student"); }

            return new StudentProfileDto 
            { 
                DisplayName = user.DisplayName,
                UserId = user.Id,
                Coins = user.StudentProfile.Coins
            };
        }

        public async Task<int> SubscribeToCourse(int studentId, int courseId, int teacherSheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Course)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new NotFoundException("Student"); }

            var course = await context.Set<Course>()
                .FirstOrDefaultAsync(x => x.CourseId == courseId);
            if (course is null) { throw new NotFoundException("Course"); }

            // strict availability check executed in-memory
            if (!AvailabilityExpressions.IsCourseAvailable.Compile()(course)) { throw new NotFoundException("Course"); }

            var teacherShedule = await context.Set<TeacherShedule>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.TeacherSheduleId == teacherSheduleId);
            if (teacherShedule is null) { throw new NotFoundException("Shedule"); }
            if (teacherShedule.Teacher.ApproveStatus != ApproveStatus.Approved) { throw new NotAvailableException("Teacher is not approved"); }
            if (!AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(teacherShedule)) { throw new BusinessLogicException("Shedule is busy"); }
            if (teacherShedule.Teacher.User?.Id == studentId) { throw new BusinessLogicException("Student can't subscribe on himself"); }
            if (user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .Any(x => x.TeacherShedule.DayOfWeek == teacherShedule.DayOfWeek && x.TeacherShedule.Time == teacherShedule.Time))
            {
                throw new BusinessLogicException("Same shedule already exists");
            }

            var abonement = user.StudentProfile.Abonements.FirstOrDefault(a => a.CourseId == courseId);

            if (abonement is null)
            {
                abonement = new Abonement
                {
                    Course = course,
                    PricePerLesson = course.Price,
                    FreeLessons = course.FreeLessons
                };
                user.StudentProfile.Abonements.Add(abonement);
            } else if (abonement.AbonementStatus == AbonementStatus.Deleted)
            {
                abonement.AbonementStatus = AbonementStatus.Active;
            }

            if (course.MaxLessons <= abonement.AbonementShedules.Count)
            {
                throw new BusinessLogicException("Can't create more shedules than course's maximum shedules per week");
            }
            if (user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .Any(x => x.TeacherShedule.Time == teacherShedule.Time && x.TeacherShedule.DayOfWeek == teacherShedule.DayOfWeek))
            {
                throw new BusinessLogicException("There is a same shedule in student profile");
            }

            var suitableDate = dateTimeService.GetNextFreeSuitableDateThisWeek(teacherShedule.DayOfWeek, teacherShedule.Time);
            var abonementShedule = new AbonementShedule
            {
                TeacherShedule = teacherShedule,
                LastIteration = suitableDate,
            };
            abonement.AbonementShedules.Add(abonementShedule);

            await context.Set<AbonementShedule>().AddAsync(abonementShedule);

            if (!context.Set<Lesson>()
                .Include(x => x.Abonement)
                .Any(x => x.DateTime == suitableDate && x.Abonement.AbonementId == abonement.AbonementId))
            {
                await context.Set<Lesson>().AddAsync(new Lesson
                {
                    Abonement = abonement,
                    Price = abonement.Course.FreeLessons >= abonement.Lessons.Count() ? 0 : abonement.PricePerLesson,
                    DateTime = suitableDate,
                    Status = LessonStatus.Waiting
                });
            }

            await context.SaveChangesAsync();

            await publisher.PublishAsync(new NewAbonementSheduleNotification
            {
                StudentName = user.DisplayName,
                StudentUserId = user.Id,
                TeacherName = teacherShedule.Teacher.User.DisplayName,
                TeacherUserId = teacherShedule.Teacher.User.Id,
                CourseName = course.Name,
                AbonementId = abonement.AbonementId,
                AbonementSheduleId = abonementShedule.AbonementSheduleId,
                DayOfWeek = teacherShedule.DayOfWeek.ToString(),
                Time = teacherShedule.Time
            });

            return abonementShedule.AbonementSheduleId;
        }
    }
}
