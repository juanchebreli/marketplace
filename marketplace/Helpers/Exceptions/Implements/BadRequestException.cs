using System.Net;

namespace marketplace.Helpers.Exceptions.Implements
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest, ResponseMessages.API_ERROR_BAD_REQUEST_LOGGER)
        {
        }
    }
}
