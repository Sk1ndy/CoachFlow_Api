using CoachFlowApi.Application.DTOs.Auth;
using CoachFlowApi.Domain.Interfaces.Repositories;

namespace CoachFlowApi.Application.UseCases.Auth;

public class GetUserInfoUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserInfoUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthResponseDto> Execute(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user is null)
        {
            throw new Exception("Utilisateur non trouvé.");
        }
        
        return new AuthResponseDto(user.Id, user.Courriel, null);
    }
}
