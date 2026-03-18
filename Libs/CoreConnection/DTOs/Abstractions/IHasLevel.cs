using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Abstractions
{
    public interface IHasLevel: IOrderable
    {
        int Level { get; }

        string Rank { get; }
    }
}
