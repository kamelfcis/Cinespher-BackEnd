using System.Collections.Generic;
using System;

namespace BackEnd.DTO.Booking
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public DateTime BookingTime { get; set; }
        public List<SeatDTO> Seats { get; set; }
        public string Note { get; set; }
    }
}
