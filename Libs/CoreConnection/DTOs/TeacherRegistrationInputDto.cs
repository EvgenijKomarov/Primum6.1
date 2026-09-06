using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs
{
    public class TeacherRegistrationInputDto
    {
        public string About { get; set; } = null!;

        public string INN { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;

        public string BankBIC { get; set; } = null!;
    }
}
