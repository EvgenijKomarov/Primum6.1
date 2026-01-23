using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public long Cash {  get; set; }
    }
}
