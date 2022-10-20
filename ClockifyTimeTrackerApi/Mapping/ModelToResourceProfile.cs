using AutoMapper;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Extensions;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Student, StudentResource>()
            .ForMember(src => src.Semester,
            opt => opt.MapFrom(src => src.Semester.ToDescriptionString()));

            CreateMap<Receipt, ReceiptResource>();
        }
    }
}