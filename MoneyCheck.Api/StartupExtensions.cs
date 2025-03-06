﻿using MoneyCheck.Api.Middleware;
using MoneyCheck.Application;
using MoneyCheck.Auth;
using MoneyCheck.Infrastructure;
using MoneyCheck.Persistance;

namespace MoneyCheck.Api
{
  public static class StartupExtensions
  {
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
      builder.Services.AddApplicationServices();
      builder.Services.AddInfrastructureServices(builder.Configuration);
      builder.Services.AddPersistenceServices(builder.Configuration);
      builder.Services.AddAuthServices(builder.Configuration);
      builder.Services.ConfigureJwtAuthentication();

      //builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

      //builder.Services.AddHttpContextAccessor();

      builder.Services.AddControllers();

      //builder.Services.AddCors(
      //    options => options.AddPolicy(
      //        "open",
      //        policy => policy.WithOrigins([builder.Configuration["ApiUrl"] ?? "https://localhost:7081",
      //                  builder.Configuration["BlazorUrl"] ?? "https://localhost:7080"])
      //.AllowAnyMethod()
      //.SetIsOriginAllowed(pol => true)
      //.AllowAnyHeader()
      //.AllowCredentials()));

      builder.Services.AddCors(
          options => options.AddPolicy(
           "CorsPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

      //builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
      //app.MapIdentityApi<ApplicationUser>();

      //app.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
      //{
      //  await signInManager.SignOutAsync();
      //  return TypedResults.Ok();
      //});

      app.UseCors("open");

      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseCustomExceptionHandler();

      app.UseHttpsRedirection();
      app.MapControllers();

      return app;
    }
  }
}