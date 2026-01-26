using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class PromocodeDto
    {
        public required int PromocodeId { get; set; }

        public required int? StudentId { get; set; }

        public required string? Code { get; set; }

        public required int CoinsPrice { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required bool IsAvailable { get; set; }
    }
}
