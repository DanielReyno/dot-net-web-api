using AutoMapper;
using WebAPITesting.Data;
using WebAPITesting.Dtos.Country;
using WebAPITesting.Dtos.Hotels;

namespace WebAPITesting.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDetailsDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();


            CreateMap<Hotel, HotelsDetailsDto>().ReverseMap();
        }
    }
}
