using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class EngineerLicense
{
    public int EngineerId { get; set; }
    public Engineer Engineer { get; set; }
    public string LicenseId { get; set; }
    public License License { get; set; }

}