using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Inputs
{
    public class ConsultationRequestInput
    {
        public string DisplayName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
