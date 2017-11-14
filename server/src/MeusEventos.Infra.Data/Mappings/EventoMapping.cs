using MeusEventos.Domain.Eventos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusEventos.Infra.Data.Mappings
{
    public static class EventoMapping
    {
        public static void Map(this EntityTypeBuilder<Evento> builder)
        {
            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.DescricaoCurta)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.DescricaoLonga)
                .HasColumnType("varchar(max)");

            builder.Property(e => e.NomeEmpresa)
                .HasColumnType("varchar(150)")
                .IsRequired();
            
            builder.Ignore(e => e.Tags);            

            builder.ToTable("Eventos");

            builder.HasOne(e => e.Organizador)
                .WithMany(o => o.Eventos)
                .HasForeignKey(e => e.OrganizadorId);

            builder.HasOne(e => e.Categoria)
                .WithMany(e => e.Eventos)
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);
        }
    }
}
