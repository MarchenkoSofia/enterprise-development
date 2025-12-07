using BikeRental.Application;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Model;
using BikeRental.Application.Contracts.Rent;
using BikeRental.Application.Contracts.Renter;
using BikeRental.Application.Service;
using BikeRental.Domain;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.EfCore;
using BikeRental.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
// removed: using BikeRental.Application.Service; // <-- add correct namespace if needed

var builder = WebApplication.CreateBuilder(args);

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new BikeRentalProfile());
});

// Application Services
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IApplicationService<BikeDto, BikeCreateUpdateDto, int>, BikeService>();

builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IApplicationService<ModelDto, ModelCreateUpdateDto, int>, ModelService>();

builder.Services.AddScoped<IRenterService, RenterService>();
builder.Services.AddScoped<IApplicationService<RenterDto, RenterCreateUpdateDto, int>, RenterService>();

builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IApplicationService<RentDto, RentCreateUpdateDto, int>, RentService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

// EF Core Repositories
builder.Services.AddScoped<IRepository<Bike, int>, BikeEfCoreRepository>();
builder.Services.AddScoped<IRepository<Model, int>, ModelEfCoreRepository>();
builder.Services.AddScoped<IRepository<Renter, int>, RenterEfCoreRepository>();
builder.Services.AddScoped<IRepository<Rent, int>, RentEfCoreRepository>();

// DbContext (SQL Server)
builder.Services.AddDbContext<BikeRentalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
    options.UseLazyLoadingProxies();
});

// Controllers + JSON settings
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger (single doc, grouping by controller -> tag)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "BikeRental API", Version = "v1" });

    // Group actions by controller name and map to our desired headings
    options.TagActionsBy(api =>
    {
        var controller = api.ActionDescriptor.RouteValues["controller"]?.ToLower() ?? "";

        return controller switch
        {
            "bike" or "bikes" => new[] { "Bikes" },
            "model" or "models" => new[] { "Models" },
            "renter" or "renters" => new[] { "Renters" },
            "rent" or "rents" => new[] { "Rents" },
            _ => new[] { api.ActionDescriptor.RouteValues["controller"] ?? "Other" }
        };
    });

    // XML docs (optional — files must exist)
    var basePath = AppContext.BaseDirectory;
    var xmlFiles = new[]
    {
        "BikeRental.Api.xml",
        "BikeRental.Domain.xml",
        "BikeRental.Application.xml",
        "BikeRental.Application.Contracts.xml"
    };

    foreach (var xml in xmlFiles)
    {
        var xmlPath = Path.Combine(basePath, xml);
        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Database Migration in Development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BikeRentalDbContext>();
    db.Database.Migrate();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikeRent API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
