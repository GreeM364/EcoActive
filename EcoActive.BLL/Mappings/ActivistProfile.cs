using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.DAL.Entities;

namespace EcoActive.BLL.Mappings
{
    public class ActivistProfile : Profile
    {
        public ActivistProfile()
        {
            CreateMap<Activist, ActivistDTO>()
            .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
            .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic))
            .ReverseMap();

            CreateMap<ActivistCreateDTO, Activist>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<ActivistCreateDTO, User>().ReverseMap();


            CreateMap<ActivistUpdateDTO, Activist>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<ActivistUpdateDTO, User>().ReverseMap();
        }
    }
}
