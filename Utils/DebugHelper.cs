using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace eLib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class DebugHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static Logger Logger { get; } = new Logger();

        public static void SaveLog(string filepath)
        {
            Logger.SaveLog(filepath);
        }

        public static async void SaveLog()
        {
            await Task.Run(() =>
            {
                try
                {
                    var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

                    var directoryName = Path.Combine(systemPath, "eSchool");
                    var debugFile = Path.Combine(directoryName, "Debug.txt");

                    SaveLog(debugFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public static void WriteLine(string message = default(string))
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
        public static void Log(string exception, string message = "Exception")
        {
            if (Logger == null)
                Debug.WriteLine(exception);
            else
                Logger.WriteException(exception, message);
        }

        public static void Log(this Exception exception, bool save = false)
        {
            Log(exception.ToString());
            if (save)
                SaveLog();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Log(Exception exception, string message = "Exception")
        {
            Log(exception.ToString(), message);
        }
    }
}