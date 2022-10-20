using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Repositories;

namespace ClockifyTimeTrackerBE.Persistence.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(IAppDbContext context) : base(context)
        {
        }
    }
}