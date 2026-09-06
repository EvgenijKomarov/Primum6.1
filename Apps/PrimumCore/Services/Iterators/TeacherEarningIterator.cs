using Common.Utilities;
using CoreConnection.DTOs;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherEarningIterator(DatabaseIterator dbIterator, EarningCalculationService service)
    {
        public async Task<TeacherEarningDto> GetTeacherEarning(int teacherId)
        {
            var teacher = await dbIterator.Teachers(false).One(x => x.UserId == teacherId);
            var resp = service.CalculateToTeacher(teacher.ConvertionIndex, teacher.Rank.EarningMultiplier);
            return new TeacherEarningDto
            {
                TotalEarningMultiplier = resp.TotalEarningMultiplier,
                EarningMultiplierByRank = resp.EarningMultiplierByRank,
                EarningMultiplierByConvertion = resp.EarningMultiplierByConvertion
            };
        }
    }
}
