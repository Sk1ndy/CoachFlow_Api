using CoachFlowApi.Application.Interfaces;
using CoachFlowApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

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