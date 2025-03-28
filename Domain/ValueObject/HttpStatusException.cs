
namespace Domain.ValueObject
{
    public class HttpStatusException : Exception
    {
        public int StatusCode { get; }

        public HttpStatusException(string message, int statusCode = 501) : base(message)
        {
            StatusCode = statusCode;
        }

    }
}
