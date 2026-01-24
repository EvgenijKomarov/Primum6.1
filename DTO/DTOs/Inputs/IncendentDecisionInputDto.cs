using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs.Inputs
{
    public class IncendentDecisionInputDto
    {
        public int ObjectId { get; set; }

        public IncendentMeaningDto Meaning { get; set; } = IncendentMeaningDto.Unknown;

        public IncendentDecisionDto Decision { get; set; }
    }
}
