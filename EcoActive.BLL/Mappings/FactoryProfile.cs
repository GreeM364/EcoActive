using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.DAL.Entities;

namespace EcoActive.BLL.Mappings
{
    public class FactoryProfile : Profile
    {
        public FactoryProfile()
        {
            CreateMap<Factory, FactoryDTO>()
                .ForMember(dest => dest.EmployeesCount, opt => opt.MapFrom(src => src.Employees.Count()))
                .ForMember(dest => dest.FactoryAdminsCount, opt => opt.MapFrom(src => src.FactoryAdmins.Count()))
                .ReverseMap();
            CreateMap<Factory, FactoryCreateDTO>().ReverseMap();
            CreateMap<Factory, FactoryUpdateDTO>().ReverseMap();
        }
    }
}
