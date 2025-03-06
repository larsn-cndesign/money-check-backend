using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
  {
    public void Configure(EntityTypeBuilder<Currency> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("currency");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      modelBuilder.HasIndex(p => new { p.VersionId, p.Code })
        .IsUnique()
        .HasDatabaseName("uk_currency__version_id__currency");

      // Columns
      modelBuilder.Property(p => p.Id)
        .HasColumnName(("currency_id"))
        .IsRequired();

      modelBuilder.Property(p => p.VersionId)
        .HasColumnName(("version_id"))
        .IsRequired();

      modelBuilder.Property(p => p.Code)
        .HasColumnName("currency")
        .HasMaxLength(3)
        .IsRequired();

      modelBuilder.Property(p => p.BudgetRate)
        .HasColumnName("budget_rate")
        .HasColumnType("decimal(9, 4)")
        .IsRequired();

      modelBuilder.Property(p => p.AverageRate)
        .HasColumnName("average_rate")
        .HasColumnType("decimal(9, 4)")
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
        .HasOne(currency => currency.BudgetVersion)
        .WithMany(budgetVersion => budgetVersion.Currencies)
        .HasForeignKey(currency => currency.VersionId)
        .HasConstraintName("fk_currency_version");
    }
  }
}