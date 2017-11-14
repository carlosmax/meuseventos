using MeusEventos.Domain.Core.Data;
using MeusEventos.Domain.Eventos;
using MeusEventos.Domain.Organizadores;
using MeusEventos.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MeusEventos.Infra.Data.Contexts
{
    public class MeusEventosContext : ContextBase
    {
        public MeusEventosContext() : base() { }

        public MeusEventosContext(DbContextOptions<MeusEventosContext> options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }

        public override string ConnectionString => Configuration.GetConnectionString("DefaultConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>().Map();
            modelBuilder.Entity<Organizador>().Map();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}

