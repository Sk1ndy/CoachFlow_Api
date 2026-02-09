using FluentValidation.Results;

namespace CoachFlowApi.Application.Exceptions;

public class AuthValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; set; }

    public AuthValidationException(IEnumerable<ValidationFailure> errors) 
        : base("Erreur de validation")
    {
        Errors = errors;
    }
}
