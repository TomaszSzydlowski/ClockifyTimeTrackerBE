using System;
using System.Threading.Tasks;

namespace ClockifyTimeTrackerBE.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
