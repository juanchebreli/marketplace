using System.Net;

namespace marketplace.Helpers.Exceptions.Implements
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(message, HttpStatusCode.InternalServerError, ResponseMessages.API_ERROR_NOT_FOUND_LOGGER)
        {
        }
    }
}
