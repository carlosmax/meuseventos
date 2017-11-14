using MeusEventos.Domain.Organizadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusEventos.Infra.Data.Mappings
{
    public static class OrganizadorMapping
    {
        public static void Map(this EntityTypeBuilder<Organizador> builder)
        {
            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(e => e.CPF)
               .HasColumnType("varchar(11)")
               .HasMaxLength(11)
               .IsRequired();
            
            builder.ToTable("Organizadores");
        }
    }
}
