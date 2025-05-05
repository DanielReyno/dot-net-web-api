using WebAPITesting.Data;

namespace WebAPITesting.IRepository
{
    public interface IHotelsRepository : IGenericRepository<Hotel>
    {
        Task<Hotel> GetHotelDetailsAsync(int id);

        Task<bool> CountryExistAsync(int id);
    }
}
