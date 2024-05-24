using BackEnd.DTO.Seat;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly CinespherContext _context;

        public SeatController(CinespherContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeats([FromBody] CreateSeatsDTO createSeatsDTO)
        {
            if (createSeatsDTO.NumberOfSeats <= 0)
            {
                return BadRequest("Number of seats must be positive.");
            }

            var seats = Enumerable.Range(1, createSeatsDTO.NumberOfSeats)
                                  .Select(_ => new Seat
                                  {
                                      CinemaHallId = createSeatsDTO.CinemaHallId,
                                      IsBooked = false
                                  })
                                  .ToList();

            _context.Seats.AddRange(seats);
            await _context.SaveChangesAsync();

            return Ok($"Successfully added {createSeatsDTO.NumberOfSeats} seats to hall {createSeatsDTO.CinemaHallId}.");
        }

        //// GET: api/Seat/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<SeatDTO>> GetSeat(int id)
        //{
        //    var seat = await _context.Seats.FindAsync(id);
        //    if (seat == null)
        //    {
        //        return NotFound();
        //    }

        //    return new SeatDTO
        //    {
        //        SeatId = seat.SeatId,
        //        CinemaHallId = seat.CinemaHallId,
        //        IsBooked = seat.IsBooked
        //    };
        //}
        [HttpGet("{id}")]
        public IActionResult GetCinemaHallBy(int id)
        {
            var seats =   _context.Seats.Where(x=>x.CinemaHallId==id);
            if (seats == null)
            {
                return NotFound();
            }

            return Ok( seats);

        }

    }
}
