using System;
using System.Linq;
using System.Text;

namespace CLib
{
    /// <summary>
    /// RandomHelper
    /// </summary>
    public class RandomHelper
    {
        private static readonly object RandomLock = new object();
        // ReSharper disable once InconsistentNaming
        private static readonly Random random = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Random(int max)
        {
            lock (RandomLock)
            {
                return random.Next(max + 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Random(int min, int max)
        {
            lock (RandomLock)
            {
                return random.Next(min, max + 1);
            }
        }

        /// <summary>
        /// Random Caracteres
        /// </summary>
        /// <param name="numberOfCharsToGenerate">Nombre de Caracteres</param>
        /// <returns></returns>
        public static string GetRandLetters(int numberOfCharsToGenerate)
        {
            var rand = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            var sb = new StringBuilder();
            for (var i = 0; i < numberOfCharsToGenerate; i++)
            {
                var num = rand.Next(0, chars.Length);
                sb.Append(chars[num]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Random Numbers
        /// </summary>
        /// <param name="nomberOfNumToGenerate"></param>
        /// <returns></returns>
        public static string GetRandNum(int nomberOfNumToGenerate)
        {
            var x = new Random();
            var idOut = string.Empty;

            for (var i = 0; i < nomberOfNumToGenerate; i++)
            {
                idOut = idOut + x.Next(0, 9);
            }
            return idOut;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="count"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetRandomKey(int length = 5, int count = 3, char separator = '-')
        {
            return Enumerable.Range(1, (length + 1) * count - 1).Aggregate("", (x, index) => x + (index % (length + 1) == 0 ? separator : GetRandomChar(FilesHelper.FilesHelper.Alphanumeric)));
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        private static char GetRandomChar(string chars)
        {
            return chars[Random(chars.Length - 1)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GetRandomString(string chars, int length)
        {
            var sb = new StringBuilder();
            while (length-- > 0)
            {
                sb.Append(GetRandomChar(chars));
            }
            return sb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GetRandomNumber(int length)
        {
            return GetRandomString(FilesHelper.FilesHelper.Numbers, length);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GetRandomAlphanumeric(int length)
        {
            return GetRandomString(FilesHelper.FilesHelper.Alphanumeric, length);
        }
    }
}
