using System.Net;

namespace Domain.Exceptions
{
    public class InternalServerErrorException : ExceptionBase
    {
        public InternalServerErrorException(string description)
            : base((int)HttpStatusCode.InternalServerError, "Internal server error", description) { }

        public InternalServerErrorException(string message, string description)
            : base((int)HttpStatusCode.InternalServerError, message, description) { }
    }
}
