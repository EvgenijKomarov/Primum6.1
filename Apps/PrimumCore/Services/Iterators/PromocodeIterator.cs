using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using PrimumCore.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using System.Linq.Expressions;
using CoreDBModel.Services;
using CoreDBModel.Extensions;

namespace PrimumCore.Services.Iterators
{
    public class PromocodeIterator(DatabaseIterator dbIterator, AdminProfileHelper helper)
    {
        public async Task<PageResult<PromocodeDto>> GetPromocodes(bool onlyAvailable, string? searchString, int _page, int _pageSize)
        {
            return await dbIterator
                .Promocodes(onlyAvailable)
                .WhereIf(!string.IsNullOrEmpty(searchString), e => EF.Functions.Like(
                    (
                         (e.Title ?? "") + " " +
                         (e.Description ?? "") + " "+
                         e.Id).ToLower(),
                    $"%{searchString.ToLower()}%"))
                .ToDto(true)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<PromocodeDto> GetPromocode(int promocodeId, bool onlyAvailable)
        {
            return await dbIterator.Promocodes(onlyAvailable).ToDto(true).One(x => x.Id == promocodeId);
        }

        public async Task<PromocodeDto> BuyPromocode(int studentId, int promocodeId)
        {
            var code = await dbIterator.Promocodes(true)
                .One(x => x.Id == promocodeId);

            var student = await dbIterator.Students()
                .One(x => x.User.Id == studentId);
            if (student.Coins < code.CoinsPrice) { throw new BusinessLogicException("Not enough coins"); }

            // Проверки выше дают быстрый ответ, но при одновременных покупках решают условные UPDATE:
            // код закрепляется, только если он ещё свободен, монеты списываются, только если их хватает
            await dbIterator.InTransactionAsync(async () =>
            {
                var claimed = await dbIterator.Promocodes(true)
                    .Where(x => x.Id == promocodeId)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.StudentId, student.Id));
                if (claimed == 0) { throw new BusinessLogicException("Promocode already bought"); }

                var charged = await dbIterator.Students()
                    .Where(x => x.Id == student.Id && x.Coins >= code.CoinsPrice)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.Coins, x => x.Coins - code.CoinsPrice));
                if (charged == 0) { throw new BusinessLogicException("Not enough coins"); }

                return true;
            });

            return new PromocodeDto
            {
                Id = code.Id,
                StudentId = studentId,
                Code = code.Code,
                CoinsPrice = code.CoinsPrice,
                Title = code.Title,
                Description = code.Description,
                IsAvailable = false
            };
        }

        public async Task<PageResult<PromocodeDto>> GetStudentPromocodes(int studentId, int _page, int _pageSize)
        {
            return await dbIterator.Promocodes(false)
                .Include(x => x.Student)
                .Where(x => x.Student != null && x.Student.UserId == studentId)
                .ToDto(false)
                .ToPageResult(_page, _pageSize);
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
            await dbIterator.AddAsync(promocode);

            await dbIterator.SaveChangesAsync();
            return promocode.Id;
        }

        public async Task<int> DeletePromocode(int adminId, int promocodeId)
        {
            await helper.CheckIteratingUser(adminId, Permission.DeletePromocodes);

            var code = await dbIterator.Promocodes(false)
                .One(x => x.Id == promocodeId);
            if (!AvailabilityExpressions.IsPromocodeAvailable.Compile()(code)) 
                { throw new BusinessLogicException("Promocode was sold"); }

            await dbIterator.RemoveAsync(code);
            await dbIterator.SaveChangesAsync();

            return promocodeId;
        }
    }
}
