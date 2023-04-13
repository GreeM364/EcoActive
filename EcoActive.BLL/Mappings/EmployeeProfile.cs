using AutoMapper;
using EcoActive.BLL.DataTransferObjects;
using EcoActive.DAL.Entities;

namespace EcoActive.BLL.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
            .ForMember(x => x.BirthDate, o => o.MapFrom(s => s.User.BirthDate))
            .ForMember(x => x.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(x => x.Phone, o => o.MapFrom(s => s.User.Phone))
            .ForMember(x => x.FirstName, o => o.MapFrom(s => s.User.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(s => s.User.LastName))
            .ForMember(x => x.Patronymic, o => o.MapFrom(s => s.User.Patronymic))
            .ReverseMap();


            CreateMap<EmployeeCreateDTO, Employee>()
            .ForMember(x => x.User, o => o.MapFrom(s => s))
            .ReverseMap();

            CreateMap<EmployeeCreateDTO, User>().ReverseMap();


            CreateMap<EmployeeUpdateDTO, Employee>()
            .ForMember(x => x.User, o => o.MapFrom(s => s));

            CreateMap<EmployeeUpdateDTO, User>().ReverseMap();
        }
    }
}
