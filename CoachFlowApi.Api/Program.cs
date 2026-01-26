using CoachFlowApi.Application.Interfaces;
using CoachFlowApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CoachFlowDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICoachRepository, CoachRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || true )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); 
app.Run();