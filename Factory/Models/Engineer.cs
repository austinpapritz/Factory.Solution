using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class Engineer
{
    public int EngineerId { get; set; }
    public string Name { get; set; }
    public ICollection<EngineerLicense> EngineerLicenses { get; set; }

}