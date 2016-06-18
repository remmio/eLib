using System.ComponentModel.DataAnnotations;
using eLib.Properties;

namespace eLib.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ImageFileExtensions
    {
        [Display(Description = "Joint_Photographic_Experts_Group", ResourceType = typeof (Resources))]
        Jpg,
        Jpeg,
        [Display(Description = "Portable_Network_Graphic", ResourceType = typeof (Resources))]
        Png,
        [Display(Description = "CompuServe_s_Graphics_Interchange_Format", ResourceType = typeof (Resources))]
        Gif,
        [Display(Description = "Microsoft_Windows_Bitmap_formatted_image", ResourceType = typeof (Resources))]
        Bmp,
        [Display(Description = "File_format_used_for_icons_in_Microsoft_Windows", ResourceType = typeof (Resources))]
        Ico,
        [Display(Description = "Tagged_Image_File_Format", ResourceType = typeof (Resources))]
        Tif,
        Tiff
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TextFileExtensions
    {
        [Display(Description = "ASCII or Unicode plaintext", ResourceType = typeof (Resources))]
        Txt,
        Log,
        [Display(Description = "ASCII or extended ASCII text file", ResourceType = typeof (Resources))]
        Nfo,
        [Display(Description = "C source", ResourceType = typeof (Resources))]
        C,
        [Display(Description = "C++ source", ResourceType = typeof (Resources))]
        Cpp,
        Cc,
        Cxx,
        [Display(Description = "C/C++ header file", ResourceType = typeof (Resources))]
        H,
        [Display(Description = "C++ header file", ResourceType = typeof (Resources))]
        Hpp,
        Hxx,
        [Display(Description = "C# source", ResourceType = typeof (Resources))]
        Cs,
        [Display(Description = "Visual Basic.NET source", ResourceType = typeof (Resources))]
        Vb,
        [Display(Description = "HyperText Markup Language", ResourceType = typeof (Resources))]
        Html,
        Htm,
        [Display(Description = "eXtensible HyperText Markup Language", ResourceType = typeof (Resources))]
        Xhtml,
        Xht,
        [Display(Description = "eXtensible Markup Language", ResourceType = typeof (Resources))]
        Xml,
        [Display(Description = "Cascading Style Sheets", ResourceType = typeof (Resources))]
        Css,
        [Display(Description = "JavaScript and JScript", ResourceType = typeof (Resources))]
        Js,
        [Display(Description = "Hypertext Preprocessor", ResourceType = typeof (Resources))]
        Php,
        [Display(Description = "Batch file", ResourceType = typeof (Resources))]
        Bat,
        [Display(Description = "Java source", ResourceType = typeof (Resources))]
        Java,
        [Display(Description = "Lua", ResourceType = typeof (Resources))]
        Lua,
        [Display(Description = "Python source", ResourceType = typeof (Resources))]
        Py,
        [Display(Description = "Perl", ResourceType = typeof (Resources))]
        Pl,
        [Display(Description = "Visual Studio solution", ResourceType = typeof (Resources))]
        Sln
    }

    public enum VideoFileExtensions
    {
        [Display(Description = "MPEG-4 Video File", ResourceType = typeof (Resources))]
        Mp4,
        M4V
    }

    public enum EncryptionStrength
    {
        Low = 128,
        Medium = 192,
        High = 256
    }
   
    public enum EInputType
    {
        None,
        Clipboard,
        FileSystem,
        Screenshot
    }

  
   

    public enum TaskProgress
    {
        ReportStarted,
        ReportProgress
    }

   
    public enum HashType
    {
        [Display(Description = "CRC-32", ResourceType = typeof (Resources))]
        Crc32,
        [Display(Description = "MD5", ResourceType = typeof (Resources))]
        Md5,
        [Display(Description = "SHA-1", ResourceType = typeof (Resources))]
        Sha1,
        [Display(Description = "SHA-256", ResourceType = typeof (Resources))]
        Sha256,
        [Display(Description = "SHA-384", ResourceType = typeof (Resources))]
        Sha384,
        [Display(Description = "SHA-512", ResourceType = typeof (Resources))]
        Sha512,
        [Display(Description = "RIPEMD-160", ResourceType = typeof (Resources))]
        Ripemd160
    }

    public enum TokenType
    {
        Unknown,
        Whitespace,
        Symbol,
        Literal,
        Identifier,
        Numeric,
        Keyword
    }

   
    public enum DownloaderFormStatus
    {
        Waiting,
        DownloadStarted,
        DownloadCompleted,
        InstallStarted
    }

    public enum InstallType
    {
        Default,
        Silent,
        VerySilent,
        Event
    }

    public enum ReleaseChannelType
    {
        [Display(Description = "Stable version", ResourceType = typeof (Resources))]
        Stable,
        [Display(Description = "Beta version", ResourceType = typeof (Resources))]
        Beta,
        [Display(Description = "Dev version", ResourceType = typeof (Resources))]
        Dev
    }

    public enum UpdateStatus
    {
        None,
        UpdateCheckFailed,
        UpdateAvailable,
        UpToDate
    }

    public enum PrintType
    {
        Image,
        Text
    }

    public enum DrawStyle
    {
        Hue,
        Saturation,
        Brightness,
        Red,
        Green,
        Blue
    }

    public enum ColorType
    {
        None, Rgba, Hsb, Cmyk, Hex, Decimal
    }

    public enum ColorFormat
    {
        Rgb, Rgba, Argb
    }

    public enum ProxyMethod 
    {
        None,
        Manual,
        Automatic
    }

    public enum SlashType
    {
        Prefix,
        Suffix
    }


}
