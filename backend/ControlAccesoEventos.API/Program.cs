using ControlAccesoEventos.Application.Interfaces;
using ControlAccesoEventos.Application.Services;
using ControlAccesoEventos.Domain.Interfaces;
using ControlAccesoEventos.Infrastructure.Data;
using ControlAccesoEventos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repositories and services
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IInvitadoService, InvitadoService>();
builder.Services.AddScoped<IEntradaService, EntradaService>();
builder.Services.AddScoped<IValidacionService, ValidacionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
