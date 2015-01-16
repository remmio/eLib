using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLib.Enums;

namespace CLib.FilesHelper
{
    /// <summary>
    /// 
    /// </summary>
    public static class FilesHelper
    {
        public const string Numbers = "0123456789"; // 48 ... 57
        public const string AlphabetCapital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // 65 ... 90
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz"; // 97 ... 122
        public const string Alphanumeric = Numbers + AlphabetCapital + Alphabet;
        public const string URLCharacters = Alphanumeric + "-._~"; // 45 46 95 126
        public const string URLPathCharacters = URLCharacters + "/"; // 47
        public const string ValidURLCharacters = URLPathCharacters + ":?#[]@!$&'()*+,;= ";

        private static bool IsValidFile(string filePath, Type enumType)
        {
            string ext = GetFilenameExtension(filePath);
            if (!String.IsNullOrEmpty(ext))
            {
                return Enum.GetNames(enumType).Any(x => ext.Equals(x, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }

       
        public static string GetFilenameExtension(string filePath)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                int pos = filePath.LastIndexOf('.');
                if (pos >= 0)
                {
                    return filePath.Substring(pos + 1).ToLowerInvariant();
                }
            }
            return null;
        }

        public static bool IsImageFile(string filePath)
        {
            return IsValidFile(filePath, typeof(ImageFileExtensions));
        }
        public static bool IsTextFile(string filePath)
        {
            return IsValidFile(filePath, typeof(TextFileExtensions));
        }
        
        public static string GetValidFileName(string fileName)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            return new string(fileName.Where(c => !invalidFileNameChars.Contains(c)).ToArray());
        }

        public static void OpenFolderWithFile(string filePath)
        {
            if (!String.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                Process.Start("explorer.exe", String.Format("/select,\"{0}\"", filePath));
            }
        }


        public static void OpenFile(string filepath)
        {
            if (!String.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                Task.Run(() =>
                {
                    try
                    {
                        Process.Start(filepath);
                    }
                    catch (Exception e)
                    {
                        DebugHelper.WriteException(e, String.Format("OpenFile({0}) failed", filepath));
                    }
                });
            }
        }

        public static bool BrowseFile(string title, TextBox tb, string initialDirectory = "")
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = title;

                try
                {
                    string path = tb.Text;

                    if (!String.IsNullOrEmpty(path))
                    {
                        path = Path.GetDirectoryName(path);

                        if (Directory.Exists(path))
                        {
                            ofd.InitialDirectory = path;
                        }
                    }
                }
                finally
                {
                    if (String.IsNullOrEmpty(ofd.InitialDirectory) && !String.IsNullOrEmpty(initialDirectory))
                    {
                        ofd.InitialDirectory = initialDirectory;
                    }
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tb.Text = ofd.FileName;
                    return true;
                }
            }

            return false;
        }

        public static void CreateDirectoryIfNotExist(string path, bool isFilePath = true)
        {
            if (!String.IsNullOrEmpty(path))
            {
                if (isFilePath)
                {
                    path = Path.GetDirectoryName(path);
                }

                if (!String.IsNullOrEmpty(path) && !Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception e)
                    {
                        DebugHelper.WriteException(e);                        
                    }
                }
            }
        }

        public static void BackupFileMonthly(string filepath, string destinationFolder)
        {
            if (!String.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string extension = Path.GetExtension(filepath);
                string newFilename = String.Format("{0}-{1:yyyy-MM}{2}", filename, DateTime.Now, extension);
                string newFilepath = Path.Combine(destinationFolder, newFilename);

                if (!File.Exists(newFilepath))
                {
                    CreateDirectoryIfNotExist(newFilepath);
                    File.Copy(filepath, newFilepath, false);
                }
            }
        }


        public static void BackupFileWeekly(string filepath, string destinationFolder)
        {
            if (!String.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                DateTime dateTime = DateTime.Now;
                string extension = Path.GetExtension(filepath);
                string newFilename = String.Format("{0}-{1:yyyy-MM}-W{2:00}{3}", filename, dateTime, dateTime.DayOfYear, extension);
                string newFilepath = Path.Combine(destinationFolder, newFilename);

                if (!File.Exists(newFilepath))
                {
                    CreateDirectoryIfNotExist(newFilepath);
                    File.Copy(filepath, newFilepath, false);
                }
            }
        }

        public static string GetAbsolutePath(string path)
        {
            if (!Path.IsPathRooted(path)) // Is relative path?
            {
                path = Path.Combine(Application.StartupPath, path);
            }

            return Path.GetFullPath(path);
        }
    }
}
