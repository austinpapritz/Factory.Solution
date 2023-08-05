using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class Machine
{
    // `Machine` has a many-to-many relationship with License class via the `MachineLicense` class.
    public Machine()
    {
        this.Engineers = new List<Engineer>();
        this.MachineLicenses = new HashSet<MachineLicense>();
    }

    public int MachineId { get; set; }
    public string Country { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    // Usually as HashSet to efficiently match up Ids.
    public ICollection<MachineLicense> MachineLicenses { get; set; }

    // This allows you to easily assign a list of qualified engineers to a `Machine` model in the controller.
    public virtual ICollection<Engineer> Engineers { get; set; }

}