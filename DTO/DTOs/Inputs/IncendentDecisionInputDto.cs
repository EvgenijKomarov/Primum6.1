using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs.Inputs
{
    public class IncidentDecisionInputDto
    {
        public int ObjectId { get; set; }

        public IncidentMeaningDto Meaning { get; set; } = IncidentMeaningDto.Unknown;

        public IncidentDecisionDto Decision { get; set; }

        public string DecisionExplanation { get; set; }
    }
}
