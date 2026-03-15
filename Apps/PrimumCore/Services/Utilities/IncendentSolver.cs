using CoreConnection.DTOs.Inputs;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Services.Utilities
{
    public class IncidentSolver(DatabaseIterator dbIterator)
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

            await dbIterator.AddAsync(new IncidentLog
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
            await dbIterator.SaveChangesAsync();

            return ruleResult;
        }

        private Dictionary<IncidentMeaning, Func<int, IncidentDecision, Task<int>>> solvingRules
            = new Dictionary<IncidentMeaning, Func<int, IncidentDecision, Task<int>>>
        {
            {
                IncidentMeaning.Teacher,
                async (id, decision) =>
                {
                    var teacher = await dbIterator.Teachers(false)
                        .Include(x => x.TeacherShedules)
                        .Include(x => x.Courses)
                        .One(x => x.User.Id == id);

                    switch(decision) 
                    {
                        case IncidentDecision.SendToAdministrator:
                            teacher.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecision.SendToManager:
                            teacher.ApproveStatus = ApproveStatus.NeedManagerReview;
                            break;
                        case IncidentDecision.Approve:
                            teacher.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecision.Delete:
                            await dbIterator.RemoveRangeAsync(teacher.TeacherShedules);
                            await dbIterator.RemoveRangeAsync(teacher.Courses);
                            await dbIterator.RemoveAsync(teacher);
                            break;
                    }
                    return teacher.User.Id;
                }
            },
            {
                IncidentMeaning.Student,
                async (id, decision) =>
                {
                    var student = await dbIterator.Students()
                        .One(x => x.User.Id == id);

                    switch(decision)
                    {
                        case IncidentDecision.SendToAdministrator:
                            student.ApproveStatus = ApproveStatus.NeedAdministratorReview;
                            break;
                        case IncidentDecision.Approve:
                            student.ApproveStatus = ApproveStatus.Approved;
                            break;
                        case IncidentDecision.Delete:
                            await dbIterator.RemoveRangeAsync(student.Abonements.SelectMany(x => x.AbonementShedules));
                            await dbIterator.RemoveRangeAsync(student.Abonements.SelectMany(x => x.Lessons));
                            await dbIterator.RemoveRangeAsync(student.Abonements);
                            await dbIterator.RemoveAsync(student);
                            break;
                    }
                    return student.User.Id;
                }
            },
            {
                IncidentMeaning.Course,
                async (id, decision) =>
                {
                    var course = await dbIterator.Courses(false)
                        .One(x => x.Id == id);

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
                            await dbIterator.RemoveRangeAsync(course.Abonements.SelectMany(x => x.AbonementShedules));
                            await dbIterator.RemoveRangeAsync(course.Abonements.SelectMany(x => x.Lessons));
                            await dbIterator.RemoveRangeAsync(course.Abonements);
                            await dbIterator.RemoveAsync(course);
                            break;
                    }
                    return course.Id;
                }
            },
            {
                IncidentMeaning.Lesson,
                async (id, decision) =>
                {
                    var lesson = await dbIterator.Lessons()
                        .One(x => x.Id == id);

                    switch(decision)
                    {
                        case IncidentDecision.Delete:
                            await dbIterator.RemoveAsync(lesson);
                            break;
                        case IncidentDecision.Revisioned:
                            lesson.Status = LessonStatus.MissedWithoutReason;
                            break;
                        case IncidentDecision.BanUser:
                            lesson.Abonement.Student.User.IsBanned = true;
                            await dbIterator.RemoveRangeAsync(lesson.Abonement.Lessons);
                            await dbIterator.RemoveRangeAsync(lesson.Abonement.AbonementShedules);
                            await dbIterator.RemoveRangeAsync(lesson.Abonement.Student.Abonements);
                            break;
                    }
                    return lesson.Id;
                }
            },
        };
    }
}
