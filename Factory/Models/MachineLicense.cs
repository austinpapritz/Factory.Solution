using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class MachineLicense
{
    // This class matches a machine's `MachineId` with the `LicenseId`s of the licenses required to repair it.
    // If the "License" part of both a `MachineLicense` and an `EngineerLicense` are the same, 
    // then ONE of the engineer's licenses matches up with ONE of the licenses that a machine requires.
    // If ALL of a `Machine`'s `MachineLicenses` can find a corresponding `EngineerLicense` for an `Engineer`,  
    // then that engineer meets every required license to repair the machine.
    public int MachineId { get; set; }
    public Machine Machine { get; set; }
    public int LicenseId { get; set; }
    public License License { get; set; }

}