using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using AutoMapper;
using ConcesionariaBackend.Data;
using ConcesionariaBackend.Mapping;
using ConcesionariaBackend.Repositories;
using ConcesionariaBackend.Services;
using ConcesionariaBackend.Validations;

var builder = WebApplication.CreateBuilder(args);

// 1. Conexion a la base de datos MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .LogTo(Console.WriteLine, LogLevel.Information) // Para ver consultas en consola
);

// 2. AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 3. FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ClienteDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VentaDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VehiculoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ServicioPostVentaDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<FacturaDTOValidator>();

// 4. Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IServicioPostVentaRepository, ServicioPostVentaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();

// 5. Servicios (clases concretas sin interfaces)
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<ServicioPostVentaService>();
builder.Services.AddScoped<FacturaService>();

// 6. Controladores
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

