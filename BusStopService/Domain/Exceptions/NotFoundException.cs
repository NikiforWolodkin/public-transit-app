using System.Net;

namespace Domain.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string description)
            : base((int)HttpStatusCode.NotFound, "Resource not found", description) { }

        public NotFoundException(string message, string description)
            : base((int)HttpStatusCode.NotFound, message, description) { }
    }
}
