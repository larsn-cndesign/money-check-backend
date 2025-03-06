using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class TripConfiguration : IEntityTypeConfiguration<Trip>
  {
    public void Configure(EntityTypeBuilder<Trip> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("trip");

      // Keys
      modelBuilder.HasKey(p => p.Id);
      modelBuilder.HasIndex(p => new { p.FromDate, p.ToDate, p.BudgetId })
        .IsUnique()
        .HasDatabaseName("uk_trip__budget_id__from_date__to_date");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName(("trip_id"))
        .IsRequired();

      modelBuilder.Property(p => p.BudgetId)
        .HasColumnName(("budget_id"))
        .IsRequired();

      modelBuilder.Property(p => p.FromDate)
        .HasColumnName(("from_date"))
        .HasColumnType("date")
        .IsRequired();

      modelBuilder.Property(p => p.ToDate)
        .HasColumnName(("to_date"))
        .HasColumnType("date")
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
        .HasOne(trip => trip.Budget)
        .WithMany(budget => budget.Trips)
        .HasForeignKey(trip => trip.BudgetId)
        .HasConstraintName("fk_trip_budget");
    }
  }
}