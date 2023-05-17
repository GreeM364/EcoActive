using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.DAL.Entities;

namespace EcoActive.BLL.Mappings
{
    public class FactoryProfile : Profile
    {
        public FactoryProfile()
        {
            CreateMap<Factory, FactoryDTO>().ReverseMap();
            CreateMap<Factory, FactoryCreateDTO>().ReverseMap();
            CreateMap<Factory, FactoryUpdateDTO>().ReverseMap();
        }
    }
}
