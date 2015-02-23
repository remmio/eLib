using System;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace CLib
{
    /// <summary>
    /// Logger des Erreurs
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public delegate void MessageAddedEventHandler(string message);

        /// <summary>
        /// 
        /// </summary>
        public event MessageAddedEventHandler MessageAdded;

        /// <summary>
        /// 
        /// </summary>
        private readonly object _loggerLock = new object();
        private readonly StringBuilder _sbMessages = new StringBuilder(4096);
        private int _lastSaveIndex;

        /// <summary>
        /// 
        /// </summary>
        protected void OnMessageAdded(string message)
        {
            MessageAdded?.Invoke(message);
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteLine(string message = "")
        {
            lock (_loggerLock)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    message = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} - {1}", DateTime.Now, message);
                }

                _sbMessages.AppendLine(message);                
                Debug.WriteLine(message);
                OnMessageAdded(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteException(string exception, string message = "Exception")
        {
            WriteLine("{0}:{1}{2}", message, Environment.NewLine, exception);
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteException(Exception exception, string message = "Exception")
        {
            while (exception != null)
            {
                WriteException(exception.ToString(), message);
                exception = exception.InnerException;
            }
        }

        /// <summary>
        /// Append new debug logs to debughelper file
        /// </summary>
        public void SaveLog(string filepath)
        {
            lock (_loggerLock)
            {
                if (_sbMessages == null || _sbMessages.Length <= 0 || string.IsNullOrEmpty(filepath)) return;
                var messages = _sbMessages.ToString(_lastSaveIndex, _sbMessages.Length - _lastSaveIndex);

                if (string.IsNullOrEmpty(messages)) return;

                FilesHelper.FilesHelper.CreateDirectoryIfNotExist(filepath);
                File.AppendAllText(filepath, messages, Encoding.UTF8);
                _lastSaveIndex = _sbMessages.Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            lock (_loggerLock)
            {
                _sbMessages.Length = 0;
                _lastSaveIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            lock (_loggerLock)
            {
                if (_sbMessages != null && _sbMessages.Length > 0) return _sbMessages.ToString();
                             
                return string.Empty;
            }
        }
    }
}