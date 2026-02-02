namespace CoachFlowApi.Application.DTOs.Auth;
public record RegisterDto(string Email, string Password, string Role, string Nom, string Prenom);