using AutoMapper;
using EcoActive.DAL.Entities;
using EcoActive.BLL.DataTransferObjects;


namespace EcoActive.BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ProfileDTO>().ReverseMap();
        }
    }
}
