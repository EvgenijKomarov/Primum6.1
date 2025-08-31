using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Patronymic { get; set; }

        public bool? IsApprovedStudent { get; set; }
        public bool? IsApprovedTeacher { get; set; }
        public bool? IsApprovedAdmin { get; set; }
    }
}
