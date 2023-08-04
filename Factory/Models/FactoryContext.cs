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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Create composite pk for join tables
        modelBuilder.Entity<EngineerLicense>()
            .HasKey(el => new { el.EngineerId, el.LicenseId });
        modelBuilder.Entity<MachineLicense>()
            .HasKey(ml => new { ml.MachineId, ml.LicenseId });
    }

}
