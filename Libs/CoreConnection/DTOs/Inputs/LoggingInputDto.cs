using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Inputs
{
    public class LoggingInputDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
