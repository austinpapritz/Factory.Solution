using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class Engineer
{
    public Engineer()
    {
        this.Machines = new HashSet<Machine>();
        this.EngineerLicenses = new HashSet<EngineerLicense>();
    }

    public int EngineerId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Machine> Machines { get; set; }
    public ICollection<EngineerLicense> EngineerLicenses { get; set; }

}