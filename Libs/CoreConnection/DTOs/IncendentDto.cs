using CoreDBModel.Models.Enums;
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

        public required IncidentStatus Status { get; set; }

        public required IncidentMeaning Meaning { get; set; }

        public required Permission PermissionBy { get; set; }

        public required IEnumerable<IncidentDecision> Decisions { get; set; }

        public required IEnumerable<IncidentLogDto> LinkedLogs { get; set; }
    }
}
