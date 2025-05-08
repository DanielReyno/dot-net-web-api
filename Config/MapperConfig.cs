using AutoMapper;
using WebAPITesting.Data;
using WebAPITesting.Dtos.Country;
using WebAPITesting.Dtos.Hotels;
using WebAPITesting.Dtos.User;

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

            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();
            CreateMap<Hotel, GetHotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, HotelsDetailsDto>().ReverseMap();

            CreateMap<UserAccount, UserAccountDto>().ReverseMap();
        }
    }
}
