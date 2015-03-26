using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using libphonenumber;


namespace CLib.Validation {


    /// <summary>
    /// 
    /// </summary>
    public static class InputHelper {

        /// <summary>
        /// Verifie si c'est un numero de telephoone valid
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="contryCode"></param>
        /// <returns></returns>
        public static bool IsValidNumber (string phoneNumber, string contryCode = "MA")
        {                     
            try {
                var numberProto = PhoneNumberUtil.Instance.Parse(phoneNumber, contryCode);
                return numberProto.IsValidNumber ;
            } catch (NumberParseException)
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
            try {
                var numberProto = PhoneNumberUtil.Instance.Parse(phoneNumber, contryCode);
                return numberProto.IsValidNumber ? numberProto.Format(PhoneNumberUtil.PhoneNumberFormat.NATIONAL) : phoneNumber;
            } catch (NumberParseException) {
                return phoneNumber;
            }
        }

        /// <summary>
        /// Verifie si c est un email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail (string email) {
            if(string.IsNullOrEmpty(email)) { return false; }
            try {
                var regex = new Regex("^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])"+
                        "+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)"+
                        "((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|"+
                        "[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\u"+
                        "FDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|"+
                        "(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|"+
                        "[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900"+
                        "-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFF"+
                        "EF])))\\.?$", RegexOptions.IgnoreCase|RegexOptions.ExplicitCapture|RegexOptions.Compiled);
                return regex.IsMatch(email);
            } catch (RegexMatchTimeoutException) {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail (string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address==email;
            } catch {
                return false;
            }
        }

    }
}
