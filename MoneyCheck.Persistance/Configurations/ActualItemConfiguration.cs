using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class ActualItemConfiguration : IEntityTypeConfiguration<ActualItem>
  {
    public void Configure(EntityTypeBuilder<ActualItem> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("actual_item");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => new { p.BudgetId })
        .HasDatabaseName("fk_actual_item_budget");

      modelBuilder.HasIndex(p => new { p.CategoryId })
        .HasDatabaseName("fk_actual_item_category");

      modelBuilder.HasIndex(p => new { p.TripId })
        .HasDatabaseName("fk_actual_item_trip");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName(("actual_item_id"))
        .IsRequired();

      modelBuilder.Property(p => p.BudgetId)
        .HasColumnName(("budget_id"))
        .IsRequired();

      modelBuilder.Property(p => p.CategoryId)
        .HasColumnName(("category_id"))
        .IsRequired();

      modelBuilder.Property(p => p.TripId)
        .HasColumnName(("trip_id"))
        .IsRequired(false);

      modelBuilder.Property(p => p.PurchaseDate)
        .HasColumnName(("purchase_date"))
        .HasColumnType("date")
        .IsRequired();

      modelBuilder.Property(p => p.CurrencyCode)
        .HasColumnName(("currency"))
        .HasMaxLength(3)
        .IsRequired();

      modelBuilder.Property(p => p.Amount)
        .HasColumnName("amount")
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
        .HasOne(actualItem => actualItem.Budget)
        .WithMany(budget => budget.ActualItems)
        .HasForeignKey(actualItem => actualItem.BudgetId)
        .HasConstraintName("fk_actual_item_budget");

      modelBuilder
        .HasOne(actualItem => actualItem.Category)
        .WithMany(category => category.ActualItems)
        .HasForeignKey(actualItem => actualItem.CategoryId)
        .HasConstraintName("fk_actual_item_category");

      modelBuilder
        .HasOne(actualItem => actualItem.Trip)
        .WithMany(trip => trip.ActualItems)
        .HasForeignKey(actualItem => actualItem.TripId)
        .HasConstraintName("fk_actual_item_trip");
    }
  }
}