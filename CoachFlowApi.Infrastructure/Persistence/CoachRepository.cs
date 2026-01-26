using CoachFlowApi.Application.Interfaces;
using CoachFlowApi.Domain.Entities;

namespace CoachFlowApi.Infrastructure.Persistence;

public class CoachRepository : ICoachRepository
{
    public async Task<Coach?> GetByIdAsync(Guid id)
    {
        await Task.Delay(50); 
        return new Coach { Id = id, FullName = "Marc Entraîneur", Specialization = "Fitness" };
    }
}