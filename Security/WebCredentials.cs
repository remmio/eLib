using System;
using System.Reflection;
using Windows.Security.Credentials;
using eLib.Utils;

namespace eLib.Security
{
    public static class WebCredentials
    {
        public static string Get() => Get("Token");

        public static bool Set(string token) => Set("Token", token);

        public static bool Delete() => Delete("Token");

        #region IMPLEMENTATION

        public static string Get(string key)
        {
            try
            {
                return new PasswordVault().Retrieve(((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyTitleAttribute), false)).Title, key)?.Password;
            }
            catch (Exception exception)
            {
                exception.Log();
                return null;
            }
        }

        public static bool Set(string key, string value)
        {
            try
            {
                new PasswordVault().Add(
                    new PasswordCredential
                    {
                        Resource = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyTitleAttribute), false)).Title,
                        UserName = key,
                        Password = value
                    });
                return true;
            }
            catch (Exception exception)
            {
                exception.Log();
                return false;
            }
        }

        public static bool Delete(string key)
        {
            try
            {
                new PasswordVault().Remove(new PasswordCredential
                {
                    Resource = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyTitleAttribute), false)).Title,
                    UserName = key
                });
                return true;
            }
            catch (Exception exception)
            {
                exception.Log();
                return false;
            }
        }

        #endregion
    }
}
