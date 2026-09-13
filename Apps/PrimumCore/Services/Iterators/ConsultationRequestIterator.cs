using CoreConnection.DTOs.Inputs;
using CoreDBModel.Models;

namespace PrimumCore.Services.Iterators
{
    public class ConsultationRequestIterator(DatabaseIterator dbIterator)
    {
        public async Task<int> CreateConsultationRequest(ConsultationRequestInput input)
        {
            var request = new ConsultationRequest()
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                IsRevisioned = false
            };

            await dbIterator.AddAsync(request);
            await dbIterator.SaveChangesAsync();

            return request.Id;
        }
    }
}
