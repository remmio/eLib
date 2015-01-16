using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using IWshRuntimeLibrary;

namespace CLib
{
    public static class ShortcutHelpers
    {
        //public static bool Create(string shortcutPath, string targetPath, string arguments = "")
        //{
        //    if (!string.IsNullOrEmpty(shortcutPath) && !string.IsNullOrEmpty(targetPath) && File.Exists(targetPath))
        //    {
        //        try
        //        {
        //            IWshShell wsh = new WshShellClass();
        //            IWshShortcut shortcut = (IWshShortcut)wsh.CreateShortcut(shortcutPath);
        //            shortcut.TargetPath = targetPath;
        //            shortcut.Arguments = arguments;
        //            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
        //            shortcut.Save();

        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            DebugHelper.WriteException(e);
        //        }
        //    }
        //    return false;
        //}

        public static bool Delete(string shortcutPath)
        {
            if (!string.IsNullOrEmpty(shortcutPath) && File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
                return true;
            }

            return false;
        }

        //public static bool SetShortcut(bool create, Environment.SpecialFolder specialFolder, string arguments = "")
        //{
        //    string shortcutPath = GetShortcutPath(specialFolder);

        //    if (create)
        //    {
        //        return Create(shortcutPath, Application.ExecutablePath, arguments);
        //    }

        //    return Delete(shortcutPath);
        //}

        public static bool CheckShortcut(Environment.SpecialFolder specialFolder)
        {
            string shortcutPath = GetShortcutPath(specialFolder);
            return File.Exists(shortcutPath);
        }

        private static string GetShortcutPath(Environment.SpecialFolder specialFolder)
        {
            string folderPath = Environment.GetFolderPath(specialFolder);
            string shortcutPath = Path.Combine(folderPath, "ShareX");

            if (!Path.GetExtension(shortcutPath).Equals(".lnk", StringComparison.InvariantCultureIgnoreCase))
            {
                shortcutPath = Path.ChangeExtension(shortcutPath, "lnk");
            }

            return shortcutPath;
        }











    }
}
