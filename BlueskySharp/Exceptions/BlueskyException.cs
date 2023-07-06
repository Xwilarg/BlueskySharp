using System.Net;

namespace BlueskySharp.Exceptions
{
    public class BlueskyException : Exception
    {
        /// <summary>
        /// The HTTP status code of the failed request
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public BlueskyException()
        {
        }

        public BlueskyException(string message)
            : base(message)
        {
        }

        public BlueskyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public BlueskyException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
