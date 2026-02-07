using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class IncidentDto
    {
        public required int ObjectId {  get; set; }

        public required string CommonInfo { get; set; }

        public required IncidentStatusDto Status { get; set; }

        public required IncidentMeaningDto Meaning { get; set; }

        public required IEnumerable<IncidentDecisionDto> Decisions { get; set; }

        public required IEnumerable<IncidentLogDto>? LinkedLogs { get; set; }
    }
}
