using MeusEventos.Domain.Core.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeusEventos.Domain.Core.Data
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected ContextBase Db;
        protected DbSet<TEntity> DbSet;

        protected RepositoryBase(ContextBase context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
        
        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public virtual TEntity ObterPorId(Guid id, string include = "")
        {
            return DbSet.AsNoTracking().Include(include).FirstOrDefault(t => t.Id == id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(Db.ConnectionString);
        }
    }
}
