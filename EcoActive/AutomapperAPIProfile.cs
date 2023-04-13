using AutoMapper;
using EcoActive.API.Models;
using EcoActive.BLL.DataTransferObjects;

namespace EcoActive.API
{
    public class AutomapperAPIProfile : Profile
    {
        public AutomapperAPIProfile()
        {
            CreateMap<EmployeeDTO, EmployeeViewModel>().ReverseMap();
            CreateMap<EmployeeCreateDTO, EmployeeCreateViewModel>().ReverseMap();
            CreateMap<EmployeeUpdateDTO, EmployeeUpdateViewModel>().ReverseMap();

            CreateMap<FactoryDTO, FactoryViewModel>().ReverseMap();
            CreateMap<FactoryCreateDTO, FactoryCreateViewModel>().ReverseMap();
            CreateMap<FactoryUpdateDTO, FactoryUpdateViewModel>().ReverseMap();

            CreateMap<FactoryAdministratorDTO, FactoryAdministratorViewModel>().ReverseMap();
            CreateMap<FactoryAdministratorCreateDTO, FactoryAdministratorCreateViewModel>().ReverseMap();
            CreateMap<FactoryAdministratorUpdateDTO, FactoryAdministratorUpdateViewModel>().ReverseMap();

            CreateMap<ActivistDTO, ActivistViewModel>().ReverseMap();
            CreateMap<ActivistCreateDTO, ActivistCreateViewModel>().ReverseMap();
            CreateMap<ActivistUpdateDTO, ActivistUpdateViewModel>().ReverseMap();

            CreateMap<LoginRequestDTO, LoginRequestViewModel>().ReverseMap();
            CreateMap<LoginResultDTO, LoginResultViewModel>().ReverseMap();

            CreateMap<ProfileDTO, ProfileViewModel>().ReverseMap();
        }
    }
}
