using System.Collections.Generic;

namespace BackEnd.DTO.Booking
{
    public class CreateBookingDTO
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public List<int> SeatIds { get; set; }
        public string Note { get; set; }
    }
}
