using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using CoreDBModel.Models;
using CoreDBModel.Extensions;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Utilities
{
    public class IncidentSolver(PrimumContext context)
    {
        //User should be identified
        public virtual async Task<int> SolveIncident(int adminProfileId, Permission[] permissions, IncidentDecisionInputDto dto, int userId)
        {
            if (!solvingRules.TryGetValue(dto.Meaning, out Func<int, IncidentDecisionDto, Task<int>> rule)) { throw new NotFoundException("Incindent"); }
            if(!permissions
                .Any(x => x.GetAvailableIncidentsAttributes()
                    .Any(y => y.Meaning == dto.Meaning && y.Decision == dto.Decision)
                    )
                ) { throw new NoPermissionException(userId, dto.Meaning, dto.Decision); }

            context.Set<IncidentLog>().Add(new IncidentLog
            {
                AdminProfileId = adminProfileId,
                Description = $"Explanation:\n" +
                $"{dto.DecisionExplanation}\n" +
                $"Object: {dto.Meaning.ToString()} with Id {dto.ObjectId}\n" +
                $"Decision: {dto.Decision.ToString()}",
                DecisionDate = DateTime.Now,
                Meaning = dto.Meaning,
                ObjectId = dto.ObjectId
            });

            var ruleResult = await rule.Invoke(dto.ObjectId, dto.Decision);
            await context.SaveChangesAsync();

            return ruleResult;
        }

        private Dictionary<IncidentMeaningDto, Func<int, IncidentDecisionDto, Task<int>>> solvingRules
            = new Dictionary<IncidentMeaningDto, Func<int, IncidentDecisionDto, Task<int>>>
        {
            {
                IncidentMeaningDto.Teacher,
                async (id, decision) =>
                {
                    var user = context.Set<User>()
                        .Include(x => x.TeacherProfile)
                        .ThenInclude(x => x.TeacherShedules)
                        .Include(x => x.TeacherProfile)
                        .ThenInclude(x => x.Courses)
                        .FirstOrDefault(x => x.Id == id);
                    if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }

                    switch(decision) 
                    {
                        case IncidentDecisionDto.SendToAdministrator:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecisionDto.SendToManager:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.NeedManagerReview;
                            break;
                        case IncidentDecisionDto.Approve:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecisionDto.Delete:
                            context.Set<TeacherShedule>().RemoveRange(user.TeacherProfile.TeacherShedules);
                            context.Set<Course>().RemoveRange(user.TeacherProfile.Courses);
                            context.Set<TeacherProfile>().Remove(user.TeacherProfile);
                            break;
                    }
                    return user.Id;
                }
            },
            {
                IncidentMeaningDto.Student,
                async (id, decision) =>
                {
                    var user = context.Set<User>()
                        .Include(x => x.StudentProfile)
                        .ThenInclude(x => x.Abonements)
                        .ThenInclude(x => x.Lessons)
                        .Include(x => x.StudentProfile)
                        .ThenInclude(x => x.Abonements)
                        .ThenInclude(x => x.AbonementShedules)
                        .FirstOrDefault(x => x.Id == id);
                    if (user is null || user.StudentProfile is null) { throw new NotFoundException("Student"); }

                    switch(decision)
                    {
                        case IncidentDecisionDto.SendToAdministrator:
                            user.StudentProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecisionDto.Approve:
                            user.StudentProfile.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecisionDto.Delete:
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
                IncidentMeaningDto.Course,
                async (id, decision) =>
                {
                    var course = context.Set<Course>()
                        .Include(x => x.Abonements)
                        .ThenInclude(x => x.Lessons)
                        .Include(x => x.Abonements)
                        .ThenInclude(x => x.AbonementShedules)
                        .FirstOrDefault(x => x.CourseId == id);
                    if (course is null) { throw new NotFoundException("Course"); }

                    switch(decision)
                    {
                        case IncidentDecisionDto.SendToAdministrator:
                            course.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecisionDto.Approve:
                            course.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecisionDto.SendToManager:
                            course.ApproveStatus = ApproveStatus.NeedManagerReview;
                            break;
                        case IncidentDecisionDto.Delete:
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
                IncidentMeaningDto.Lesson,
                async (id, decision) =>
                {
                    var lesson = context.Set<Lesson>()
                        .FirstOrDefault(x => x.LessonId == id);
                    if (lesson is null) { throw new NotFoundException("Lesson"); }

                    switch(decision)
                    {
                        case IncidentDecisionDto.Delete:
                            context.Set<Lesson>().Remove(lesson);
                            break;
                        case IncidentDecisionDto.Revisioned:
                            lesson.Status = LessonStatus.MissedWithoutReason;
                            break;
                    }
                    return lesson.LessonId;
                }
            },
        };
    }
}
