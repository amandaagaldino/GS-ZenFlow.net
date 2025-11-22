using gs_ZenFlow.Domain.Entities;
using gs_ZenFlow.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace gs_ZenFlow.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Registro> Registros { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new UsuarioMapping());
        modelBuilder.ApplyConfiguration(new RegistroMapping());
        
        base.OnModelCreating(modelBuilder);
    }
}