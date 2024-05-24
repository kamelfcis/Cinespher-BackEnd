using System.Collections.Generic;

namespace BackEnd.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public int? Duration { get; set; }
        public float? Rating { get; set; } 
        public string? MoviePictureUrl { get; set; }
        public List<Show> Shows { get; set; }
    }


}
