using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyCheck.Application.Contracts.Infrastructure;
using MoneyCheck.Application.Models.Mail;
using MoneyCheck.Infrastructure.Mail;

namespace MoneyCheck.Infrastructure
{
  public static class InfrastructureServiceRegistration
  {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

      services.AddTransient<IEmailService, EmailService>();

      return services;
    }
  }
}