using System;
using System.Net;

namespace eLib.Exceptions
{
    /// <summary>
    /// CoolException
    /// </summary>
    public class UxException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public override string Message { get; }


        public UxException()
        {
            StatusCode = HttpStatusCode.BadRequest;
        }

        public UxException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = message;
        }

        public UxException(string message, Exception inner) : base(message, inner)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = message;
        }

        public UxException(HttpStatusCode statusCode, string message = default(string), string reasonPhrase = default(string)) : base(reasonPhrase)
        {
            if (!string.IsNullOrEmpty(message))
                Message = message;

            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public static UxException NotFound(Type obj) => new UxException(obj.Name + " Not Found");
    }

    public class LicenceException : UxException
    {
        public Type Type { get; set; }
        public int Limit { get; set; }

        public LicenceException() { }

        public LicenceException(Type type, int typeLimit) : base(type.ToString())
        {
            Type = type;
            Limit = typeLimit;
        }

        public LicenceException(string message) : base(message)
        {
        }

        public LicenceException(string message, Exception inner) : base(message, inner) { }
    }

 
}
