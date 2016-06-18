using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using eLib.Enums;
using eLib.Utils;

namespace eLib.FilesHelper
{
    /// <summary>
    /// 
    /// </summary>
    public static class FilesHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Numbers = "0123456789"; // 48 ... 57
        /// <summary>
        /// 
        /// </summary>
        public const string AlphabetCapital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // 65 ... 90
        /// <summary>
        /// 
        /// </summary>
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz"; // 97 ... 122
        /// <summary>
        /// 
        /// </summary>
        public const string Alphanumeric = Numbers + AlphabetCapital + Alphabet;
        /// <summary>
        /// 
        /// </summary>
        public const string UrlCharacters = Alphanumeric + "-._~"; // 45 46 95 126
        /// <summary>
        /// 
        /// </summary>
        public const string UrlPathCharacters = UrlCharacters + "/"; // 47
        /// <summary>
        /// 
        /// </summary>
        public const string ValidUrlCharacters = UrlPathCharacters + ":?#[]@!$&'()*+,;= ";


        /// <summary>
        ///in this format=> "Shirley","Rose","Sean","Jeremy"
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string[] FileToStringList(string file)
        {
            var names = Regex.Split(file, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            for (var i = 0; i < names.Length; i++)
                names[i] = names[i].Replace("\"", "");
            return names;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private static bool IsValidFile(string filePath, Type enumType)
        {
            var ext = GetFilenameExtension(filePath);
            return !string.IsNullOrEmpty(ext) && Enum.GetNames(enumType).Any(x => ext.Equals(x, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
       /// 
       /// </summary>
       /// <param name="filePath"></param>
       /// <returns></returns>
        public static string GetFilenameExtension(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
            var pos = filePath.LastIndexOf('.');

            return pos >= 0 ? filePath.Substring(pos + 1).ToLowerInvariant() : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsImageFile(string filePath) => IsValidFile(filePath, typeof(ImageFileExtensions));

        /// <summary>
        /// Verifie si cest un document Pdf ou Doc
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsDocumentFile (string filePath) => IsValidFile(filePath, typeof(DocumentType));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsTextFile(string filePath)
        {
            return IsValidFile(filePath, typeof(TextFileExtensions));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetValidFileName(string fileName)
        {
            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            return new string(fileName.Where(c => !invalidFileNameChars.Contains(c)).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public static void OpenFolderWithFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public static void OpenFile(string filepath)
        {
            if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                Task.Run(() =>
                {
                    try
                    {
                        Process.Start(filepath);
                    }
                    catch (Exception e)
                    {
                        DebugHelper.Log(e, string.Format("OpenFile({0}) failed", filepath));
                    }
                });
            }
        }

        /// <summary>
        /// Open Dialog for Image File
        /// </summary>
        public static string BrowseDocuments (string title, string path, string initialDirectory = default(string)) {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = title;
                ofd.Filter = "Document (*.pdf, *.doc, *.docx)|*.pdf; *.doc; *.docx";

                try
                {

                    if (!string.IsNullOrEmpty(path))
                    {
                        path = Path.GetDirectoryName(path);

                        if (path != null && Directory.Exists(path))
                            ofd.InitialDirectory = path;
                    }
                }
                finally
                {
                    if (string.IsNullOrEmpty(ofd.InitialDirectory) && !string.IsNullOrEmpty(initialDirectory))
                        ofd.InitialDirectory = initialDirectory;
                }

                if (ofd.ShowDialog() != DialogResult.OK)
                    return ofd.FileName;
                return ofd.FileName;
            }
        }

        public static string BrowseLicence(string title, string path, string initialDirectory = default(string))
        {   
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = title;
                ofd.Filter = "Document (*.lic)|*.lic;";

                try
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        path = Path.GetDirectoryName(path);

                        if (path != null && Directory.Exists(path))
                            ofd.InitialDirectory = path;
                    }
                }
                finally
                {
                    if (string.IsNullOrEmpty(ofd.InitialDirectory) && !string.IsNullOrEmpty(initialDirectory))
                        ofd.InitialDirectory = initialDirectory;
                }

                return ofd.ShowDialog() != DialogResult.OK ? ofd.FileName : ofd.FileName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tb"></param>
        /// <param name="initialDirectory"></param>
        /// <returns></returns>
        public static bool BrowseFile(string title, TextBox tb, string initialDirectory = default(string))
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = title;

                try
                {
                    var path = tb.Text;

                    if (!string.IsNullOrEmpty(path))
                    {
                        path = Path.GetDirectoryName(path);

                        if (path != null && Directory.Exists(path))
                            ofd.InitialDirectory = path;
                    }
                }
                finally
                {
                    if (string.IsNullOrEmpty(ofd.InitialDirectory) && !string.IsNullOrEmpty(initialDirectory))
                        ofd.InitialDirectory = initialDirectory;
                }

                if (ofd.ShowDialog() != DialogResult.OK) return false;
                tb.Text = ofd.FileName;
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isFilePath"></param>
        public static void CreateDirectoryIfNotExist(string path, bool isFilePath = true)
        {
            if (string.IsNullOrEmpty(path)) return;
            if (isFilePath)
            {
                path = Path.GetDirectoryName(path);
            }

            if (string.IsNullOrEmpty(path) || Directory.Exists(path)) return;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                e.Log();                        
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="destinationFolder"></param>
        public static void BackupFileMonthly(string filepath, string destinationFolder)
        {
            if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string extension = Path.GetExtension(filepath);
                string newFilename = string.Format("{0}-{1:yyyy-MM}{2}", filename, DateTime.Now, extension);
                string newFilepath = Path.Combine(destinationFolder, newFilename);

                if (!File.Exists(newFilepath))
                {
                    CreateDirectoryIfNotExist(newFilepath);
                    File.Copy(filepath, newFilepath, false);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="destinationFolder"></param>
        public static void BackupFileWeekly(string filepath, string destinationFolder)
        {
            if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                DateTime dateTime = DateTime.Now;
                string extension = Path.GetExtension(filepath);
                string newFilename = string.Format("{0}-{1:yyyy-MM}-W{2:00}{3}", filename, dateTime, dateTime.DayOfYear, extension);
                string newFilepath = Path.Combine(destinationFolder, newFilename);

                if (!File.Exists(newFilepath))
                {
                    CreateDirectoryIfNotExist(newFilepath);
                    File.Copy(filepath, newFilepath, false);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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
