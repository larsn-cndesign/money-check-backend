using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Persistance.Contexts;
using MoneyCheck.Persistance.Repositories;

namespace MoneyCheck.Persistance
{
  public static class PersistenceServiceRegistration
  {
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
    {
      var connectionString = config["ConnectionString"] ?? throw new InvalidOperationException("Could not get connection string");
      var sqlVersion = config["MySqlVersion"] ?? throw new InvalidOperationException("Could not get MySQL Server version");
      var version = new MySqlServerVersion(new Version(sqlVersion));

      services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(connectionString, version));

      services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

      // Add custom repositories here
      services.AddScoped<ISharedRepository, SharedRepository>();
      services.AddScoped<ITripRepository, TripRepository>();
      services.AddScoped<IPersonRepository, PersonRepository>();
      services.AddScoped<IBudgetRepository, BudgetRepository>();
      services.AddScoped<IUnitRepository, UnitRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<IBudgetYearRepository, BudgetYearRepository>();
      services.AddScoped<IBudgetVersionRepository, BudgetVersionRepository>();
      services.AddScoped<ICurrencyRepository, CurrencyRepository>();
      services.AddScoped<IBudgetItemRepository, BudgetItemRepository>();
      services.AddScoped<IActualItemRepository, ActualItemRepository>();
      services.AddScoped<IVarianceItemRepository, VarianceItemRepository>();

      return services;
    }
  }
}