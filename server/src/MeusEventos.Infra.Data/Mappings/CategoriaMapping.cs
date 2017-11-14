using MeusEventos.Domain.Eventos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusEventos.Infra.Data.Mappings
{
    public static class CategoriaMapping
    {
        public static void Map(this EntityTypeBuilder<Categoria> builder)
        {
            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();
            
            builder.ToTable("Categorias");
        }
    }
}