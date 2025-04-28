using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using WebAPITesting.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        public static List<Hotel> hotels = new List<Hotel>()
        {
            new Hotel() { Id = 1 ,Name = "Cabana", Location = "Rep Dom", Price = 500},
            new Hotel() { Id = 2 ,Name = "La Caribena", Location = "Rep Dom", Price = 500},
            new Hotel() { Id = 3 ,Name = "Moonsvault", Location = "US", Price = 1000},
            new Hotel() { Id = 4 ,Name = "Monica Tail", Location = "Es", Price = 2000},
        };
        // GET: api/<HotelController>
        [HttpGet]
        public ActionResult<List<Hotel>> Get()
        {
            return Ok(hotels);
        }

        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotel = hotels.FirstOrDefault(hotel => hotel.Id == id);
            if(hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // POST api/<HotelController>
        [HttpPost]
        public ActionResult Post([FromBody] Hotel value)
        {
            var hotel = hotels.FirstOrDefault(hotel => hotel.Id == value.Id);
            if(hotel != null)
            {
                return BadRequest($"There is already an hotel with the id: {value.Id}");
            }

            hotels.Add(value);
            return CreatedAtAction(nameof(Get), hotel);
        }

        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel value)
        {
            var  hotel = hotels.FirstOrDefault(hotel => hotel.Id == id);
            if(hotel == null)
            {
                return NotFound($"There is no Hotel with the id= {id}");
            }
            hotel.Name = value.Name;
            hotel.Location = value.Location;
            hotel.Price = value.Price;

            return NoContent();
        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(hotel =>  id == hotel.Id);
            if(hotel == null)
            {
                return NotFound($"There is no Hotel with the id= {id}");
            }
            hotels.Remove(hotel);
            return NoContent();
        }
    }
}
