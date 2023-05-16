using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.IoT.Models;

namespace EcoActive.IoT
{
    public class AutomapperIoTProfile : Profile
    {
        public AutomapperIoTProfile()
        {
            CreateMap<EnvironmentalIndicatorsCreateDTO, AverageEnvironmentalIndicators>().ReverseMap();
            CreateMap<CriticalIndicatorsCreateDTO, CriticalEnvironmentalIndicators>().ReverseMap();
        }
    }
}
