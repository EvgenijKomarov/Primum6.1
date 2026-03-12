using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
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
        private IQueryable<Promocode> Themes(bool isOnlyAvailable, Expression<Func<Promocode, bool>>? predicate) => context
            .Set<Promocode>()
            .WhereIf(predicate is not null, predicate!)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsPromocodeAvailable)
            .Include(x => x.Student)
            .ThenInclude(x => x.User);

        private IQueryable<PromocodeDto> ToDto(IQueryable<Promocode> queryable) => queryable
            .Select(x => new PromocodeDto
            {
                PromocodeId = x.PromocodeId,
                StudentId = x.Student!.StudentId,
                Code = null,
                CoinsPrice = x.CoinsPrice,
                Title = x.Title,
                Description = x.Description,
                IsAvailable = AvailabilityExpressions.IsPromocodeAvailable.Compile()(x)
            });

        public async Task<IEnumerable<PromocodeDto>> GetPromocodes(bool onlyAvailable)
        {
            return await ToDto(
                    Themes(onlyAvailable, null)
                ).ToArrayAsync();
        }

        public async Task<PromocodeDto> GetPromocode(int promocodeId, bool onlyAvailable)
        {
            return await ToDto(
                    Themes(onlyAvailable, null)
                ).FirstOrDefaultAsync(x => x.PromocodeId == promocodeId) ?? throw new NotFoundException("Promocode");
        }

        public async Task<PromocodeDto> BuyPromocode(int studentId, int promocodeId)
        {
            var code = await Themes(true, null)
                .FirstOrDefaultAsync(x => x.PromocodeId == promocodeId) ?? throw new NotFoundException("Promocode");

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
                StudentId = code.StudentId,
                Code = code.Code,
                CoinsPrice = code.CoinsPrice,
                Title = code.Title,
                Description = code.Description,
                IsAvailable = AvailabilityExpressions.IsPromocodeAvailable.Compile()(code)
            };
        }

        public async Task<IEnumerable<PromocodeDto>> GetStudentPromocodes(int studentId)
        {
            return await ToDto(
                    Themes(false, x => x.Student != null && x.Student.UserId == studentId)
                ).ToArrayAsync();
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

            var code = await Themes(false, null)
                .FirstOrDefaultAsync(x => x.PromocodeId == promocodeId) ?? throw new NotFoundException("Promocode");
            if (code is null) { throw new NotFoundException("Promocode"); }
            if (!AvailabilityExpressions.IsPromocodeAvailable.Compile()(code)) 
                { throw new BusinessLogicException("Promocode was sold"); }

            context.Set<Promocode>().Remove(code);
            await context.SaveChangesAsync();

            return promocodeId;
        }
    }
}
