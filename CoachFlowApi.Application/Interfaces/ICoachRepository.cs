using CoachFlowApi.Domain.Entities;

namespace CoachFlowApi.Application.Interfaces;

public interface ICoachRepository
{
    Task<Coach?> GetByIdAsync(Guid id);
}