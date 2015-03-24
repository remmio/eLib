using System;
using System.Text.RegularExpressions;

namespace CLib.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationsHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Version OsVersion = Environment.OSVersion.Version;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsXp()
        {
            return OsVersion.Major == 5 && OsVersion.Minor == 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsXpOrGreater()
        {
            return (OsVersion.Major == 5 && OsVersion.Minor >= 1) || OsVersion.Major > 5;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsVista()
        {
            return OsVersion.Major == 6;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsVistaOrGreater()
        {
            return OsVersion.Major >= 6;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows7()
        {
            return OsVersion.Major == 6 && OsVersion.Minor == 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows7OrGreater()
        {
            return (OsVersion.Major == 6 && OsVersion.Minor >= 1) || OsVersion.Major > 6;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows8()
        {
            return OsVersion.Major == 6 && OsVersion.Minor == 2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows8OrGreater()
        {
            return (OsVersion.Major == 6 && OsVersion.Minor >= 2) || OsVersion.Major > 6;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsValidIpAddress(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return false;

            const string pattern = @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";

            return Regex.IsMatch(ip.Trim(), pattern);
        }


    }
}
