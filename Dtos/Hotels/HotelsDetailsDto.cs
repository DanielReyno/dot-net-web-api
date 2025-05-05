using WebAPITesting.Dtos.Country;

namespace WebAPITesting.Dtos.Hotels
{
    public class HotelsDetailsDto : HotelBaseDto
    {
        public int Id { get; set; }
        public GetCountryDto Country { get; set; }
    }
}
