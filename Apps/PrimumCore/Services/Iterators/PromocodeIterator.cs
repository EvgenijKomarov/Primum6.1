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

namespace PrimumCore.Services.Iterators
{
    public class PromocodeIterator(DatabaseIterator dbIterator, AdminProfileHelper helper)
    {
        public async Task<PageResult<PromocodeDto>> GetPromocodes(bool onlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Promocodes(onlyAvailable).ToDto(true).ToPageResult(_page, _pageSize);
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

            student.Coins -= code.CoinsPrice;
            code.Student = student;
            await dbIterator.SaveChangesAsync();

            return new PromocodeDto
            {
                Id = code.Id,
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
            return await dbIterator.Promocodes(false)
                .Where(x => x.Student != null && x.Student.UserId == studentId)
                .ToDto(true)
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
