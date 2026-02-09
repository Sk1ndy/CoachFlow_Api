namespace CoachFlowApi.Application.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string message = "Email ou mot de passe invalide.") 
        : base(message)
    {
    }
}
