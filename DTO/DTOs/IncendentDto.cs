using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class IncendentDto
    {
        public required int ObjectId {  get; set; }

        public required string CommonInfo { get; set; }

        public required IncendentStatusDto Status { get; set; }

        public required IncendentMeaningDto Meaning { get; set; }

        public required IEnumerable<IncendentDecisionDto> Decisions { get; set; }
    }
}
