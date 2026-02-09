namespace CoachFlowApi.Application.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string message = "Un utilisateur avec cet email existe déjà.") 
        : base(message)
    {
    }
}
