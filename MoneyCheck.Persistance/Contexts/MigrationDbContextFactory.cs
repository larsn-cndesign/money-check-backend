using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MoneyCheck.Persistance.Contexts
{
  public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
  {
    public MigrationDbContext CreateDbContext(string[] args)
    {
      string path = Directory.GetCurrentDirectory();

      IConfigurationBuilder builder = new ConfigurationBuilder()
        .SetBasePath(path)
        .AddJsonFile("appsettings.Development.json");

      IConfigurationRoot config = builder.Build();

      if (config != null)
      {
        string? connectionString = config["ConnectionString"];

        if (string.IsNullOrWhiteSpace(connectionString))
        {
          throw new InvalidOperationException("Could not find connection string named 'ConnectionString'");
        }

        string? sqlVersion = config["MySqlVersion"];
        if (string.IsNullOrWhiteSpace(sqlVersion))
        {
          throw new InvalidOperationException("Could not find string named 'MySqlVersion'");
        }

        Version? version;

        try
        {
          version = new Version(sqlVersion);
        }
        catch (Exception ex)
        {
          throw new Exception("Invalid version format " + ex.Message);
        }

        var optionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(version));

        return new MigrationDbContext(optionsBuilder.Options);
      }
      else
      {
        throw new Exception("Coud not run migration");
      }
    }
  }
}