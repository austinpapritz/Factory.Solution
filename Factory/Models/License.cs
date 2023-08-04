using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class License
{
    public int LicenseId { get; set; }
    public string Name { get; set; }
    ICollection<EngineerLicense> EngineerLicenses { get; set; }
    ICollection<MachineLicense> MachineLicenses { get; set; }

}