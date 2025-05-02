using WebAPITesting.Dtos.Hotels;

namespace WebAPITesting.Dtos.Country
{
    public class CountryDetailsDto
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<HotelsDetailsDto> Hotels {  get; set; }

    }

    
}
