using AutoMapper;
using Entities.Configurations;
using Entities.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcommerceApi.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterationDto, User>();
        }
    }
}
