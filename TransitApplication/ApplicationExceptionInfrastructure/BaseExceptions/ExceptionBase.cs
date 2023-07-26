namespace TransitApplication.BaseExceptions
{
    public abstract class ExceptionBase : Exception
    {
        public int StatusCode { get; private set; }
        public string Description { get; private set; }

        public ExceptionBase(int statusCode, string message, string description) 
            : base(message) 
        {
            StatusCode = statusCode;
            Description = description;
        }
    }
}
