using CoachFlowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachFlowApi.Infrastructure.Persistence;

public class CoachFlowDbContext : DbContext
{
    public CoachFlowDbContext(DbContextOptions<CoachFlowDbContext> options) : base(options)
    {
    }

    public DbSet<Coach> Coaches { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Guide> Guides { get; set; }
    public DbSet<Achat> Achats { get; set; }
}
