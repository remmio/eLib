﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using eLib.Enums;

namespace eLib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class UrlHelper
    {





        #region URL

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Task.Run(() =>
                {
                    try
                    {
                        Process.Start(url);
                    }
                    catch (Exception e)
                    {
                        DebugHelper.Log(e, $"OpenURL({url}) failed");
                    }
                });
            }
        }

        private static string Encode(string text, string unreservedCharacters)
        {
            var result = new StringBuilder();

            if (!string.IsNullOrEmpty(text))
            {
                foreach (var c in text)
                {
                    if (unreservedCharacters.Contains(c))
                    {
                        result.Append(c);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes(c.ToString());

                        foreach (var b in bytes)
                        {
                            result.AppendFormat(CultureInfo.InvariantCulture, "%{0:X2}", b);
                        }
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlEncode(string text) => Encode(text, FilesHelper.FilesHelper.UrlCharacters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UrlPathEncode(string text) => Encode(text, FilesHelper.FilesHelper.UrlPathCharacters);

        public static string HtmlEncode(string text)
        {
            var chars = HttpUtility.HtmlEncode(text).ToCharArray();
            var result = new StringBuilder(chars.Length + (int) (chars.Length*0.1));

            foreach (var c in chars)
            {
                var value = Convert.ToInt32(c);

                if (value > 127)
                {
                    result.AppendFormat("&#{0};", value);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public static string CombineUrl(string url1, string url2)
        {
            var url1Empty = string.IsNullOrEmpty(url1);
            var url2Empty = string.IsNullOrEmpty(url2);

            if (url1Empty && url2Empty)
            {
                return string.Empty;
            }

            if (url1Empty)
            {
                return url2;
            }

            if (url2Empty)
            {
                return url1;
            }

            if (url1.EndsWith("/"))
            {
                url1 = url1.Substring(0, url1.Length - 1);
            }

            if (url2.StartsWith("/"))
            {
                url2 = url2.Remove(0, 1);
            }

            return url1 + "/" + url2;
        }

        public static string CombineUrl(params string[] urls) => urls.Aggregate(CombineUrl);

        public static bool IsValidUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Trim();
                return !url.StartsWith("file://") && Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
            }

            return false;
        }

        public static bool IsValidUrlRegex(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;

            // https://gist.github.com/729294
            const string pattern = "^" +
                                   // protocol identifier
                                   "(?:(?:https?|ftp)://)" +
                                   // user:pass authentication
                                   "(?:\\S+(?::\\S*)?@)?" +
                                   "(?:" +
                                   // IP address exclusion
                                   // private & local networks
                                   "(?!10(?:\\.\\d{1,3}){3})" +
                                   "(?!127(?:\\.\\d{1,3}){3})" +
                                   "(?!169\\.254(?:\\.\\d{1,3}){2})" +
                                   "(?!192\\.168(?:\\.\\d{1,3}){2})" +
                                   "(?!172\\.(?:1[6-9]|2\\d|3[0-1])(?:\\.\\d{1,3}){2})" +
                                   // IP address dotted notation octets
                                   // excludes loopback network 0.0.0.0
                                   // excludes reserved space >= 224.0.0.0
                                   // excludes network & broacast addresses
                                   // (first & last IP address of each class)
                                   "(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])" +
                                   "(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}" +
                                   "(?:\\.(?:[1-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))" +
                                   "|" +
                                   // host name
                                   "(?:(?:[a-z\\u00a1-\\uffff0-9]+-?)*[a-z\\u00a1-\\uffff0-9]+)" +
                                   // domain name
                                   "(?:\\.(?:[a-z\\u00a1-\\uffff0-9]+-?)*[a-z\\u00a1-\\uffff0-9]+)*" +
                                   // TLD identifier
                                   "(?:\\.(?:[a-z\\u00a1-\\uffff]{2,}))" +
                                   ")" +
                                   // port number
                                   "(?::\\d{2,5})?" +
                                   // resource path
                                   "(?:/[^\\s]*)?" +
                                   "$";

            return Regex.IsMatch(url.Trim(), pattern, RegexOptions.IgnoreCase);
        }

        public static string AddSlash(string url, SlashType slashType) => AddSlash(url, slashType, 1);

        public static string AddSlash(string url, SlashType slashType, int count)
        {
            if (slashType == SlashType.Prefix)
            {
                if (url.StartsWith("/"))
                {
                    url = url.Remove(0, 1);
                }

                for (var i = 0; i < count; i++)
                {
                    url = "/" + url;
                }
            }
            else
            {
                if (url.EndsWith("/"))
                {
                    url = url.Substring(0, url.Length - 1);
                }

                for (var i = 0; i < count; i++)
                {
                    url += "/";
                }
            }

            return url;
        }

        public static string GetFileName(string path, bool checkExtension = false, bool urlDecode = false)
        {
            if (urlDecode)
            {
                string tempPath = null;

                for (var i = 0; i < 10 && path != tempPath; i++)
                {
                    tempPath = path;
                    path = HttpUtility.UrlDecode(path);
                }
            }

            if (path != null && path.Contains("/"))
            {
                path = path.Remove(0, path.LastIndexOf('/') + 1);
            }

            if (checkExtension && !Path.HasExtension(path))
            {
                return null;
            }

            return path;
        }

        public static string GetDirectoryPath(string path)
        {
            if (path.Contains("/"))
            {
                path = path.Substring(0, path.LastIndexOf('/'));
            }

            return path;
        }

        public static List<string> GetPaths(string path)
        {
            var result = new List<string>();
            var temp = string.Empty;
            var dirs = path.Split('/');
            foreach (var dir in dirs)
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    temp += "/" + dir;
                    result.Add(temp);
                }
            }

            return result;
        }

        private static readonly string[] UrlPrefixes = {"http://", "https://", "ftp://", "ftps://", "file://"};

        public static string FixPrefix(string url)
        {
            if (!HasPrefix(url))
            {
                return "http://" + url;
            }

            return url;
        }

        public static bool HasPrefix(string url) => UrlPrefixes.Any(x => url.StartsWith(x, StringComparison.InvariantCultureIgnoreCase));

        public static string RemovePrefixes(string url)
        {
            foreach (var prefix in UrlPrefixes)
            {
                if (url.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                {
                    url = url.Remove(0, prefix.Length);
                    break;
                }
            }

            return url;
        }

        #endregion



    }
}
