using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using eLib.Properties;

namespace eLib.Exceptions
{
        public class Operation<TResult>
        {
            private Operation()
            {
            }
            
            public bool Success { get; private set; }
            public TResult Result { get; private set; }
            public string Message { get; private set; }
            public Exception Exception { get; private set; }
           
            public static Operation<TResult> Succes(TResult result) => new Operation<TResult> { Success = true, Result = result };
            public static Operation<TResult> Succes(TResult result, string message) => new Operation<TResult> { Success = true, Result = result, Message = message};

            public static Operation<TResult> Failed() => new Operation<TResult> { Success = false};
            public static Operation<TResult> Failed(string nonSuccessMessage) => new Operation<TResult> { Success = false, Message = nonSuccessMessage };
            public static Operation<TResult> Failed(TResult result, string message) => new Operation<TResult> { Success = false, Result =  result, Message = message};
            public static Operation<TResult> Failed(Exception ex)
            {
                return new Operation<TResult>
                {
                    Success = false,
                    Message = string.Format("{0}{1}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace),
                    Exception = ex
                };
            }
        }

    public class Operation :IEqualityComparer<Operation>
    {
        private Operation()
        {
        }

        public bool Success { get;  set; }
        public string Message { get;  set; }
        public Exception Exception { get;  set; }

        public static Operation Succes() => new Operation { Success = true };
        public static Operation Succes(string message) => new Operation { Success = true, Message = message };

        public static Operation Failed() => new Operation { Success = false};
        public static Operation Failed(string nonSuccessMessage) => new Operation { Success = false, Message = nonSuccessMessage };
        public static Operation Failed(Exception ex)
        {
            return new Operation
            {
                Success = false,
                Message = string.Format("{0}{1}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace),
                Exception = ex
            };
        }

        public static Operation Canceled(string message) => new Operation { Success = false, Message = message, Exception = new OperationCanceledException(message)};
        public static Operation Canceled() => new Operation { Success = false, Message = "Operation Canceled", Exception = new OperationCanceledException("Operation Canceled") };
            
        public static Operation AccesDenied(string message = default (string))
        {
            throw new UxException(HttpStatusCode.Forbidden, Resources.AccesDenied_You_are_not_authorized_to_do_this_operation_, "Forbidden");
        }

        public static Operation Denied(string message = default(string))
        {
            return new Operation
            {
                Success = false,
                Message = message,
                Exception = new UxException(HttpStatusCode.Forbidden, Resources.AccesDenied_You_are_not_authorized_to_do_this_operation_, "Forbidden")
            };
        }


        public bool Equals(Operation x, Operation y) => x.Success ==y.Success;

        public int GetHashCode(Operation obj) => obj.Success.GetHashCode();
    }


    public static class Extension
    {
        public static Operation AsOperation(this bool result) => result ? Operation.Succes() : Operation.Failed();

        public static void EnsureSucces(this Operation operation, string nonSuccesMessage)
        {
            if (!operation.Success)
                throw new UxException(nonSuccesMessage);
        }       

        public static string AsMessage(this Exception exception)
        {
            exception = exception.MostInner();

            if (exception is HttpResponseException)
            {
                switch (((HttpResponseException)exception).Response.StatusCode)
                {
                    case HttpStatusCode.GatewayTimeout:
                        return "The connection timed out";
                    case HttpStatusCode.InternalServerError:
                        return "Oops! Sorry! Something went wrong";
                    case HttpStatusCode.RequestTimeout:
                        return "The connection timed out";
                    case HttpStatusCode.Unauthorized:
                        return "You must log in to access the requested resource.";
                    case HttpStatusCode.Forbidden:
                        return "You are not authorized to access this page.";
                    case HttpStatusCode.NotFound:
                        return "The requested resource is not found, please update the program";
                }
                return ((HttpResponseException) exception).Response.ReasonPhrase;
            }
            if (exception is UxException)
            {
                switch (((UxException)exception).StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return ((UxException)exception).Message;
                    case HttpStatusCode.GatewayTimeout:
                        return "The connection timed out";
                    case HttpStatusCode.InternalServerError:
                        return "Oops! Sorry! Something went wrong";
                    case HttpStatusCode.RequestTimeout:
                        return "The connection timed out";
                    case HttpStatusCode.Unauthorized:
                        return "You must log in to access the requested resource";
                    case HttpStatusCode.Forbidden:
                        return "You are not authorized to access this page.";
                    case HttpStatusCode.NotFound:
                        return "The requested resource is not found, please update the program";
                }
                return ((UxException)exception).ReasonPhrase;
            }

            return exception.MostInner().Message;
        }

        public static Exception MostInner(this Exception exception)
        {
            if (exception == null)
                return null;

            while (exception.InnerException != null) exception = exception.InnerException;
            return exception;
        }
    }
}
