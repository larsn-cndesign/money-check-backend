using Microsoft.AspNetCore.StaticFiles;
using MoneyCheck.Api.Middleware;
using MoneyCheck.Application;
using MoneyCheck.Auth;
using MoneyCheck.Infrastructure;
using MoneyCheck.Persistance;
using System.Text.Json.Serialization;

namespace MoneyCheck.Api
{
  public static class StartupExtensions
  {
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
      builder.Services.AddPersistenceServices(builder.Configuration);
      builder.Services.AddApplicationServices();
      builder.Services.AddInfrastructureServices(builder.Configuration);
      builder.Services.AddAuthServices(builder.Configuration);

      builder.Services.AddCors(
        options => options.AddPolicy(
          "CorsPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
      builder.Services.ConfigureJwtAuthentication(builder.Configuration);

      // ---------------------------------------------------------------------
      // When using frontend application as part of the deploy process
      builder.Services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      });

      if (builder.Environment.IsProduction())
      {
        builder.Services.AddSpaStaticFiles(configuration =>
        {
          configuration.RootPath = "frontend/browser";
        });
      }
      // ---------------------------------------------------------------------

      //builder.Services.AddEndpointsApiExplorer(); // Uncomment when using Swagger
      builder.Services.AddSwaggerGen();

      return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
      if (app.Environment.IsProduction())
      {
        app.UseHttpsRedirection();

        // ---------------------------------------------------------------------
        // When using frontend application as part of the deploy process
        app.UseStaticFiles(new StaticFileOptions
        {
          ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
          {
            { ".js", "application/javascript" }
          })
        });
        app.UseSpaStaticFiles();
        app.UseSpa(spa =>
        {
          spa.Options.SourcePath = "frontend/browser";
        });
        // ---------------------------------------------------------------------
      }

      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseCustomExceptionHandler();

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllers();

      return app;
    }
  }
}