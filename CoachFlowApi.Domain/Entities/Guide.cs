namespace CoachFlowApi.Domain.Entities;

public class Guide
{
    public int Id { get; set; }
    public int CoachId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Categorie { get; set; } = string.Empty;
    public bool IsDubutant { get; set; }
    public string LienUrl { get; set; } = string.Empty;
    public int Prix { get; set; }

    // Relations
    public Coach? Coach { get; set; }
    public ICollection<Achat> Achats { get; set; } = new List<Achat>();
}
