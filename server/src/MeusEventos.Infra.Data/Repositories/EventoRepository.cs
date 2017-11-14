using MeusEventos.Domain.Core.Data;
using MeusEventos.Domain.Eventos;
using MeusEventos.Domain.Eventos.Repositories;
using MeusEventos.Infra.Data.Contexts;
using System;
using System.Collections.Generic;

namespace MeusEventos.Infra.Data.Repositories
{
    public class EventoRepository : RepositoryBase<Evento>, IEventoRepository
    {
        public EventoRepository(MeusEventosContext context) 
            : base(context) { }

        public void AdicionarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> ObterCategorias()
        {
            throw new NotImplementedException();
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
        {
            throw new NotImplementedException();
        }
    }
}
