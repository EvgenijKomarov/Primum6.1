using Common.Utilities;
using CoreConnection.DTOs;
using CoreDBModel.Services;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherEarningIterator(DatabaseIterator dbIterator, EarningCalculationService service)
    {
        public async Task<TeacherEarningDto> GetTeacherEarning(int teacherId)
        {
            var teacher = await dbIterator.Teachers(false).Include(x => x.Rank).One(x => x.UserId == teacherId);
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
