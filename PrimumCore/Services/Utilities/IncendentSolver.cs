using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Utilities
{
    public class IncendentSolver(IPrimumContext context)
    {
        //User should be identified
        public async Task<int> SolveIncendent(Permission[] permissions, IncendentDecisionInputDto dto)
        {
            if (!solvingRules.TryGetValue(dto.Meaning, out Func<int, IncendentDecisionDto, Task<int>> rule)) { throw new Exception("Unknown incindent"); }
            if(permissions
                .Any(x => x.GetAvailableIncendentsAttributes()
                    .Any(y => y.Meaning == dto.Meaning && y.Decision == dto.Decision)
                    )
                ) { throw new Exception("User haven't needed permissions"); }

            return await rule.Invoke(dto.ObjectId, dto.Decision);
        }

        private Dictionary<IncendentMeaningDto, Func<int, IncendentDecisionDto, Task<int>>> solvingRules
            = new Dictionary<IncendentMeaningDto, Func<int, IncendentDecisionDto, Task<int>>>
        {
            {
                IncendentMeaningDto.Teacher,
                async (id, decision) =>
                {
                    var user = context.Set<User>()
                        .Include(x => x.TeacherProfile)
                        .ThenInclude(x => x.TeacherShedules)
                        .Include(x => x.TeacherProfile)
                        .ThenInclude(x => x.Courses)
                        .FirstOrDefault(x => x.Id == id);
                    if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

                    switch(decision) 
                    {
                        case IncendentDecisionDto.SendToAdministrator:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncendentDecisionDto.SendToManager:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.NeedManagerReview;
                            break;
                        case IncendentDecisionDto.Approve:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncendentDecisionDto.Delete:
                            context.Set<TeacherShedule>().RemoveRange(user.TeacherProfile.TeacherShedules);
                            context.Set<Course>().RemoveRange(user.TeacherProfile.Courses);
                            context.Set<TeacherProfile>().Remove(user.TeacherProfile);
                            break;
                    }
                    return user.Id;
                }
            },
            {
                IncendentMeaningDto.Student,
                async (id, decision) =>
                {
                    var user = context.Set<User>()
                        .Include(x => x.TeacherProfile)
                        .ThenInclude(x => x.TeacherShedules)
                        .Include(x => x.TeacherProfile)
                        .ThenInclude(x => x.Courses)
                        .FirstOrDefault(x => x.Id == id);
                    if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

                    switch(decision)
                    {
                        case IncendentDecisionDto.SendToAdministrator:
                            user.StudentProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncendentDecisionDto.Approve:
                            user.StudentProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncendentDecisionDto.Delete:
                            context.Set<AbonementShedule>().RemoveRange(user.StudentProfile.Abonements.SelectMany(x => x.AbonementShedules));
                            context.Set<Lesson>().RemoveRange(user.StudentProfile.Abonements.SelectMany(x => x.Lessons));
                            context.Set<Abonement>().RemoveRange(user.StudentProfile.Abonements);
                            context.Set<StudentProfile>().Remove(user.StudentProfile);
                            break;
                    }
                    return user.Id;
                }
            },
            {
                IncendentMeaningDto.Course,
                async (id, decision) =>
                {
                    var course = context.Set<Course>()
                        .Include(x => x.Abonements)
                        .ThenInclude(x => x.Lessons)
                        .Include(x => x.Abonements)
                        .ThenInclude(x => x.AbonementShedules)
                        .FirstOrDefault(x => x.CourseId == id);
                    if (course is null) { throw new Exception("Course not found"); }

                    switch(decision)
                    {
                        case IncendentDecisionDto.SendToAdministrator:
                            course.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncendentDecisionDto.Approve:
                            course.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncendentDecisionDto.SendToManager:
                            course.ApproveStatus = ApproveStatus.NeedManagerReview;
                            break;
                        case IncendentDecisionDto.Delete:
                            context.Set<AbonementShedule>().RemoveRange(course.Abonements.SelectMany(x => x.AbonementShedules));
                            context.Set<Lesson>().RemoveRange(course.Abonements.SelectMany(x => x.Lessons));
                            context.Set<Abonement>().RemoveRange(course.Abonements);
                            context.Set<Course>().Remove(course);
                            break;
                    }
                    return course.CourseId;
                }
            },
            {
                IncendentMeaningDto.Lesson,
                async (id, decision) =>
                {
                    var lesson = context.Set<Lesson>()
                        .FirstOrDefault(x => x.LessonId == id);
                    if (lesson is null) { throw new Exception("Lesson not found"); }

                    switch(decision)
                    {
                        case IncendentDecisionDto.Delete:
                            context.Set<Lesson>().RemoveRange(lesson);
                            break;
                    }
                    return lesson.LessonId;
                }
            },
        };
    }
}
