using System;
using System.Net;

namespace eLib.Exceptions
{
    /// <summary>
    /// CoolException
    /// </summary>
    public class CoolException : Exception
    {

        /// <summary>
        /// Exception Concernant des donnees invalides
        /// </summary>
        public CoolException() { }


        /// <summary>
        /// Exception Concernant des donnees invalides
        /// </summary>
        /// <param name="message"></param>
        public CoolException(string message) : base(message) { }


        /// <summary>
        /// Exception Concernant des donnees invalides
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public CoolException(string message, Exception inner) : base(message, inner) { }


        public static CoolException NotFound(Type obj) => new CoolException(obj.Name + " Not Found");
    }

    public class LicenceException : CoolException
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

    public class RemoteException : CoolException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public override string Message { get; }

        public RemoteException() { }
       
        public RemoteException(string message) : base(message)
        {
        }

        public RemoteException(string message, Exception inner) : base(message, inner) { }

        public RemoteException(HttpStatusCode statusCode, string message = default(string), string reasonPhrase = default(string)) : base(reasonPhrase)
        {
            if (!string.IsNullOrEmpty(message))
                Message = message;

            StatusCode   = statusCode;
            ReasonPhrase = reasonPhrase;
        }
    }
}
