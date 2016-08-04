using eLib.IEntity;

namespace eLib.IService
{
    public interface IMetaManager
    {
        string ProductName { get; }
        string ProductDescription { get; }
        byte[] ProductIcon { get; }
        string DevelopperEmail { get; }
        IAboutCard About { get; }
        string AssemblyProductVersion { get; }
        bool IsBeta { get; }
        string CurrentVersion{ get; }
        string VersionNumber { get; }
        string CompanyName { get; }
        string DevelopperName { get; }
        string[] CopyrightLicence{ get; }
        int CopyrightStartYear { get; }
    }
}