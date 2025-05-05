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

namespace WebAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly HotelsDbContext _context;
        private readonly IMapper _mapper;

        public HotelsController(HotelsDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            var mappedHotels = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(mappedHotels);  
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelsDetailsDto>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.Include(h => h.Country).FirstOrDefaultAsync(h => h.Id == id);

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

            var entity = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if(entity == null)
            {
                return NotFound();
            }

            _mapper.Map(hotel, entity);
            //_context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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
            var checkCountryId = await _context.Countries.AnyAsync(c => c.CountryId == hotelDto.CountryId);
            if (!checkCountryId)
            {
                return BadRequest($"countryId: {hotelDto.CountryId} doesn't exist");
            }

            var hotel = _mapper.Map<Hotel>(hotelDto); 
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
