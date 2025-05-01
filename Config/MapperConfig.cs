using AutoMapper;
using WebAPITesting.Data;
using WebAPITesting.Dtos.Country;

namespace WebAPITesting.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
        }
    }
}
