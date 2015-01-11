using System;
using System.Text;



namespace CLib
{
    /// <summary>
    /// RandomHelper
    /// </summary>
    class RandomHelper
    {
        /// <summary>
        /// Random Caracteres
        /// </summary>
        /// <param name="numberOfCharsToGenerate">Nombre de Caracteres</param>
        /// <returns></returns>
        public static string GetLetters(int numberOfCharsToGenerate)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            var sb = new StringBuilder();
            for (var i = 0; i < numberOfCharsToGenerate; i++)
            {
                var num = random.Next(0, chars.Length);
                sb.Append(chars[num]);
            }
            return sb.ToString();
        }









    }
}
