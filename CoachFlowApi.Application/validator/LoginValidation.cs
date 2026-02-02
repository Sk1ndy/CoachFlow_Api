using CoachFlowApi.Application.DTOs.Auth;
using FluentValidation;

namespace CoachFlowApi.Application.Validators;

public class LoginValidation : AbstractValidator<LoginDto>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}