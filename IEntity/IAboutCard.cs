namespace eLib.IEntity
{
    public interface IAboutCard
    {
        string Copyright { get; }
        string CopyrightLicence { get; }
        string CurrentVersion { get; }
        string DevelopperEmail { get; }
        string DevelopperName { get; }
        bool IsBeta { get; }
        string ProductDescription { get; }
        byte[] ProductIcon { get; }
        string ProductName { get; }
    }
}