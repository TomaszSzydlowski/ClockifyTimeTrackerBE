using System.Threading.Tasks;
using ClockifyTimeTrackerBE.Domain.Services.Communication;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Domain.Services
{
    public interface IAuthService
    {
        Task<RegisterUserResponse> Register(SaveUserRegisterResource saveRegisterUserResource);
        Task<LoginUserResponse> Login(LoginUserResource loginUserResource);
    }
}