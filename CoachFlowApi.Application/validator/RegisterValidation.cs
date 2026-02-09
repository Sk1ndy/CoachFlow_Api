using CoachFlowApi.Application.DTOs.Auth;
using FluentValidation;

namespace CoachFlowApi.Application.Validators;

public class RegisterValidation : AbstractValidator<RegisterDto>
{
    public RegisterValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Nom).NotEmpty();
        RuleFor(x => x.Prenom).NotEmpty();
        RuleFor(x => x.Role).NotEmpty();
    
    }
}