using System;
using System.Text;



namespace CLib
{
    /// <summary>
    /// RandomHelper
    /// </summary>
    public class RandomHelper
    {

        /// <summary>
        /// Random Caracteres
        /// </summary>
        /// <param name="numberOfCharsToGenerate">Nombre de Caracteres</param>
        /// <returns></returns>
        public static string GetRandLetters(int numberOfCharsToGenerate)
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
                idOut = idOut + x.Next(1, 9);
            }
            return idOut;
        }







    }
}
