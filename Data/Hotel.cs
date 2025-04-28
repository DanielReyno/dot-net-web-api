namespace WebAPITesting.Data
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }

        public int CountryId {  get; set; }
        public Country? Country { get; set; }
    }
}
