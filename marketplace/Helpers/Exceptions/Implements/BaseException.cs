using System.Net;

namespace marketplace.Helpers.Exceptions.Implements
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string MessageLog { get; }
        public BaseException(string message, HttpStatusCode statusCode, string messageLog) : base(message)
        {
            StatusCode = statusCode;
            MessageLog = messageLog;
        }
    }
}
