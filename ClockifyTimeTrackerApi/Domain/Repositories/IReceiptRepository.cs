using System.Collections.Generic;
using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Domain.Repositories
{
    public interface IReceiptRepository:IRepository<Receipt>
    {
        Task<List<Receipt>> Find(ReceiptFilters filters);
    }
}