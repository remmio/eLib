using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace eLib.Utils
{
    public static class RegistryHelper
    {

        private static readonly string WindowsStartupRun = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private static readonly string ApplicationName = "eApp";
        private static readonly string ApplicationPath = $"\"{Application.ExecutablePath}\"";
        private static readonly string StartupPath = ApplicationPath + " -silent";

        private static readonly string ShellExtMenuFiles = @"Software\Classes\*\shell\" + ApplicationName;
        private static readonly string ShellExtMenuFilesCmd = ShellExtMenuFiles + @"\command";

        private static readonly string ShellExtMenuDirectory = @"Software\Classes\Directory\shell\" + ApplicationName;
        private static readonly string ShellExtMenuDirectoryCmd = ShellExtMenuDirectory + @"\command";

        private static readonly string ShellExtMenuFolders = @"Software\Classes\Folder\shell\" + ApplicationName;
        private static readonly string ShellExtMenuFoldersCmd = ShellExtMenuFolders + @"\command";

        private static readonly string ShellExtDesc = $"Uploaded With {ApplicationName}";
        private static readonly string ShellExtIcon = ApplicationPath + ",0";
        private static readonly string ShellExtPath = ApplicationPath + " \"%1\"";

        public static bool CheckStartWithWindows()
        {
            try
            {
                return CheckRegistry(WindowsStartupRun, ApplicationName, StartupPath);
            }
            catch (Exception e)
            {
                e.Log();
            }

            return false;
        }

        public static void SetStartWithWindows(bool startWithWindows)
        {
            try
            {
                using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(WindowsStartupRun, true))
                {
                    if (regkey != null)
                    {
                        if (startWithWindows)
                        {
                            regkey.SetValue(ApplicationName, StartupPath, RegistryValueKind.String);
                        }
                        else
                        {
                            regkey.DeleteValue(ApplicationName, false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                e.Log();
            }
        }

        public static bool CheckShellContextMenu()
        {
            try
            {
                return CheckRegistry(ShellExtMenuFilesCmd) && CheckRegistry(ShellExtMenuDirectoryCmd);
            }
            catch (Exception e)
            {
                e.Log();
            }

            return false;
        }

        public static void SetShellContextMenu(bool register)
        {
            try
            {
                if (register)
                {
                    UnregisterShellContextMenu();
                    RegisterShellContextMenu();
                }
                else
                {
                    UnregisterShellContextMenu();
                }
            }
            catch (Exception e)
            {
                e.Log();
            }
        }

        public static void RegisterShellContextMenu()
        {
            CreateRegistry(ShellExtMenuFiles, ShellExtDesc);
            CreateRegistry(ShellExtMenuFiles, "Icon", ShellExtIcon);
            CreateRegistry(ShellExtMenuFilesCmd, ShellExtPath);

            CreateRegistry(ShellExtMenuDirectory, ShellExtDesc);
            CreateRegistry(ShellExtMenuDirectory, "Icon", ShellExtIcon);
            CreateRegistry(ShellExtMenuDirectoryCmd, ShellExtPath);
        }

        public static void UnregisterShellContextMenu()
        {
            RemoveRegistry(ShellExtMenuFilesCmd);
            RemoveRegistry(ShellExtMenuFiles);
            RemoveRegistry(ShellExtMenuDirectoryCmd);
            RemoveRegistry(ShellExtMenuDirectory);
            RemoveRegistry(ShellExtMenuFoldersCmd);
            RemoveRegistry(ShellExtMenuFolders);
        }

        //public static ExternalProgram FindProgram(string name, string filename)
        //{
        //    // First method: HKEY_CLASSES_ROOT\Applications\{filename}\shell\{command}\command

        //    string[] commands = new string[] { "open", "edit" };

        //    foreach (string command in commands)
        //    {
        //        string path = string.Format(@"HKEY_CLASSES_ROOT\Applications\{0}\shell\{1}\command", filename, command);
        //        string value = Registry.GetValue(path, null, null) as string;

        //        if (!string.IsNullOrEmpty(value))
        //        {
        //            string filePath = value.ParseQuoteString();

        //            if (File.Exists(filePath))
        //            {
        //                DebugHelper.WriteLine("Found program with first method: " + filePath);
        //                return new ExternalProgram(name, filePath);
        //            }
        //        }
        //    }

        //    // Second method: HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\MuiCache

        //    using (RegistryKey programs = Registry.CurrentUser.OpenSubKey(@"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\MuiCache"))
        //    {
        //        if (programs != null)
        //        {
        //            foreach (string filePath in programs.GetValueNames())
        //            {
        //                if (!string.IsNullOrEmpty(filePath) && programs.GetValueKind(filePath) == RegistryValueKind.String)
        //                {
        //                    string programName = programs.GetValue(filePath, null) as string;

        //                    if (!string.IsNullOrEmpty(programName) && programName.Equals(name, StringComparison.InvariantCultureIgnoreCase) && File.Exists(filePath))
        //                    {
        //                        DebugHelper.WriteLine("Found program with second method: " + filePath);
        //                        return new ExternalProgram(name, filePath);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}

        public static void CreateRegistry(string path, string value)
        {
            CreateRegistry(path, null, value);
        }

        public static void CreateRegistry(string path, string name, string value)
        {
            using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(path))
            {
                if (rk != null)
                {
                    rk.SetValue(name, value, RegistryValueKind.String);
                }
            }
        }

        public static void CreateRegistry(string path, int value)
        {
            CreateRegistry(path, null, value);
        }

        public static void CreateRegistry(string path, string name, int value)
        {
            using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(path))
            {
                if (rk != null)
                {
                    rk.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
        }

        public static void RemoveRegistry(string path)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(path))
            {
                if (rk != null)
                {
                    Registry.CurrentUser.DeleteSubKey(path);
                }
            }
        }

        public static bool CheckRegistry(string path, string name = null, string value = null)
        {
            string registryValue = GetRegistryValue(path, name);

            if (registryValue != null)
            {
                return value == null || registryValue.Equals(value, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public static string GetRegistryValue(string path, string name = null)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(path))
            {
                if (rk != null)
                {
                    return rk.GetValue(name, null) as string;
                }
            }

            return null;
        }




















    }
}
