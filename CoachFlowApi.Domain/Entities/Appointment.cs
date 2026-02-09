namespace CoachFlowApi.Domain.Entities;

public class Appointment
{
    public int Id { get; set; }
    public int CoachId { get; set; }
    public int UserId { get; set; }
    public DateTime DateRdv { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Duree { get; set; }

    // Relations
    public Coach? Coach { get; set; }
    public User? User { get; set; }
}
