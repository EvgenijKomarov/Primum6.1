using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models
{
    public class ConsultationRequest: BaseEntity
    {
        public string DisplayName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsRevisioned { get; set; }
    }
}
