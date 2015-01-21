using System;
using System.Diagnostics;

namespace CLib
{
    /// <summary>
    /// 
    /// </summary>
    public static class DebugHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static Logger Logger { get; } = new Logger();

        static DebugHelper()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static void WriteLine(string message = "")
        {
            if (Logger == null)
                Debug.WriteLine(message);
            else
                Logger.WriteLine(message);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        /// <summary>
        /// 
        /// </summary>
        public static void WriteException(string exception, string message = "Exception")
        {
            if (Logger == null)
                Debug.WriteLine(exception);
            else
                Logger.WriteException(exception, message);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void WriteException(Exception exception, string message = "Exception")
        {
            WriteException(exception.ToString(), message);
        }
    }
}