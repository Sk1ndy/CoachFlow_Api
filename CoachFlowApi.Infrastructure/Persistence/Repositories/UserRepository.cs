using CoachFlowApi.Domain.Entities;
using CoachFlowApi.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoachFlowApi.Infrastructure.Persistence.Repositories;

public class UserRepository(CoachFlowDbContext context) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email) 
        => await context.Users.FirstOrDefaultAsync(u => u.Courriel == email);

    public async Task<User> AddAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> EmailExistsAsync(string email)
        => await context.Users.AnyAsync(u => u.Courriel == email);
}