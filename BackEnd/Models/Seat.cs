using System.Collections.Generic;

namespace BackEnd.Models
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int CinemaHallId { get; set; }
        public bool IsBooked { get; set; }
        public List<BookingSeat> BookingSeats { get; set; } // Relationship to bookings
    }
}
    