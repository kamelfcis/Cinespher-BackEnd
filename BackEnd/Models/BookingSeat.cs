﻿namespace BackEnd.Models
{
    public class BookingSeat
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
