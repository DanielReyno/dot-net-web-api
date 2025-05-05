namespace WebAPITesting.Dtos.Hotels
{
    public abstract class HotelBaseDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public int CountryId { get; set; }
    }
}
