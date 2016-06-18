using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace eLib.Utils {


    /// <summary>
    /// 
    /// </summary>
    public static class InputHelper
    {

        /// <summary>
        /// Verifie si c'est un numero de telephoone valid
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="contryCode"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string phoneNumber, string contryCode = "MA")
        {
            try
            {
                //if (string.IsNullOrEmpty(contryCode))
                //    contryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName;

                //var numberProto = PhoneNumberUtil.GetInstance().Parse(phoneNumber, contryCode);
                return PhoneNumberUtil.IsViablePhoneNumber(phoneNumber);
                    //isValidNumber(numberProto); //numberProto..IsValidNumber;
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

        /// <summary>
        /// Format le numero de telephone 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="contryCode"></param>
        /// <returns></returns>
        public static string FormatNumber(string phoneNumber, string contryCode = "MA")
        {
            try
            {
                if (string.IsNullOrEmpty(contryCode))
                    contryCode=RegionInfo.CurrentRegion.TwoLetterISORegionName;
                var p = PhoneNumberUtil.GetInstance().Parse(phoneNumber, contryCode);

                return PhoneNumberUtil.GetInstance().IsValidNumber(p)
                    ? p.NationalNumber.ToString(CultureInfo.CurrentCulture)
                    : phoneNumber;
            }
            catch (NumberParseException)
            {
                return phoneNumber;
            }
        }

        /// <summary>
        /// Verifie si c est un email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            try
            {
                var regex =
                    new Regex(
                        "^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])" +
                        "+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)" +
                        "((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|" +
                        "[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\u" +
                        "FDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|" +
                        "(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|" +
                        "[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900" +
                        "-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFF" +
                        "EF])))\\.?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// GetCurrencySymbol
        /// </summary>
        /// <returns></returns>
        public static string GetCurrencySymbol()
        {
            return RegionInfo.CurrentRegion.ISOCurrencySymbol;
        }


        

    }
}
