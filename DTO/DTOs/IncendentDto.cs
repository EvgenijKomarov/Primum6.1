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
        public int ObjectId {  get; set; }

        public string CommonInfo { get; set; } = null!;

        public IncendentStatusDto Status { get; set; } = IncendentStatusDto.Unknown;

        public IncendentMeaningDto Meaning { get; set; } = IncendentMeaningDto.Unknown;

        public IEnumerable<IncendentDecisionDto> Decisions { get; set; } = new List<IncendentDecisionDto>();
    }
}
