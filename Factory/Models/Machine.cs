using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public class Machine
{
    public Machine()
    {
        this.Engineers = new HashSet<Engineer>();
        this.MachineLicenses = new HashSet<MachineLicense>();
    }
    public int MachineId { get; set; }
    public string Country { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public virtual ICollection<Engineer> Engineers { get; set; }
    public ICollection<MachineLicense> MachineLicenses { get; set; }

}