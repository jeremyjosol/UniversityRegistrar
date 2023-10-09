using Microsoft.EntityFrameworkCore;

namespace UniversityRegistrar.Models
{
  public class UniversityRegistrarContext : DbContext
  {
    // public DbSet<ModelName> ModelName { get; set; }

    public UniversityRegistrarContext(DbContextOptions options) : base(options) { }
  }
}