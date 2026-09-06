using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentServiceConnection.Exceptions
{
    public class PaymentServiceException : Exception
    {
        public PaymentServiceException() { }

        public PaymentServiceException(string message) : base(message) { }

        public PaymentServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
