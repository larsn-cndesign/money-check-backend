using Microsoft.EntityFrameworkCore;

namespace MoneyCheck.Persistance.Contexts
{
  /// <summary>
  /// Represents the context used for migrations (not runtime).
  /// </summary>
  public class MigrationDbContext : DbContext
  {
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
    {
      ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(MigrationDbContext).Assembly);

      foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        relationship.DeleteBehavior = DeleteBehavior.Restrict;
    }
  }
}