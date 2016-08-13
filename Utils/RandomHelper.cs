using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using eLib.Collections;
using eLib.Properties;

namespace eLib.Utils
{
    /// <summary>
    /// RandomHelper
    /// </summary>
    public class RandomHelper
    {
        private static readonly object RandomLock = new object();
        // ReSharper disable once InconsistentNaming
        private static readonly Random random = new Random();
     
        public static int Random(int max)
        {
            lock (RandomLock)
            {
                return random.Next(max + 1);
            }
        }
        
        public static int Random(int min, int max)
        {
            lock (RandomLock)
            {
                return random.Next(min, max + 1);
            }
        }
        
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
        
        public static string GetRandNum(int nomberOfNumToGenerate)
        {
            var x = new Random();
            var idOut = string.Empty;

            for (var i = 0; i < nomberOfNumToGenerate; i++)
            {
                idOut += x.Next(0, 9);
            }
            return idOut;
        }
       
        public static string GetRandomKey(int length = 5, int count = 3, char separator = '-') 
            => Enumerable.Range(1, (length + 1) * count - 1).Aggregate("", (x, index) => x + (index % (length + 1) == 0 ? separator : GetRandomChar(FilesHelper.FilesHelper.Alphanumeric)));

        private static char GetRandomChar(string chars) => chars[Random(chars.Length - 1)];

        private static string GetRandomString(string chars, int length)
        {
            var sb = new StringBuilder();
            while (length-- > 0)
            {
                sb.Append(GetRandomChar(chars));
            }
            return sb.ToString();
        }

        public static string GetRandomNumber(int length) => GetRandomString(FilesHelper.FilesHelper.Numbers, length);

        public static string GetRandomAlphanumeric(int length) => GetRandomString(FilesHelper.FilesHelper.Alphanumeric, length);

        public static Color RandomColor()
        {
            var randomGen = new Random();
            var names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            var randomColorName = names[randomGen.Next(names.Length)];
            return Color.FromKnownColor(randomColorName);
        }

        public static string RandomColorString() => ((KnownColor[])Enum.GetValues(typeof(KnownColor)))
            .Where(c => c > KnownColor.Transparent && c != KnownColor.Black && c != KnownColor.White && c != KnownColor.WhiteSmoke && c != KnownColor.Gray && c != KnownColor.DarkGray)
            .Random().ToString();

        public static string RandomCoolColorString()
        {
           return new[]
            {
                "Teal",
                "SkyBlue",
                "YellowGreen",
                "MediumSeaGreen",
                "MediumAquaMarine",
                "LightSeaGreen",
                "SeaGreen",
                "DarkTurquoise",
                "Turquoise"
            }.Random();
        }

        //public static string RandomColorString()
        //{
        //    lock (RandomLock)
        //    {
        //        Color col;
        //        do
        //        {
        //            col = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        //        } while (!col.IsNamedColor);

        //        return col.Name;
        //    }
        //}

        public static string RandomCountry()
        {
            return CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                    .Select(culture => new RegionInfo(culture.LCID))
                        .Select(region => region.EnglishName).Random();
        }

        public static string RandomFirstName() => FilesHelper.FilesHelper.FileToStringList(Resources.first_names).Random();

        public static string RandomLastName() => FilesHelper.FilesHelper.FileToStringList(Resources.last_names).Random();

        public static IEnumerable<string> Countries()
        {
            return CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                    .Select(culture => new RegionInfo(culture.LCID))
                        .Select(region => region.EnglishName);
        }
    }
}
