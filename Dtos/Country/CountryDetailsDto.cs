using WebAPITesting.Dtos.Hotels;

namespace WebAPITesting.Dtos.Country
{
    public class CountryDetailsDto : CountryBaseDto
    {
        public int CountryId { get; set; }
        public List<GetHotelDto> Hotels {  get; set; }

    }

    
}
