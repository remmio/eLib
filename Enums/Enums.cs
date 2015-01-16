using System.ComponentModel;

namespace CLib.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ImageFileExtensions
    {
        [Description("Joint Photographic Experts Group")]
        jpg,
        jpeg,
        [Description("Portable Network Graphic")]
        png,
        [Description("CompuServe's Graphics Interchange Format")]
        gif,
        [Description("Microsoft Windows Bitmap formatted image")]
        bmp,
        [Description("File format used for icons in Microsoft Windows")]
        ico,
        [Description("Tagged Image File Format")]
        tif,
        tiff
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TextFileExtensions
    {
        [Description("ASCII or Unicode plaintext")]
        txt,
        log,
        [Description("ASCII or extended ASCII text file")]
        nfo,
        [Description("C source")]
        c,
        [Description("C++ source")]
        cpp,
        cc,
        cxx,
        [Description("C/C++ header file")]
        h,
        [Description("C++ header file")]
        hpp,
        hxx,
        [Description("C# source")]
        cs,
        [Description("Visual Basic.NET source")]
        vb,
        [Description("HyperText Markup Language")]
        html,
        htm,
        [Description("eXtensible HyperText Markup Language")]
        xhtml,
        xht,
        [Description("eXtensible Markup Language")]
        xml,
        [Description("Cascading Style Sheets")]
        css,
        [Description("JavaScript and JScript")]
        js,
        [Description("Hypertext Preprocessor")]
        php,
        [Description("Batch file")]
        bat,
        [Description("Java source")]
        java,
        [Description("Lua")]
        lua,
        [Description("Python source")]
        py,
        [Description("Perl")]
        pl,
        [Description("Visual Studio solution")]
        sln
    }

    public enum VideoFileExtensions
    {
        [Description("MPEG-4 Video File")]
        mp4,
        m4v
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
        CRC32,
        [Description("MD5")]
        MD5,
        [Description("SHA-1")]
        SHA1,
        [Description("SHA-256")]
        SHA256,
        [Description("SHA-384")]
        SHA384,
        [Description("SHA-512")]
        SHA512,
        [Description("RIPEMD-160")]
        RIPEMD160
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
        None, RGBA, HSB, CMYK, Hex, Decimal
    }

    public enum ColorFormat
    {
        RGB, RGBA, ARGB
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
