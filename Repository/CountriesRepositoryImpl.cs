using Microsoft.EntityFrameworkCore;
using WebAPITesting.Data;
using WebAPITesting.IRepository;

namespace WebAPITesting.Repository
{
    public class CountriesRepositoryImpl : GenericRepositoryImpl<Country> , ICountriesRepository
    {
        private readonly HotelsDbContext _context;

        public CountriesRepositoryImpl(HotelsDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Country> GetCountryDetailsAsync(int id)
        {
            var country = await _context.Countries.Include(c => c.Hotels).FirstOrDefaultAsync(c => c.CountryId == id);
            if(country == null)
            {
                return null;
            }
            return country;
        }
    }
}
