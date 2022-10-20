using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Domain.Services.Communication;

namespace ClockifyTimeTrackerBE.Domain.Services
{
    public interface IStudentService
    {
        Task<StudentResponse> FindAsync(int id);
        Task<StudentsResponse> ListAsync();

        Task<StudentResponse> AddAsync(Student student);

        Task<StudentResponse> UpdateAsync(Student student);

        Task<StudentResponse> DeleteAsync(int id);
        Task<StudentsResponse> DeleteAllAsync();
    }
}