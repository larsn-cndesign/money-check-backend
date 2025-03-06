using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class BudgetVersionConfiguration : IEntityTypeConfiguration<BudgetVersion>
  {
    public void Configure(EntityTypeBuilder<BudgetVersion> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("version");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => new { p.BudgetYearId })
        .IsUnique()
        .HasDatabaseName("fk_version_budget_year");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName("version_id")
        .IsRequired();

      modelBuilder.Property(p => p.BudgetYearId)
        .HasColumnName("budget_year_id")
        .IsRequired();

      modelBuilder.Property(p => p.VersionName)
        .HasColumnName(("version_name"))
        .HasMaxLength(15)
        .IsRequired();

      modelBuilder.Property(p => p.DateCreated)
        .HasColumnName("date_created")
        .HasColumnType("date")
        .IsRequired();

      modelBuilder.Property(p => p.IsClosed)
        .HasColumnName("is_closed")
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
        .HasOne(budgetVersion => budgetVersion.BudgetYear)
        .WithMany(budgetYear => budgetYear.BudgetVersions)
        .HasForeignKey(budgetVersion => budgetVersion.BudgetYearId)
        .HasConstraintName("fk_version_budget_year");
    }
  }
}