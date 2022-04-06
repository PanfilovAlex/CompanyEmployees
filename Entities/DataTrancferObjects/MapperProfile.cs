
using AutoMapper;
using Entities.Models;

namespace Entities.DataTrancferObjects
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress, option =>
                option.MapFrom(x => string.Join(' ', x.Country, x.Address)));

            CreateMap<Employee, EmployeeDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>();
            CreateMap<CompanyForUpdateDto, Company>();
        }
    }
}
