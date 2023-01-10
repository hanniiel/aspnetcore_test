using System;
using AutoMapper;
using test.Entities.DTO;
using test.Entities.Models;

namespace test.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<AssistanceForRegistrationDto, Assistance>()
                .ForMember(a => a.ScheduleID, opt => opt.MapFrom(x => x.IdSchedule))
                .ForMember(a => a.UserID, opt => opt.MapFrom(x => x.UserId))
                .ForMember(a => a.WorkplaceID, opt => opt.MapFrom(x => x.IdWorkplace));
        }
    }
}

