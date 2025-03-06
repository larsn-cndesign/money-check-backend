using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class BudgetYearConfiguration : IEntityTypeConfiguration<BudgetYear>
  {
    public void Configure(EntityTypeBuilder<BudgetYear> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("budget_year");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => new { p.Year, p.BudgetId })
        .IsUnique()
        .HasDatabaseName("uk_budget_year__budget_id__budget_year_id");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName(("budget_year_id"))
        .IsRequired();

      modelBuilder.Property(p => p.BudgetId)
        .HasColumnName(("budget_id"))
        .IsRequired();

      modelBuilder.Property(p => p.Year)
        .HasColumnName(("budget_year"))
        .IsRequired();

      modelBuilder.Property(p => p.CreatedDate)
        .HasColumnName(("row_created"))
        .HasColumnType("datetime(0)")
        .HasDefaultValueSql("CURRENT_TIMESTAMP")
        .IsRequired();

      modelBuilder.Property(p => p.LastModifiedDate)
        .HasColumnName(("row_updated"))
        .HasColumnType("datetime(0)")
        .HasDefaultValueSql("CURRENT_TIMESTAMP")
        .IsRequired();

      // Relationships
      modelBuilder
        .HasOne(budgetYear => budgetYear.Budget)
        .WithMany(budget => budget.BudgetYears)
        .HasForeignKey(budgetYear => budgetYear.BudgetId)
        .HasConstraintName("fk_budget_year_budget");
    }
  }
}