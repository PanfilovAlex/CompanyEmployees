
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
                option.MapFrom(x => string.Join(', ', x.Country, x.Address)));
        }
    }
}
