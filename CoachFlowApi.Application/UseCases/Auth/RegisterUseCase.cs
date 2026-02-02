using CoachFlowApi.Application.DTOs.Auth;
using CoachFlowApi.Application.Exceptions;
using CoachFlowApi.Application.Interfaces.Security;
using CoachFlowApi.Domain.Entities;
using CoachFlowApi.Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CoachFlowApi.Application.UseCases.Auth;

public class RegisterUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IValidator<RegisterDto> _validator;
    private readonly ILogger<RegisterUseCase> _logger;

    public RegisterUseCase(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IValidator<RegisterDto> validator,
        ILogger<RegisterUseCase> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _validator = validator;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Execute(RegisterDto dto)
    {
        _logger.LogInformation($"Tentative d'enregistrement pour l'email: {dto.Email}");

        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid) 
            throw new AuthValidationException(validationResult.Errors);

        if (await _userRepository.EmailExistsAsync(dto.Email))
        {
            _logger.LogWarning($"Tentative d'enregistrement avec un email déjà existant: {dto.Email}");
            throw new UserAlreadyExistsException();
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
        
        _logger.LogInformation($"Utilisateur enregistré avec succès: {newUser.Id}");
        
        return new AuthResponseDto(newUser.Id, newUser.Courriel, token);
    }
}