using System;
using System.Reflection;
using eLib.Utils;
using Microsoft.Win32;

namespace eLib.Program
{
   public static class StartUpRun
    {
       public static void InstallMeOnStartUp()
        {
            try
            {
                var key =
                    Registry.CurrentUser.OpenSubKey(
                        "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var curAssembly = Assembly.GetCallingAssembly();
                key?.SetValue(curAssembly.GetName().Name, curAssembly.Location);
            }
            catch (Exception exception)
            {
                exception.Log();
            }
        }
    }
}
