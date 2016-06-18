using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using eLib.Attributes;

namespace eLib.Utils
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
        public static T ToEnum<T>(this string enumString) => (T)Enum.Parse(typeof(T), enumString);

        /// <summary>
        /// Gets the description of a specific enum value.
        /// </summary>
        public static string Resume(this Enum eValue)
        {
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(ResumeAttribute), false);

            if (nAttributes.Any()) return ((ResumeAttribute)nAttributes.First()).Description;
                
            var oTi = CultureInfo.CurrentCulture.TextInfo;
            return oTi.ToTitleCase(oTi.ToLower(eValue.ToString().Replace("_", " ")));
        }

        /// <summary>
        /// Gets the description of a specific enum value.
        /// </summary>
        public static string Description(this Enum eValue)
        {
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (nAttributes.Any()) return ((DescriptionAttribute) nAttributes.First()).Description;
           
            var oTi = CultureInfo.CurrentCulture.TextInfo;
            return oTi.ToTitleCase(oTi.ToLower(eValue.ToString().Replace("_", " ")));
        }

        /// <summary>
        /// Returns an enumerable collection of all values and descriptions for an enum type.
        /// </summary>        
        public static IEnumerable<KeyValuePair<string, Enum>> GetAllValuesAndDescriptions<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an Enumeration type");
           
            return Enum.GetValues(typeof (TEnum))
                       .Cast<Enum>()
                       .Select(e => new KeyValuePair<string, Enum>(e.Description(), e));
        }

        public static string GetDescription(this Type myObject, string propName)
        {
            return TypeDescriptor.GetProperties(myObject)
                .Cast<PropertyDescriptor>()
                .Where(prop => prop.Name.Equals(propName, StringComparison.CurrentCultureIgnoreCase))
                .Select(prop => prop.Description ?? prop.Name)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the description of a specific enum value.
        /// </summary>
        public static string GetEnumDescription(this Type eValue)
        {
            var nAttributes = eValue.GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (nAttributes.Any()) return ((DescriptionAttribute)nAttributes.First()).Description;

            var oTi = CultureInfo.CurrentCulture.TextInfo;
            return oTi.ToTitleCase(oTi.ToLower(eValue.ToString().Replace("_", " ")));
        }
    }

}





