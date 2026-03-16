using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Abstractions
{
    public interface IHasId : IOrderable
    {
        int Id { get; }
    }
}
