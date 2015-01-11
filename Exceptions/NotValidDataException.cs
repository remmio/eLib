using System;



namespace CLib.Exceptions
{
    /// <summary>
    /// Exception Concernant des donnees invalides
    /// </summary>
    public class NotValidDataException : Exception
    {

        /// <summary>
        /// Exception Concernant des donnees invalides
        /// </summary>
        public NotValidDataException() { }
       

        /// <summary>
        /// Exception Concernant des donnees invalides
        /// </summary>
        /// <param name="message"></param>
        public NotValidDataException(string message) : base(message) { }


        /// <summary>
        /// Exception Concernant des donnees invalides
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public NotValidDataException(string message, Exception inner) : base(message, inner) { }

        
    }
}
