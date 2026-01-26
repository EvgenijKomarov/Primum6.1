using CoreConnection.DTOs;
using CoreConnection.Enums;
using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Iterators
{
    public class StudentIterator(IPrimumContext context, ConverterToDateTimeService dateTimeService, IPublisher publisher)
    {
        public async Task<StudentProfileDto> GetStudentProfile(int studentId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            return new StudentProfileDto 
            { 
                DisplayName = user.Name,
                UserId = user.Id
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
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var course = await context.Set<Course>()
                .Where(x => x.IsAvailable)
                .FirstOrDefaultAsync(x => x.CourseId == courseId);
            if (course is null) { throw new Exception("Course not found"); }

            var teacherShedule = await context.Set<TeacherShedule>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(x => !x.IsBusy)
                .FirstOrDefaultAsync(x => x.TeacherSheduleId == teacherSheduleId);
            if (teacherShedule.Teacher.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }
            if (teacherShedule is null) { throw new Exception("Shedule not found"); }
            if (teacherShedule.IsBusy) { throw new Exception("Shedule is busy"); }
            if (teacherShedule.Teacher.User.Id == studentId) { throw new Exception("Student can't subscribe on himself"); }
            if (user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .Any(x => x.TeacherShedule.DayOfWeek == teacherShedule.DayOfWeek && x.TeacherShedule.Time == teacherShedule.Time))
            {
                throw new Exception("Same shedule already exists");
            }

            var abonement = user.StudentProfile.Abonements.FirstOrDefault(a => a.CourseId == courseId);

            if (abonement is null)
            {
                abonement = new Abonement
                {
                    CourseId = courseId,
                    StudentId = studentId,
                    PricePerLesson = course.Price
                };
                await context.Set<Abonement>().AddAsync(abonement);
            } else if (abonement.AbonementStatus == AbonementStatus.Deleted)
            {
                abonement.AbonementStatus = AbonementStatus.Active;
            }

            if (course.MaxLessons >= abonement.AbonementShedules.Count)
            {
                throw new Exception("Can't create more shedules than course's maximum shedules per week");
            }
            if (user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .Any(x => x.TeacherShedule.Time == teacherShedule.Time && x.TeacherShedule.DayOfWeek == teacherShedule.DayOfWeek))
            {
                throw new Exception("There is a same shedule in student profile");
            }

            var suitableDate = dateTimeService.GetNextFreeSuitableDateThisWeek(teacherShedule.DayOfWeek, teacherShedule.Time);
            var abonementShedule = new AbonementShedule
            {
                Abonement = abonement,
                TeacherShedule = teacherShedule,
                LastIteration = suitableDate,
            };

            await context.Set<AbonementShedule>().AddAsync(abonementShedule);
            await context.Set<Lesson>().AddAsync(new Lesson
            {
                Abonement = abonement,
                Price = abonement.Course.FreeLessons >= abonement.Lessons.Count() ? 0 : abonement.PricePerLesson,
                DateTime = suitableDate,
                Status = LessonStatus.Waiting
            });

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
