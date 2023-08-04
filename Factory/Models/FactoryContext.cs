using Microsoft.EntityFrameworkCore;

namespace Factory.Models;

public class FactoryContext : DbContext
{
    public DbSet<Engineer> Engineers { get; set; }
    public DbSet<EngineerLicense> EngineerLicenses { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<MachineLicense> MachineLicenses { get; set; }
    public DbSet<License> Licenses { get; set; }

    public FactoryContext(DbContextOptions options) : base(options) { }
}
