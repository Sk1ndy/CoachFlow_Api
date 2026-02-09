using CoachFlowApi.Domain.Entities;

namespace CoachFlowApi.Application.Interfaces.Security;

public interface IJwtProvider
{
    string Generate(User user);
}