namespace CoachFlowApi.Domain.Entities;

public class Coach
{
    public Guid Id { get; init; }
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}