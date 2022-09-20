using System.Net;

namespace marketplace.Helpers.Exceptions.Implements
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized, ResponseMessages.API_ERROR_BAD_REQUEST_LOGGER)
        {
        }
    }
}
