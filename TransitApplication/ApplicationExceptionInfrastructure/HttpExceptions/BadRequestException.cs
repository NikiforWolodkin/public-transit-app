using TransitApplication.BaseExceptions;
using System.Net;

namespace TransitApplication.HttpExceptions
{
    public class BadRequestException : ExceptionBase
    {
        public BadRequestException(string description)
            : base((int)HttpStatusCode.BadRequest, "Bad request", description) { }

        public BadRequestException(string message, string description)
            : base((int)HttpStatusCode.BadRequest, message, description) { }
    }
}
