using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace CLib
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class EnumsHelper
    {

        /// <summary>
        /// Trouver un Enum a Partir de son string : Color colorEnum = "Red".ToEnum<Color/>();
        /// </summary>
        /// <param name="enumString"> Son String</param>
        /// <typeparam name="T">Type Enum</typeparam>
        /// <returns></returns>
        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }


        /// <summary>
        /// Gets the description of a specific enum value.
        /// </summary>
        public static string GetEnumDescription(this Enum eValue)
        {
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (nAttributes.Any()) return ((DescriptionAttribute) nAttributes.First()).Description;

            // If no description is found, best guess is to generate it by replacing underscores with spaces
            // and title case it. You can change this to however you want to handle enums with no descriptions.

            var oTi = CultureInfo.CurrentCulture.TextInfo;
            return oTi.ToTitleCase(oTi.ToLower(eValue.ToString().Replace("_", " ")));
        }


        /// <summary>
        /// Returns an enumerable collection of all values and descriptions for an enum type.
        /// </summary>        
        public static IEnumerable<KeyValuePair<string, Enum>> GetAllValuesAndDescriptions<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an Enumeration type");
           
            return from e in Enum.GetValues(typeof(TEnum)).Cast<Enum>() select new KeyValuePair<string, Enum>(e.GetEnumDescription(), e);
        }

    }

}





