using AutoMapper;
using DiplomaAPI.DTOs;
using DiplomaAPI.Models;

namespace DiplomaAPI
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            //Mappings for creating and updating entities
            CreateMap<AgreedDto, Agreed>();
            CreateMap<ApprovedDto, Approved>();
            CreateMap<DocFileDto, DocFile>();
            CreateMap<UserDto, User>();
            CreateMap<UserRoleDto, UserRole>();

            //Mappings for returning entities
            CreateMap<Agreed, AgreedDto>();
            CreateMap<Approved, ApprovedDto>();
            CreateMap<DocFile, DocFileDto>();
            CreateMap<UserRole, UserRoleDto>();
        }
    }
}
