using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Inputs
{
    public class LessonWorkoffInputDto
    {
        public required DateTime DateTime { get; set; }

        public required int AbonementId { get; set; }
    }
}
