using TransitApplication.BaseExceptions;
using System.Net;

namespace TransitApplication.HttpExceptions
{
    public class InternalServerErrorException : ExceptionBase
    {
        public InternalServerErrorException(string description)
            : base((int)HttpStatusCode.InternalServerError, "Internal server error", description) { }

        public InternalServerErrorException(string message, string description)
            : base((int)HttpStatusCode.InternalServerError, message, description) { }
    }
}
