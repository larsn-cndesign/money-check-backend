using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyCheck.Application.Contracts.Authentication;
using MoneyCheck.Application.Contracts.Infrastructure;
using MoneyCheck.Application.Models.Auth;
using MoneyCheck.Application.Models.Mail;
using MoneyCheck.Auth.Auth;
using System.Text;

namespace MoneyCheck.Auth
{
  public static class AuthServiceRegistration
  {
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));

      services.AddTransient<IAuthService, AuthService>();

      return services;
    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services)
    {
      var serviceProvider = services.BuildServiceProvider();
      var jwtTokenOptions = serviceProvider.GetRequiredService<IOptions<JwtTokenSettings>>();
      var jwtToken = jwtTokenOptions.Value;

      if (jwtToken?.Issuer == null || jwtToken?.Audience == null || jwtToken?.SecretKey == null)
        throw new Exception("Unknown error");

      services
        .AddAuthentication(opt =>
        {
          opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
        {
          opt.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtToken.Issuer,
            ValidAudience = jwtToken.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken.SecretKey))
          };
        });
    }
  }
}