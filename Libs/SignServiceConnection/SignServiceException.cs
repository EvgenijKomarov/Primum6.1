using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SignServiceConnection
{
    /// <summary>
    /// Специфичное исключение для ошибок SignService
    /// </summary>
    public class SignServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? RawContent { get; set; }

        public SignServiceException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
