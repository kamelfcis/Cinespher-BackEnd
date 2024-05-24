using BackEnd.DTO.Booking;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    { 
         
            private readonly CinespherContext _context;

            public BookingController(CinespherContext context)
            {
                _context = context;
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<BookingDTO>> GetBooking(int id)
            {
                var booking = await _context.Bookings
                    .Include(b => b.BookingSeats).ThenInclude(bs => bs.Seat)
                    .Where(b => b.BookingId == id)
                    .Select(b => new BookingDTO
                    {
                        BookingId = b.BookingId,
                        UserId = b.UserId,
                        ShowId = b.ShowId,
                        BookingTime = b.BookingTime,
                        Seats = b.BookingSeats.Select(bs => new SeatDTO
                        {
                            SeatId = bs.Seat.SeatId,
                            CinemaHallId = bs.Seat.CinemaHallId,
                            IsBooked = bs.Seat.IsBooked
                        }).ToList(),
                        Note = b.Note
                    })
                    .FirstOrDefaultAsync();

                if (booking == null)
                    return NotFound();

                return booking;
            }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO createBookingDTO)
        {
            // Start a transaction to ensure data consistency
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var seats = await _context.Seats
                                          .Where(s => createBookingDTO.SeatIds.Contains(s.SeatId) && !s.IsBooked)
                                          .ToListAsync();

                if (seats.Count != createBookingDTO.SeatIds.Count)
                {
                    return BadRequest("One or more seats are already booked or do not exist.");
                }

                // Update seats to booked
                foreach (var seat in seats)
                {
                    seat.IsBooked = true;
                }

                var booking = new Booking
                {
                    UserId = createBookingDTO.UserId,
                    ShowId = createBookingDTO.ShowId,
                    BookingTime = DateTime.Now,
                    Note = createBookingDTO.Note,
                    BookingSeats = seats.Select(seat => new BookingSeat { SeatId = seat.SeatId }).ToList()
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return Ok("Booked Successfully");
            }
            catch (Exception ex)
            {
                // Rollback transaction if something goes wrong
                await transaction.RollbackAsync();
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Additional methods (Update, Delete) can be implemented similarly
    }
}
 
