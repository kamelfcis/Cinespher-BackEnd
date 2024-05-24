namespace BackEnd.DTO.Booking
{
    public class SeatDTO
    {
        public int SeatId { get; set; }
        public int CinemaHallId { get; set; }
        public bool IsBooked { get; set; }
    }
}
