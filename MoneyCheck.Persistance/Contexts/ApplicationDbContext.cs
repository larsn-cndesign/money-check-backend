using Microsoft.EntityFrameworkCore;
using MoneyCheck.Domain.Common;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Contexts
{
  /// <summary>
  /// Represents the main database context used by the application.
  /// </summary>
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
      ChangeTracker.LazyLoadingEnabled = false;
    }

    // Add context here
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BudgetYear> BudgetYears { get; set; }
    public DbSet<BudgetVersion> BudgetVersions { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<BudgetItem> BudgetItems { get; set; }
    public DbSet<ActualItem> ActualItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.CreatedDate = DateTime.Now;
            break;

          case EntityState.Modified:
            entry.Entity.LastModifiedDate = DateTime.Now;
            break;
        }
      }
      return base.SaveChangesAsync(cancellationToken);
    }
  }
}