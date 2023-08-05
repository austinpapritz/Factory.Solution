using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

// Engineer has a many-to-many relationship with License class via the EngineerLicense class.
public class Engineer
{
    public Engineer()
    {
        this.Machines = new HashSet<Machine>();
        this.EngineerLicenses = new HashSet<EngineerLicense>();
    }

    public int EngineerId { get; set; }
    public string Name { get; set; }
    // Usually as HashSet to efficiently match up Ids.
    public ICollection<EngineerLicense> EngineerLicenses { get; set; }
    // Navigation field to easily assign a list of machines to an Engineer model in controller before sending to view.
    public virtual ICollection<Machine> Machines { get; set; }
}