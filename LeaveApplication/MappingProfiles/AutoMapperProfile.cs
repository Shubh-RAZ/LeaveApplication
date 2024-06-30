using AutoMapper;
using LeaveApplication.Data;
using LeaveApplication.Models.LeaveTypes;

namespace LeaveApplication.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public  AutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            CreateMap<LeaveTypeCreateOnlyVM, LeaveType>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
        }

    }
}
