using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.EntityFrameworkCore;
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
                .WhereIf(OnlyAvailable, x => x.IsAvailable)
                .Select(x => new PromocodeDto
                {
                    PromocodeId = x.PromocodeId,
                    StudentId = x.StudentId,
                    Code = null,
                    CoinsPrice = x.CoinsPrice,
                    Title = x.Title,
                    Description = x.Description,
                    IsAvailable = x.IsAvailable
                })
                .ToArrayAsync();
        }

        public async Task<PromocodeDto> GetPromocode(int promocodeId, bool OnlyAvailable)
        {
            var code = await context.Set<Promocode>()
                .Include(x => x.Student)
                .WhereIf(OnlyAvailable, x => x.IsAvailable)
                .Select(x => new PromocodeDto
                {
                    PromocodeId = x.PromocodeId,
                    StudentId = x.StudentId,
                    Code = null,
                    CoinsPrice = x.CoinsPrice,
                    Title = x.Title,
                    Description = x.Description,
                    IsAvailable = x.IsAvailable
                })
                .FirstOrDefaultAsync(x => x.PromocodeId == promocodeId);
            if (code is null) { throw new Exception("Promocode not found"); }

            return code;
        }

        public async Task<PromocodeDto> BuyPromocode(int studentId, int promocodeId)
        {
            var code = context.Set<Promocode>()
                .Include(x => x.Student)
                .Where(x => x.IsAvailable)
                .FirstOrDefault(x => x.PromocodeId == promocodeId);
            if (code is null) { throw new Exception("Promocode not found"); }

            var student = await context.Set<User>()
                .Include(x => x.StudentProfile)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (student is null || student.StudentProfile is null) { throw new Exception("Student not found"); }
            if (student.StudentProfile.Coins < code.CoinsPrice) { throw new Exception("Not enough coins"); }

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
                IsAvailable = code.IsAvailable
            };
        }

        public async Task<IEnumerable<PromocodeDto>> GetStudentPromocodes(int studentId)
        {
            var student = await context.Set<User>()
                .Include(x => x.StudentProfile)
                .ThenInclude(x => x.Promocodes)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (student is null || student.StudentProfile is null) { throw new Exception("Student not found"); }

            return student.StudentProfile.Promocodes
                .Select(x => new PromocodeDto
                {
                    PromocodeId = x.PromocodeId,
                    StudentId = x.StudentId,
                    Code = null,
                    CoinsPrice = x.CoinsPrice,
                    Title = x.Title,
                    Description = x.Description,
                    IsAvailable = x.IsAvailable
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
            if (code is null) { throw new Exception("Promocode not found"); }
            if (!code.IsAvailable) { throw new Exception("Promocode was sold"); }

            context.Set<Promocode>().Remove(code);
            await context.SaveChangesAsync();

            return promocodeId;
        }
    }
}
