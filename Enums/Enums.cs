using System.ComponentModel;

namespace CLib.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ImageFileExtensions
    {
        [Description("Joint Photographic Experts Group")]
        Jpg,
        Jpeg,
        [Description("Portable Network Graphic")]
        Png,
        [Description("CompuServe's Graphics Interchange Format")]
        Gif,
        [Description("Microsoft Windows Bitmap formatted image")]
        Bmp,
        [Description("File format used for icons in Microsoft Windows")]
        Ico,
        [Description("Tagged Image File Format")]
        Tif,
        Tiff
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TextFileExtensions
    {
        [Description("ASCII or Unicode plaintext")]
        Txt,
        Log,
        [Description("ASCII or extended ASCII text file")]
        Nfo,
        [Description("C source")]
        C,
        [Description("C++ source")]
        Cpp,
        Cc,
        Cxx,
        [Description("C/C++ header file")]
        H,
        [Description("C++ header file")]
        Hpp,
        Hxx,
        [Description("C# source")]
        Cs,
        [Description("Visual Basic.NET source")]
        Vb,
        [Description("HyperText Markup Language")]
        Html,
        Htm,
        [Description("eXtensible HyperText Markup Language")]
        Xhtml,
        Xht,
        [Description("eXtensible Markup Language")]
        Xml,
        [Description("Cascading Style Sheets")]
        Css,
        [Description("JavaScript and JScript")]
        Js,
        [Description("Hypertext Preprocessor")]
        Php,
        [Description("Batch file")]
        Bat,
        [Description("Java source")]
        Java,
        [Description("Lua")]
        Lua,
        [Description("Python source")]
        Py,
        [Description("Perl")]
        Pl,
        [Description("Visual Studio solution")]
        Sln
    }

    public enum VideoFileExtensions
    {
        [Description("MPEG-4 Video File")]
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

  
    public enum TaskStatus
    {
        InQueue,
        Preparing,
        Working,
        Stopping,
        Completed
    }

    public enum TaskProgress
    {
        ReportStarted,
        ReportProgress
    }

   
    public enum HashType
    {
        [Description("CRC-32")]
        Crc32,
        [Description("MD5")]
        Md5,
        [Description("SHA-1")]
        Sha1,
        [Description("SHA-256")]
        Sha256,
        [Description("SHA-384")]
        Sha384,
        [Description("SHA-512")]
        Sha512,
        [Description("RIPEMD-160")]
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
        [Description("Stable version")]
        Stable,
        [Description("Beta version")]
        Beta,
        [Description("Dev version")]
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
