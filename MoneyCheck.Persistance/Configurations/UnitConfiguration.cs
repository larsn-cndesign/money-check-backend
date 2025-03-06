using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class UnitConfiguration : IEntityTypeConfiguration<Unit>
  {
    public void Configure(EntityTypeBuilder<Unit> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("unit");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => new { p.BudgetId, p.UnitName })
        .IsUnique()
        .HasDatabaseName("uk_unit__budget_id__unit_name");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName("unit_id")
        .IsRequired();

      modelBuilder.Property(p => p.BudgetId)
        .HasColumnName("budget_id")
        .IsRequired();

      modelBuilder.Property(p => p.UnitName)
        .HasColumnName("unit_name")
        .HasMaxLength(50)
        .IsRequired();

      modelBuilder.Property(p => p.UseCurrency)
        .HasColumnName("use_currency")
        .IsRequired();

      modelBuilder.Property(p => p.CreatedDate)
        .HasColumnName("row_created")
        .HasColumnType("datetime(0)")
        .HasDefaultValueSql("CURRENT_TIMESTAMP")
        .IsRequired();

      modelBuilder.Property(p => p.LastModifiedDate)
        .HasColumnName("row_updated")
        .HasColumnType("datetime(0)")
        .HasDefaultValueSql("CURRENT_TIMESTAMP")
        .IsRequired();

      // Relationships
      modelBuilder
        .HasOne(unit => unit.Budget)
        .WithMany(budget => budget.Units)
        .HasForeignKey(unit => unit.BudgetId)
        .HasConstraintName("fk_unit_budget");
    }
  }
}