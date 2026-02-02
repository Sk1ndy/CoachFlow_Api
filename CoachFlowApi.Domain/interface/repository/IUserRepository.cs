using CoachFlowApi.Domain.Entities;

namespace CoachFlowApi.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task<bool> EmailExistsAsync(string email);
}