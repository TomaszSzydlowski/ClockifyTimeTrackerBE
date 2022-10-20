using System;
using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Services.Communication;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Domain.Services
{
    public interface IReceiptService
    {
        Task<ReceiptResponse> FindAsync(Guid id);
        Task<ReceiptsResponse> FindAsync(ReceiptFilters filters);
        Task<ReceiptResponse> AddAsync(Receipt receipt);
    }
}