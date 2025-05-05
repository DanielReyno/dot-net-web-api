using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITesting.Data;
using WebAPITesting.Dtos.Hotels;
using WebAPITesting.IRepository;

namespace WebAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _repository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository repository ,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await _repository.GetAllAsync();
            var mappedHotels = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(mappedHotels);  
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelsDetailsDto>> GetHotel(int id)
        {
            var hotel = await _repository.GetHotelDetailsAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var mappedHotel = _mapper.Map<HotelsDetailsDto>(hotel);

            return Ok(mappedHotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            var entity = await _repository.GetAsync(id);

            if(entity == null)
            {
                return NotFound();
            }

            _mapper.Map(hotel, entity);
            //_context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _repository.UpdateAsync(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
        {
            var checkCountryId = await _repository.CountryExistAsync(hotelDto.CountryId);
            if (!checkCountryId)
            {
                return BadRequest();
            }

            var hotel = _mapper.Map<Hotel>(hotelDto); 
            //await _context.Hotels.AddAsync(hotel);
            //await _context.SaveChangesAsync();
            await _repository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _repository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            //_context.Hotels.Remove(hotel);
            //await _context.SaveChangesAsync();

            await _repository.DeleteAsync(hotel);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
