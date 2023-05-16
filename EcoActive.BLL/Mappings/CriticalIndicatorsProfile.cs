using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.DAL.Entities;

namespace EcoActive.BLL.Mappings
{
    public class CriticalIndicatorsProfile : Profile
    {
        public CriticalIndicatorsProfile()
        {
            CreateMap<CriticalIndicatorsDTO, CriticalIndicators>()
            .ForMember(x => x.CreatedDate, o => o.MapFrom(s => s.Time))
            .ReverseMap();
            CreateMap<CriticalIndicators, CriticalIndicatorsCreateDTO>().ReverseMap();
        }
    }
}
