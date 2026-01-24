using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class UserDto
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required string Patronymic { get; set; }

        public required string DisplayName { get; set; }

        public required long Cash {  get; set; }
    }
}
