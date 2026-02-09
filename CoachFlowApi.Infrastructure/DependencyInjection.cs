using CoachFlowApi.Application.Interfaces.Security;
using CoachFlowApi.Domain.Interfaces.Repositories;
using CoachFlowApi.Infrastructure.Persistence;
using CoachFlowApi.Infrastructure.Persistence.Repositories;
using CoachFlowApi.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoachFlowApi.Infrastructure.Security;

namespace CoachFlowApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DB Context
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CoachFlowDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Repositories & Services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}