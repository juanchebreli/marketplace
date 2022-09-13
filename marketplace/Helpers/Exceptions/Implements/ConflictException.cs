using System.Net;

namespace marketplace.Helpers.Exceptions.Implements
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message) : base(message, HttpStatusCode.Conflict, ResponseMessages.API_ERROR_CONFLICT_LOGGER)
        {
        }
    }
}
