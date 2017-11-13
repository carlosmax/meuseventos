using MeusEventos.Domain.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeusEventos.Domain.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
        void Adicionar(TEntity obj);
        TEntity ObterPorId(Guid id);
        TEntity ObterPorId(Guid id, string include);
        IEnumerable<TEntity> ObterTodos();
        void Atualizar(TEntity obj);
        void Remover(Guid id);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
