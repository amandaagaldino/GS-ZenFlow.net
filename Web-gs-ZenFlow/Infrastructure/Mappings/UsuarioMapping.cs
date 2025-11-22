using Web_gs_ZenFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web_gs_ZenFlow.Infrastructure.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> b)
    {
        b.ToTable("T_GS_USUARIO");
        
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        b.Property(x => x.NomeCompleto)
            .HasMaxLength(100)
            .IsRequired();
        
        b.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();
        
        b.HasIndex(x => x.Email)
            .IsUnique();
        
        b.Property(x => x.Senha)
            .HasMaxLength(50)
            .IsRequired();
        
        b.Property(x => x.Cpf)
            .HasMaxLength(11)
            .IsRequired();
        
        b.HasIndex(x => x.Cpf)
            .IsUnique();
        
        b.Property(x => x.DataNascimento)
            .IsRequired();
        
        b.Property(x => x.IsGestor)
            .IsRequired()
            .HasConversion<int>(
                v => v ? 1 : 0,
                v => v == 1);
        
        b.Property(x => x.DataCriacao)
            .IsRequired();
        
        b.Property(x => x.DataAtualizacao);
        
        b.Property(u => u.Ativo)
            .IsRequired()
            .HasConversion<int>(
                v => v ? 1 : 0,
                v => v == 1);

        b.HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

