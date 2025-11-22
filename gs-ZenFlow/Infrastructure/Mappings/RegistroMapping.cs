using gs_ZenFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gs_ZenFlow.Infrastructure.Mappings;

public class RegistroMapping : IEntityTypeConfiguration<Registro>
{
    public void Configure(EntityTypeBuilder<Registro> b)
    {
        b.ToTable("T_GS_REGISTRO");
        
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        b.Property(x => x.UsuarioId)
            .IsRequired();
        
        b.Property(x => x.NivelEstresse)
            .IsRequired();
        
        b.Property(x => x.Observacoes)
            .HasMaxLength(500);
        
        b.Property(x => x.Data)
            .IsRequired();
        
        b.Property(x => x.DataCriacao)
            .IsRequired();
        
        b.Property(x => x.DataAtualizacao);
        
        b.Property(r => r.Ativo)
            .IsRequired()
            .HasConversion<int>(
                v => v ? 1 : 0,
                v => v == 1);
        
        b.HasOne(r => r.Usuario)
            .WithMany(u => u.Registros)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}