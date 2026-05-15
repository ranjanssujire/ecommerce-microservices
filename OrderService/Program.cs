using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Data;
using OrderService.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to container

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

// =========================
// 1. Controllers + Swagger
// =========================
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
// =========================
// 2. DB Context
// =========================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
// =========================
// 3. Dependency Injection (REPOSITORIES)
// =========================

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderService.Application.Services.OrderService>();

// =========================
// 4. AutoMapper / FluentValidation / MediatR (if used)
// =========================
// builder.Services.AddAutoMapper(...);
// builder.Services.AddFluentValidationAutoValidation();
// builder.Services.AddMediatR(...);

var app = builder.Build();

// =========================
// Middleware pipeline
// =========================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();