using AutoMapper;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Identity;

namespace EcoActive.DAL.Infrastructure
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<User, ApplicationUser>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone));
        }
    }
}
