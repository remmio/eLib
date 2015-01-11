using System;



namespace CLib.Exceptions
{
    /// <summary>
    /// Aucune Valeur Correspondante Trouver
    /// </summary>
    public class NoValidDataFoundException : Exception
    {

        /// <summary>
        /// Aucune Valeur Correspondante Trouver
        /// </summary>
        public NoValidDataFoundException() { }


        /// <summary>
        /// Aucune Valeur Correspondante Trouver
        /// </summary>
        /// <param name="message"></param>
        public NoValidDataFoundException(string message) : base(message) { }


        /// <summary>
        /// Aucune Valeur Correspondante Trouver
        /// </summary>        
        public NoValidDataFoundException(string message, Exception inner) : base(message, inner) { }

    }
}
