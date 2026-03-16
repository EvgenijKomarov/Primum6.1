using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class IncidentLogDto : IHasId
    {
        public required int Id { get; set; }

        public required int AdminUserId { get; set; }

        public required DateTime DateTime { get; set; }

        public required string AdminDisplayName { get; set; }

        public required string Description { get; set; }
    }
}
