using Ecommerce_V1.Data;
using Ecommerce_V1.Interfaces;
using Ecommerce_V1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var con = builder.Configuration.GetConnectionString("ecommerceDB");
builder.Services.AddDbContext<EcommerceDbContext>(options => options.UseSqlServer(con));

/// Add services to the container.
builder.Services.AddScoped<IProduct, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
