﻿using MeusEventos.Domain.Eventos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusEventos.Infra.Data.Mappings
{
    public static class EnderecoMapping
    {
        public static void Map(this EntityTypeBuilder<Endereco> builder)
        {

            builder.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnType("varchar(8)");

            builder.Property(e => e.Complemento)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.HasOne(c => c.Evento)
                .WithOne(c => c.Endereco)
                .HasForeignKey<Endereco>(c => c.EventoId)
                .IsRequired(false);

            builder.ToTable("Enderecos");
        }
    }
}