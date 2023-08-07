namespace Factory.Models;

public interface ILicenseable
{
    int LicenseId { get; set; }
    public License License { get; set; }
}
