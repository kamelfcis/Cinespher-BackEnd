using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    public partial class CinespherContext : DbContext
    {
        
        public CinespherContext(DbContextOptions options) : base(options)
        {
        }
        
         
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Show> Shows { get; set; } 
        public DbSet<Booking> Bookings { get; set; }





    }

}
