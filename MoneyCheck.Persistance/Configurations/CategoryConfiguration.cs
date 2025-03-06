using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("category");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => new { p.BudgetId, p.CategoryName })
        .IsUnique()
        .HasDatabaseName("uk_category__budget_id__category_name");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName("category_id")
        .IsRequired();

      modelBuilder.Property(p => p.BudgetId)
        .HasColumnName("budget_id")
        .IsRequired();

      modelBuilder.Property(p => p.CategoryName)
        .HasColumnName("category_name")
        .HasMaxLength(50)
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
        .HasOne(category => category.Budget)
        .WithMany(budget => budget.Categories)
        .HasForeignKey(category => category.BudgetId)
        .HasConstraintName("fk_category_budget");
    }
  }
}