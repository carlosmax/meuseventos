using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MeusEventos.Domain.Core.Data
{
    public abstract class ContextBase : DbContext, IDisposable
    {
        public ContextBase() : base() { }

        public ContextBase(DbContextOptions options) : base(options) { }

        protected IConfiguration Configuration;

        public abstract string ConnectionString { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}