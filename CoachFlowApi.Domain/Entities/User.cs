namespace CoachFlowApi.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Courriel { get; set; } = string.Empty;
    public string MotDePasse { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int Balance { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Achat> Achats { get; set; } = new List<Achat>();
}
