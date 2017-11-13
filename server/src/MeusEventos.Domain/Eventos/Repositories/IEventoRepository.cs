using MeusEventos.Domain.Core.Data;
using System;
using System.Collections.Generic;

namespace MeusEventos.Domain.Eventos.Repositories
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId);
        Endereco ObterEnderecoPorId(Guid id);
        void AdicionarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);
        IEnumerable<Categoria> ObterCategorias();
    }
}