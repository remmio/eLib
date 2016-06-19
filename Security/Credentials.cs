using System;
using CredentialManagement;
using eLib.Utils;

namespace eLib.Security
{
    public static class Credentials
    {

        public static string Get()
        {
            return Get("Token");
        }

        public static bool Set(string token)
        {
            try
            {
                return new Credential
                {
                    Target = "Token",
                    Password = token,
                    Type = CredentialType.DomainPassword,
                    PersistanceType = PersistanceType.Session,
                }.Save();
            }
            catch (Exception exception)
            {
                exception.Log();
                return false;
            }
        }

        public static bool Delete()
        {
            try
            {
                return new Credential { Target = "Token" }.Delete();
            }
            catch (Exception exception)
            {
                exception.Log();
                return false;
            }
        }

        #region IMPLEMENTATION

        public static string Get(string key)
        {
            var credential = new Credential
            {
                Target = key,
                Type = CredentialType.DomainPassword,
                PersistanceType = PersistanceType.Session
            };
            if (!credential.Exists())
                return null;

            credential.Load();
            return credential.Password;
        }

        public static bool Set(string key, string value)
        {
            try
            {
                return new Credential
                {
                    Target = key,
                    Password = value,
                    Type = CredentialType.DomainPassword,
                    PersistanceType = PersistanceType.Session,
                }.Save();
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
                return new Credential {Target = key}.Delete();
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
