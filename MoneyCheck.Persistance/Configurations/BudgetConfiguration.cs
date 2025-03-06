using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
  {
    public void Configure(EntityTypeBuilder<Budget> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("budget");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => p.BudgetName)
        .IsUnique()
        .HasDatabaseName("uk_budget__budget_name");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName("budget_id")
        .IsRequired();

      modelBuilder.Property(p => p.BudgetName)
        .HasColumnName("budget_name")
        .HasMaxLength(45)
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
    }
  }
}