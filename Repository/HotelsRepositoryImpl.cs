using Microsoft.EntityFrameworkCore;
using WebAPITesting.Data;
using WebAPITesting.IRepository;

namespace WebAPITesting.Repository
{
    public class HotelsRepositoryImpl: GenericRepositoryImpl<Hotel> , IHotelsRepository
    {
        private readonly HotelsDbContext _context;

        public HotelsRepositoryImpl(HotelsDbContext context) : base(context) 
        {
            this._context = context;
        }

        public async Task<bool> CountryExistAsync(int id)
        {
            return await _context.Countries.AnyAsync(c => c.CountryId == id);
        }

        public async Task<Hotel> GetHotelDetailsAsync(int id)
        {
            var entity = await _context.Hotels.Include(h => h.Country).FirstOrDefaultAsync(h => h.Id == id);
            if(entity == null)
            {
                return null;
            }

            return entity;
        }
    }
}
