using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Models;

public class License
{
    public int LicenseId { get; set; }
    public string Name { get; set; }
    ICollection<EngineerLicense> EngineerLicenses { get; set; }
    ICollection<MachineLicense> MachineLicenses { get; set; }

    // Allows Licenses to work with checkbox form inputs. Not mapped to db.
    [NotMapped]
    public bool IsSelected { get; set; }

}