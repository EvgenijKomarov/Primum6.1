using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentServiceConnection.Exceptions
{
    public class EquiringException: Exception
    {
        public EquiringException() { }

        public EquiringException(string message) : base(message) { }

        public EquiringException(string message, Exception innerException) : base(message, innerException) { }
    }
}
