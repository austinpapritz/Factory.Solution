using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class EngineerLicense
{
    // This class matches the EngineerId with the LicenseId which corresponds with one of the licenses needed 
    // to repair a machine. If the "License" part of both the `MachineLicense` and `EngineerLicense` are the same,  
    // then ONE of the engineer's licenses matches up with ONE of the licenses that a machine requires.
    // If ALL of a Machine's `MachineLicenses` can find a corresponding `EngineerLicense` of an Engineer, then
    // that Engineer meets every required license to repair the machine.
    public int EngineerId { get; set; }
    public Engineer Engineer { get; set; }
    public int LicenseId { get; set; }
    public License License { get; set; }

}