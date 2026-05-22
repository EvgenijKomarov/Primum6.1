using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class UserDto : IHasId
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required string Patronymic { get; set; }

        public required string DisplayName { get; set; }

        public required string Email { get; set; }

        public required bool IsBanned { get; set; }

        public required bool MailConfirmed { get; set; }

        public required bool? IsApprovedStudent { get; set; }

        public required bool? IsApprovedTeacher { get; set; }

        public required bool? IsAdmin {get; set; }

        public required bool IsAvailable { get; set; }
    }
}
