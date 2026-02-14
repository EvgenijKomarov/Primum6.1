using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class PromocodeIterator(IPrimumContext context)
    {
        private AdminProfileHelper helper = new AdminProfileHelper(context);

        public async Task<IEnumerable<PromocodeDto>> GetPromocodes(bool OnlyAvailable)
        {
            return await context.Set<Promocode>()
                .Include(x => x.Student)
                .WhereIf(OnlyAvailable, AvailabilityExpressions.IsPromocodeAvailable)
                .Select(x => new PromocodeDto
                {
                    PromocodeId = x.PromocodeId,
                    StudentId = x.StudentId,
                    Code = null,
                    CoinsPrice = x.CoinsPrice,
                    Title = x.Title,
                    Description = x.Description,
                    IsAvailable = AvailabilityExpressions.IsPromocodeAvailable.Compile()(x)
                })
                .ToArrayAsync();
        }

        public async Task<PromocodeDto> GetPromocode(int promocodeId, bool onlyAvailable)
        {
            var code = (await GetPromocodes(onlyAvailable)).FirstOrDefault(x => x.PromocodeId == promocodeId);
            if (code is null) { throw new NotFoundException("Promocode"); }

            return code;
        }

        public async Task<PromocodeDto> BuyPromocode(int studentId, int promocodeId)
        {
            var code = context.Set<Promocode>()
                .Include(x => x.Student)
                .Where(AvailabilityExpressions.IsPromocodeAvailable)
                .FirstOrDefault(x => x.PromocodeId == promocodeId);
            if (code is null) { throw new NotFoundException("Promocode"); }

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
            var student = await context.Set<User>()
                .Include(x => x.StudentProfile)
                .ThenInclude(x => x.Promocodes)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (student is null || student.StudentProfile is null) { throw new NotFoundException("Student"); }

            return student.StudentProfile.Promocodes
                .Select(x => new PromocodeDto
                {
                    PromocodeId = x.PromocodeId,
                    StudentId = x.StudentId,
                    Code = x.Code,
                    CoinsPrice = x.CoinsPrice,
                    Title = x.Title,
                    Description = x.Description,
                    IsAvailable = AvailabilityExpressions.IsPromocodeAvailable.Compile()(x)
                })
                .ToArray();
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

            var code = await context.Set<Promocode>()
                .Include(x => x.Student)
                .FirstOrDefaultAsync(x => x.PromocodeId == promocodeId);
            if (code is null) { throw new NotFoundException("Promocode"); }
            if (!AvailabilityExpressions.IsPromocodeAvailable.Compile()(code)) 
                { throw new BusinessLogicException("Promocode was sold"); }

            context.Set<Promocode>().Remove(code);
            await context.SaveChangesAsync();

            return promocodeId;
        }
    }
}
