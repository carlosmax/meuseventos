using MeusEventos.Domain.Core.Data;
using MeusEventos.Domain.Organizadores;
using MeusEventos.Domain.Organizadores.Repositories;
using MeusEventos.Infra.Data.Contexts;

namespace MeusEventos.Infra.Data.Repositories
{
    public class OrganizadorRepository : RepositoryBase<Organizador>, IOrganizadorRepository
    {
        public OrganizadorRepository(MeusEventosContext context) 
            : base(context) { }
    }
}
