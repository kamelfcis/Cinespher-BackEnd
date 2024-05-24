namespace BackEnd.DTO.Movie
{
    public class CreateMovieDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int? Duration { get; set; }
        public float? Rating { get; set; }
        public string MoviePictureUrl { get; set; }
    }
}
