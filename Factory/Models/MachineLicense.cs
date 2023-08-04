using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class MachineLicense
{
    public int MachineId { get; set; }
    public Machine Machine { get; set; }
    public string LicenseId { get; set; }
    public License License { get; set; }

}