using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITesting.Config;
using WebAPITesting.Data;
using WebAPITesting.Dtos.Country;
using WebAPITesting.IRepository;

namespace WebAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _repository;
        private readonly IMapper _mapper;



        //Constructor que recibe la inyeccion del databaseContext.
        public CountriesController(ICountriesRepository repository,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _repository.GetAllAsync();
            var mapped = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(mapped);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetailsDto>> GetCountry(int id)
        {
            var country = await _repository.GetCountryDetailsAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            var mappedCountry = _mapper.Map<CountryDetailsDto>(country);
            return Ok(mappedCountry);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto country)
        {
            if (id != country.CountryId)
            {
                return BadRequest();
            }
            //_context.Entry(country).State = EntityState.Modified;

            var data = await _repository.GetAsync(id);

            if(data == null)
            {
                return NotFound($"The id: {id} cannot be found");
            }

            _mapper.Map(country,data);
            try
            {
                await _repository.UpdateAsync(data);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
        {
            //Al utilizar un dto nos protejemos de los ataques de overposting ya que con esto logramos limitar la 
            //cantidad de parametros que ingresan al controlador.
            
            Country countryOld = new Country() 
            {
                Name = countryDto.Name,
                ShortName = countryDto.ShortName,
            };

            var country = _mapper.Map<Country>(countryDto);

            await _repository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _repository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(country);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
