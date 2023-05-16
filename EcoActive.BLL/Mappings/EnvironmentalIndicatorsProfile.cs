using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.DAL.Entities;

namespace EcoActive.BLL.Mappings
{
    public class EnvironmentalIndicatorsProfile : Profile
    {
        public EnvironmentalIndicatorsProfile()
        {
            CreateMap<EnvironmentalIndicatorsDTO, EnvironmentalIndicators>()
            .ForMember(x => x.CreatedDate, o => o.MapFrom(s => s.Time))
            .ReverseMap();
            CreateMap<EnvironmentalIndicators, EnvironmentalIndicatorsCreateDTO>().ReverseMap();
        }
    }
}
