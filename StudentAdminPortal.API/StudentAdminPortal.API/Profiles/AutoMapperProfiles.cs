using AutoMapper;
using StudentAdminPortal.API.DataModels;
using DataModels=StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student, Student>();

            CreateMap<DataModels.Gender, Gender>();

            CreateMap<DataModels.Address, Address>();
        }
    }
}
