using CoachFlowApi.Application.DTOs.Auth;
using CoachFlowApi.Application.Interfaces.Security;
using CoachFlowApi.Domain.Interfaces.Repositories;
using FluentValidation;

namespace CoachFlowApi.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher; 
    private readonly IValidator<LoginDto> _validator;

    public LoginUseCase(
        IUserRepository userRepository, 
        IJwtProvider jwtProvider, 
        IPasswordHasher passwordHasher, 
        IValidator<LoginDto> validator)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<AuthResponseDto> Execute(LoginDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid) 
            throw new ValidationException(validationResult.Errors);

        var user = await _userRepository.GetByEmailAsync(dto.Email);
        
        if (user is null || !_passwordHasher.Verify(dto.Password, user.MotDePasse))
        {
            throw new Exception("Email ou mot de passe invalide.");
        }

        var token = _jwtProvider.Generate(user);
        
        return new AuthResponseDto(user.Id, user.Courriel, token);
    }
}