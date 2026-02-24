using CoreDBModel.Models.Enums;
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

        public IncidentMeaning Meaning { get; set; } = IncidentMeaning.Unknown;

        public IncidentDecision Decision { get; set; }

        public string DecisionExplanation { get; set; }
    }
}
