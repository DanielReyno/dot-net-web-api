using System.ComponentModel.DataAnnotations;

namespace WebAPITesting.Dtos.Country
{
    public abstract class CountryBaseDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
