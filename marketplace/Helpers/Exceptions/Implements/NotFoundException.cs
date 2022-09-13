using System.Net;

namespace marketplace.Helpers.Exceptions.Implements
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound, ResponseMessages.API_ERROR_NOT_FOUND_LOGGER)
        {
        }
    }
}
