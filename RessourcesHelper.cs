using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace CLib
{
    /// <summary>
    /// Ressources Helper
    /// </summary>
    public class RessourcesHelper
    {

        /// <summary>
        /// Load a resource WPF-BitmapImage (png, bmp, ...) from embedded resource defined as 'Resource' not as 'Embedded resource'.
        /// </summary>
        /// <param name="pathInApplication">Path without starting slash</param>
        /// <param name="assembly">Usually 'Assembly.GetExecutingAssembly()'. If not mentionned, I will use the calling assembly</param>
        /// <returns></returns>
        public static BitmapImage LoadBitmapFromResource(string pathInApplication, Assembly assembly = null)
        {
            if (assembly == null) assembly = Assembly.GetCallingAssembly();
            
            if (pathInApplication[0] == '/')
            {
                pathInApplication = pathInApplication.Substring(1);
            }
            return new BitmapImage(new Uri(@"pack://application:,,,/" + assembly.GetName().Name + ";component/" + pathInApplication, UriKind.Absolute));
        }


        /// <summary>
        /// Get Embedded Ressource in Assembly
        /// </summary>
        /// <param name="pathInAssembly">"DataService.Migrations.InitData.Students.sql"</param>
        /// <param name="thisAssembly"></param>
        /// <returns></returns>
        public static string GetEmbeddedResource(string pathInAssembly, Assembly thisAssembly = null)
        {
            if (thisAssembly == null) thisAssembly = Assembly.GetCallingAssembly();
            
            using (var s = thisAssembly.GetManifestResourceStream(pathInAssembly))
            {
                if (s == null) return null;
                using (var sr = new StreamReader(s)) return sr.ReadToEnd();
            }         
        }



    }
}
