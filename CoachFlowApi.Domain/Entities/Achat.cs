namespace CoachFlowApi.Domain.Entities;

public class Achat
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GuideId { get; set; }
    public DateTime DateAchat { get; set; }
    public int PrixAchat { get; set; }

    // Relations
    public User? User { get; set; }
    public Guide? Guide { get; set; }
}
