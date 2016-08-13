using System;
using System.Text.RegularExpressions;

namespace eLib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class OsHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Version OsVersion = Environment.OSVersion.Version;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsXp() => OsVersion.Major == 5 && OsVersion.Minor == 1;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsXpOrGreater() => (OsVersion.Major == 5 && OsVersion.Minor >= 1) || OsVersion.Major > 5;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsVista() => OsVersion.Major == 6;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindowsVistaOrGreater() => OsVersion.Major >= 6;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows7() => OsVersion.Major == 6 && OsVersion.Minor == 1;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows7OrGreater() => (OsVersion.Major == 6 && OsVersion.Minor >= 1) || OsVersion.Major > 6;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows8() => OsVersion.Major == 6 && OsVersion.Minor == 2;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows8OrGreater() => (OsVersion.Major == 6 && OsVersion.Minor >= 2) || OsVersion.Major > 6;

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
