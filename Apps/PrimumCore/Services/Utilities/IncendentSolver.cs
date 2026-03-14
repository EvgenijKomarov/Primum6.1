using CoreConnection.DTOs.Inputs;
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
            if (!solvingRules.TryGetValue(dto.Meaning, out Func<int, IncidentDecision, Task<int>> rule)) { throw new NotFoundException("Incindent"); }
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
                Meaning = (IncidentMeaning)dto.Meaning,
                ObjectId = dto.ObjectId
            });

            var ruleResult = await rule.Invoke(dto.ObjectId, dto.Decision);
            await context.SaveChangesAsync();

            return ruleResult;
        }

        private Dictionary<IncidentMeaning, Func<int, IncidentDecision, Task<int>>> solvingRules
            = new Dictionary<IncidentMeaning, Func<int, IncidentDecision, Task<int>>>
        {
            {
                IncidentMeaning.Teacher,
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
                        case IncidentDecision.SendToAdministrator:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecision.SendToManager:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.NeedManagerReview;
                            break;
                        case IncidentDecision.Approve:
                            user.TeacherProfile.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecision.Delete:
                            context.Set<TeacherShedule>().RemoveRange(user.TeacherProfile.TeacherShedules);
                            context.Set<Course>().RemoveRange(user.TeacherProfile.Courses);
                            context.Set<TeacherProfile>().Remove(user.TeacherProfile);
                            break;
                    }
                    return user.Id;
                }
            },
            {
                IncidentMeaning.Student,
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
                        case IncidentDecision.SendToAdministrator:
                            user.StudentProfile.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecision.Approve:
                            user.StudentProfile.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecision.Delete:
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
                IncidentMeaning.Course,
                async (id, decision) =>
                {
                    var course = context.Set<Course>()
                        .Include(x => x.Abonements)
                        .ThenInclude(x => x.Lessons)
                        .Include(x => x.Abonements)
                        .ThenInclude(x => x.AbonementShedules)
                        .FirstOrDefault(x => x.Id == id);
                    if (course is null) { throw new NotFoundException("Course"); }

                    switch(decision)
                    {
                        case IncidentDecision.SendToAdministrator:
                            course.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecision.Approve:
                            course.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecision.SendToManager:
                            course.ApproveStatus = ApproveStatus.NeedManagerReview;
                            break;
                        case IncidentDecision.Delete:
                            context.Set<AbonementShedule>().RemoveRange(course.Abonements.SelectMany(x => x.AbonementShedules));
                            context.Set<Lesson>().RemoveRange(course.Abonements.SelectMany(x => x.Lessons));
                            context.Set<Abonement>().RemoveRange(course.Abonements);
                            context.Set<Course>().Remove(course);
                            break;
                    }
                    return course.Id;
                }
            },
            {
                IncidentMeaning.Lesson,
                async (id, decision) =>
                {
                    var lesson = context.Set<Lesson>()
                        .Include(x => x.Abonement)
                        .ThenInclude(a => a.Lessons)
                        .Include(x => x.Abonement)
                        .ThenInclude(x => x.Student)
                        .ThenInclude(x => x.User)
                        .Include(x => x.Abonement)
                        .ThenInclude(x => x.Student)
                        .ThenInclude(x => x.Abonements)
                        .FirstOrDefault(x => x.Id == id);
                    if (lesson is null) { throw new NotFoundException("Lesson"); }

                    switch(decision)
                    {
                        case IncidentDecision.Delete:
                            context.Set<Lesson>().Remove(lesson);
                            break;
                        case IncidentDecision.Revisioned:
                            lesson.Status = LessonStatus.MissedWithoutReason;
                            break;
                        case IncidentDecision.BanUser:
                            lesson.Abonement.Student.User.IsBanned = true;
                            context.Set<Lesson>().RemoveRange(lesson.Abonement.Lessons);
                            context.Set<AbonementShedule>().RemoveRange(lesson.Abonement.AbonementShedules);
                            context.Set<Abonement>().RemoveRange(lesson.Abonement.Student.Abonements);
                            break;
                    }
                    return lesson.Id;
                }
            },
        };
    }
}
