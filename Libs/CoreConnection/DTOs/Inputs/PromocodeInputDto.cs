using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs.Inputs
{
    public class PromocodeInputDto
    {
        public required string Code { get; set; }

        public required int CoinsPrice { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }
    }
}
