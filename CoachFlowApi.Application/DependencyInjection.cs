using CoachFlowApi.Application.UseCases.Auth;
using CoachFlowApi.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CoachFlowApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginValidation>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<RegisterUseCase>();
        return services;
    }
}