using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClockifyTimeTrackerBE.Domain.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(int id);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> ListAsync();
        void Update(TEntity obj);
        void Remove(int id);
        void RemoveAll();
    }
}
