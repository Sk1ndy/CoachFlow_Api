using CoachFlowApi.Application.DTOs.Auth;
using CoachFlowApi.Application.Interfaces.Security;
using CoachFlowApi.Domain.Entities;
using CoachFlowApi.Domain.Interfaces.Repositories;
using FluentValidation;

namespace CoachFlowApi.Application.UseCases.Auth;

public class RegisterUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IValidator<RegisterDto> _validator;

    public RegisterUseCase(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IValidator<RegisterDto> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _validator = validator;
    }

    public async Task<AuthResponseDto> Execute(RegisterDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid) 
        if (await _userRepository.EmailExistsAsync(dto.Email))
        {
            throw new Exception("Un utilisateur avec cet email existe déjà.");
        }
        
        var hashedPassword = _passwordHasher.Hash(dto.Password);
        
        var newUser = new User
        {
            Courriel = dto.Email,
            MotDePasse = hashedPassword,
            Nom = dto.Nom,
            Role = dto.Role,
            Balance = 0
        };
        
        await _userRepository.AddAsync(newUser);
        
        var token = _jwtProvider.Generate(newUser);
        
        return new AuthResponseDto(newUser.Id, newUser.Courriel, token);
        
    }
}