using WebAPITesting.Data;

namespace WebAPITesting.IRepository
{
    //Heredamos todos los metodos requeridos de la interfaz IGenericRepository
    //con el objetivo de aplicar otra capa de abstraccion que nos permite denifinir metodos exclusivos de
    //ICountriesRepository a parte de los de la clase padre.
    public interface ICountriesRepository :  IGenericRepository<Country>
    {
        Task<Country> GetCountryDetailsAsync(int id);
    }
}
