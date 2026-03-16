using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Abstractions
{
    public interface IHasRating : IOrderable
    {
        float? Rating { get; }
    }
}
