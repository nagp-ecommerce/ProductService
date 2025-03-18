using Microsoft.EntityFrameworkCore;
using ProductService.Api.Controllers;
using ProductService.Application.Interfaces;
using ProductService.Application.Services;
using ProductService.Core.Entities;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repositories;
using SharedService.Lib.DI;
using SharedService.Lib.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SharedServicesContainer.AddSharedServices<ProductDbContext>(builder.Services, builder.Configuration);

builder.Services.AddScoped<IGenericRepository<Product>, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductService, ProductInfoService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ProductController>();
//builder.Services.AddScoped<CategoryController>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = null; // System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddSwaggerGen();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var appDbContext = services.GetRequiredService<ProductDbContext>();
        appDbContext.Database.Migrate();
        Console.WriteLine("Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
SharedServicesContainer.UseSharedPolicies(app);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
