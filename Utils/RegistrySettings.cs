using System;
using System.Reflection;
using Microsoft.Win32;

namespace eLib.Utils
{
    public static class RegistrySettings
    {
        private static readonly RegistryKey BaseRegistryKey = Registry.CurrentUser;
        private static string _subKey = Assembly.GetEntryAssembly().ToString();

        public static string SubRoot
        {
            set
            { _subKey = value; }
        }

        public static string Read(string keyName, object defaultValue)
        {
            try
            {
                // Opening the registry key 
                var rk = BaseRegistryKey;

                // Open a subKey as read-only 
                var sk1 = rk.OpenSubKey(_subKey);

                // If the RegistrySubKey doesn't exist return default value
                if (sk1 == null)
                    return defaultValue.ToString();

                // If the RegistryKey exists I get its value 
                // or defaultValue is returned. 
                return string.IsNullOrEmpty(sk1.GetValue(keyName).ToString()) ? defaultValue.ToString() : sk1.GetValue(keyName).ToString();
            }
            catch (Exception)
            {
                return defaultValue.ToString();
            }
        }

        public static bool Write(string keyName, object value)
        {
            try
            {
                // Setting
                var rk = BaseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                var sk1 = rk.CreateSubKey(_subKey);
                // Save the value
                sk1?.SetValue(keyName, value);

                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}
