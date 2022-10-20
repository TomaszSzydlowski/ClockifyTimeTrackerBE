using AutoMapper;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Mapping
{
    public class SaveUserRegisterResourceToLoginUserResource : Profile
    {
        public SaveUserRegisterResourceToLoginUserResource()
        {
            CreateMap<SaveUserRegisterResource, LoginUserResource>();
        }
    }
}