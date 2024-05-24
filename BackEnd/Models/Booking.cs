using System.Collections.Generic;
using System;

namespace BackEnd.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ShowId { get; set; }
        public Show Show { get; set; }
        public DateTime BookingTime { get; set; }
        public List<BookingSeat> BookingSeats { get; set; } // Relationship to seats
        public  string Note { get; set; }
    }

}
