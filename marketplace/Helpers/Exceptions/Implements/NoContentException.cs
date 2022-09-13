using System.Net;

namespace marketplace.Helpers.Exceptions.Implements
{
    public class NoContentException : BaseException
    {
        public NoContentException(string message, HttpStatusCode statusCode = HttpStatusCode.NoContent) : base(message, statusCode, ResponseMessages.API_ERROR_NOT_CONTENT_LOGGER)
        {
        }
    }
}
