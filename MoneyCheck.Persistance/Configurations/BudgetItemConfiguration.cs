using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class BudgetItemConfiguration : IEntityTypeConfiguration<BudgetItem>
  {
    public void Configure(EntityTypeBuilder<BudgetItem> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("budget_item");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => new { p.CategoryId, p.UnitId, p.VersionId })
        .IsUnique()
        .HasDatabaseName("uk_budget_item__version_id__category_id__unit_id");

      modelBuilder.HasIndex(p => p.UnitId)
        .HasDatabaseName("fk_budget_item_unit");

      modelBuilder.HasIndex(p => p.VersionId)
        .HasDatabaseName("fk_budget_item_version");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName(("budget_item_id"))
        .IsRequired();

      modelBuilder.Property(p => p.VersionId)
        .HasColumnName(("version_id"))
        .IsRequired();

      modelBuilder.Property(p => p.CategoryId)
        .HasColumnName(("category_id"))
        .IsRequired();

      modelBuilder.Property(p => p.UnitId)
        .HasColumnName(("unit_id"))
        .IsRequired();

      modelBuilder.Property(p => p.CurrencyCode)
        .HasColumnName(("currency"))
        .HasMaxLength(3)
        .IsRequired(false);

      modelBuilder.Property(p => p.UnitValue)
        .HasColumnName("unit_value")
        .HasColumnType("decimal(9, 2)")
        .IsRequired();

      modelBuilder.Property(p => p.Note)
        .HasColumnName(("note"))
        .HasMaxLength(255)
        .IsRequired(false);

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
        .HasOne(budgetItem => budgetItem.Category)
        .WithMany(category => category.BudgetItems)
        .HasForeignKey(budgetItem => budgetItem.CategoryId)
        .HasConstraintName("fk_budget_item_category");

      modelBuilder
        .HasOne(budgetItem => budgetItem.Unit)
        .WithMany(unit => unit.BudgetItems)
        .HasForeignKey(budgetItem => budgetItem.UnitId)
        .HasConstraintName("fk_budget_item_unit");

      modelBuilder
        .HasOne(budgetItem => budgetItem.BudgetVersion)
        .WithMany(budgetVersion => budgetVersion.BudgetItems)
        .HasForeignKey(budgetItem => budgetItem.VersionId)
        .HasConstraintName("fk_budget_item_version");
    }
  }
}