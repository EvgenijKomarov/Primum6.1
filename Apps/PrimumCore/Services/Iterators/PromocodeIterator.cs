using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class PromocodeIterator(PrimumContext context, AdminProfileHelper helper)
    {
        private IQueryable<Promocode> Promocodes(bool isOnlyAvailable, Expression<Func<Promocode, bool>>? predicate) => context
            .Set<Promocode>()
            .WhereIf(predicate is not null, predicate!)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsPromocodeAvailable)
            .Include(x => x.Student)
            .ThenInclude(x => x.User);

        public async Task<PageResult<PromocodeDto>> GetPromocodes(bool onlyAvailable, int _page, int _pageSize)
        {
            return await Promocodes(onlyAvailable, null).ToDto(true).ToPageResult(_page, _pageSize);
        }

        public async Task<PromocodeDto> GetPromocode(int promocodeId, bool onlyAvailable)
        {
            return await Promocodes(onlyAvailable, null).ToDto(true).One(x => x.PromocodeId == promocodeId);
        }

        public async Task<PromocodeDto> BuyPromocode(int studentId, int promocodeId)
        {
            var code = await Promocodes(true, null)
                .One(x => x.PromocodeId == promocodeId);

            var student = await context.Set<User>()
                .Include(x => x.StudentProfile)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (student is null || student.StudentProfile is null) { throw new NotFoundException("Student"); }
            if (student.StudentProfile.Coins < code.CoinsPrice) { throw new BusinessLogicException("Not enough coins"); }

            student.StudentProfile.Coins -= code.CoinsPrice;
            code.Student = student.StudentProfile;
            await context.SaveChangesAsync();

            return new PromocodeDto
            {
                PromocodeId = code.PromocodeId,
                StudentId = code.Student.UserId,
                Code = code.Code,
                CoinsPrice = code.CoinsPrice,
                Title = code.Title,
                Description = code.Description,
                IsAvailable = AvailabilityExpressions.IsPromocodeAvailable.Compile()(code)
            };
        }

        public async Task<PageResult<PromocodeDto>> GetStudentPromocodes(int studentId, int _page, int _pageSize)
        {
            return await Promocodes(false, x => x.Student != null && x.Student.UserId == studentId).ToDto(true).ToPageResult(_page, _pageSize);
        }

        public async Task<int> AddPromocode(int adminId, PromocodeInputDto dto)
        {
            await helper.CheckIteratingUser(adminId, Permission.AddPromocodes);

            var promocode = new Promocode
            {
                Code = dto.Code,
                CoinsPrice = dto.CoinsPrice,
                Title = dto.Title,
                Description = dto.Description
            };
            await context.Set<Promocode>().AddAsync(promocode);

            await context.SaveChangesAsync();
            return promocode.PromocodeId;
        }

        public async Task<int> DeletePromocode(int adminId, int promocodeId)
        {
            await helper.CheckIteratingUser(adminId, Permission.DeletePromocodes);

            var code = await Promocodes(false, null)
                .One(x => x.PromocodeId == promocodeId);
            if (!AvailabilityExpressions.IsPromocodeAvailable.Compile()(code)) 
                { throw new BusinessLogicException("Promocode was sold"); }

            context.Set<Promocode>().Remove(code);
            await context.SaveChangesAsync();

            return promocodeId;
        }
    }
}
