using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.CORE.Repository;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.CORE.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        Task<int> Commit();
        void RollBack();

    }
}
