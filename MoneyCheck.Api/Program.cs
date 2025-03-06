using MoneyCheck.Api;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("MoneyCheck API starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
  .ReadFrom.Configuration(context.Configuration)
  .ReadFrom.Services(services)
  .Enrich.FromLogContext()
  .Filter.ByExcluding(e => e.Exception is FluentValidation.ValidationException)
  .WriteTo.Console(),
  true);

var app = builder
  .ConfigureServices()
  .ConfigurePipeline();

app.UseSerilogRequestLogging();

app.Run();