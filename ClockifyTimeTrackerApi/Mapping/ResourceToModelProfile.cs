using System;
using AutoMapper;
using ClockifyTimeTrackerBE.Domain.Models;
using ClockifyTimeTrackerBE.Resources;

namespace ClockifyTimeTrackerBE.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveStudentResource, Student>()
            .ForMember(src => src.Semester,
            opt => opt.MapFrom(src => (ESemester)src.Semester));

            CreateMap<SaveReceiptResource, Receipt>()
                .ForMember(src => src.BoughtAt,
                    opt => opt.MapFrom(src => DateTime.Parse(src.BoughtAt)));
        }
    }
}