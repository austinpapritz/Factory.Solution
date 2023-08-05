using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Models;

public class License
{
    // A License has a many-to-many relationship with both Engineer (via EngineerLicense class) 
    // and Machine (via MachineLicense class). As such, it is the middle-man between the licenses
    // that a machine requires to be repaired, and all the licenses that an engineer has.

    public int LicenseId { get; set; }
    public string Name { get; set; }
    // Usually as HashSet to quickly match up Ids.
    ICollection<EngineerLicense> EngineerLicenses { get; set; }
    ICollection<MachineLicense> MachineLicenses { get; set; }

    // Allows Licenses to work with checkbox form inputs. Not mapped to db.
    [NotMapped]
    public bool IsSelected { get; set; }

}