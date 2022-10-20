using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;

namespace ClockifyTimeTrackerBE.Persistence.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IAppDbContext _context;
        protected IMongoCollection<TEntity> _dbSet;

        protected BaseRepository(IAppDbContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity obj)
        {
            ConfigDbSet();
            _context.AddCommand(() => _dbSet.InsertOneAsync(obj));
        }

        protected void ConfigDbSet()
        {
            _dbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            ConfigDbSet();
            var data = await _dbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            ConfigDbSet();
            var data = await _dbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public async Task<TEntity> GetByPropertyName(string propertyName, string value)
        {
            ConfigDbSet();
            var data = await _dbSet.FindAsync(Builders<TEntity>.Filter.Eq(propertyName, value));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync()
        {
            ConfigDbSet();
            var all = await _dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual void Update(TEntity obj)
        {
            ConfigDbSet();
            _context.AddCommand(() => _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj));
        }

        public virtual void Remove(int id)
        {
            ConfigDbSet();
            _context.AddCommand(() => _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public void RemoveAll()
        {
            ConfigDbSet();
            _context.AddCommand(() => _dbSet.DeleteManyAsync(new BsonDocument()));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}