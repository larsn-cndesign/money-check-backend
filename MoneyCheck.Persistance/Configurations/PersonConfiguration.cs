using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Persistance.Configurations
{
  public class PersonConfiguration : IEntityTypeConfiguration<Person>
  {
    public void Configure(EntityTypeBuilder<Person> modelBuilder)
    {
      // Table
      modelBuilder.ToTable("person");

      // Keys
      modelBuilder.HasKey(p => p.Id);

      // Columns
      modelBuilder.HasIndex(p => p.PersonName)
        .IsUnique()
        .HasDatabaseName("uk_person__person_name");

      modelBuilder.Property(p => p.Id)
        .HasColumnName("person_id")
        .IsRequired();

      modelBuilder.Property(p => p.PersonName)
        .HasColumnName("person_name")
        .HasMaxLength(50)
        .IsRequired();

      modelBuilder.Property(p => p.Email)
        .HasColumnName("email")
        .HasMaxLength(50)
        .IsRequired();

      modelBuilder.Property(p => p.HashedPassword)
        .HasColumnName("hashed_password")
        .HasMaxLength(100)
        .IsRequired();

      modelBuilder.Property(p => p.PasswordSalt)
        .HasColumnName("password_salt")
        .HasMaxLength(50)
        .IsRequired();

      modelBuilder.Property(p => p.IsAdmin)
        .HasColumnName("is_admin")
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