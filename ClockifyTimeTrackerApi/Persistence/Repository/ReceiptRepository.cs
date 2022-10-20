using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Repositories;
using ClockifyTimeTrackerBE.Resources;
using MongoDB.Bson;
using MongoDB.Driver;


namespace ClockifyTimeTrackerBE.Persistence.Repository
{
    public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(IAppDbContext context) : base(context)
        {
        }

        public new Receipt Add(Receipt obj)
        {
            obj.BlobId = Guid.NewGuid();
            base.Add(obj);
            return obj;
        }

        public async Task<List<Receipt>> Find(ReceiptFilters filters)
        {
            ConfigDbSet();
            var filterBuilder = Builders<Receipt>.Filter;
                
            var filter = filterBuilder.Eq(x=>x.UserId, filters.UserId) &
                         filterBuilder.Gte(x => x.BoughtAt, filters.Start) &
                         filterBuilder.Lte(x => x.BoughtAt, filters.End);

            if (filters.Shop is not null)
            {
                filter &= filterBuilder.Eq(x => x.Shop, filters.Shop);
            }
            
            if (filters.Type is not null)
            {
                filter &= filterBuilder.Eq(x => x.Type, filters.Type);
            }

            if (filters.Name is not null)
            {
                filter &= filterBuilder.Regex("Name", new BsonRegularExpression(filters.Name));
            }

            var searchResult = await _dbSet.FindAsync(filter);
            return searchResult.ToList();
        }
    }
}