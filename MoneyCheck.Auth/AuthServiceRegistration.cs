using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MoneyCheck.Application.Contracts.Authentication;
using MoneyCheck.Application.Models.Auth;
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

    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
      var jwtTokenSettings = configuration.GetSection("JwtTokenSettings").Get<JwtTokenSettings>();

      if (jwtTokenSettings == null || jwtTokenSettings.Issuer == null || jwtTokenSettings.Audience == null || jwtTokenSettings.SecretKey == null)
        throw new Exception("JWT settings are missing or incomplete.");

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
            ValidIssuer = jwtTokenSettings.Issuer,
            ValidAudience = jwtTokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettings.SecretKey))
          };
        });
    }
  }
}