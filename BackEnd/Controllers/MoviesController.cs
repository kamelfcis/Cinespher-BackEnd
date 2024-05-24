using BackEnd.DTO.Movie;
using BackEnd.DTO.Show;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly CinespherContext cinespherContext;

        public MoviesController(CinespherContext cinespherContext)
        {
            this.cinespherContext = cinespherContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await cinespherContext.Movies
       .Include(m => m.Shows)
       .Select(m => new MovieDTO
       {
           MovieId = m.MovieId,
           Title = m.Title,
           Description = m.Description,
           Genre = m.Genre,
           Duration = m.Duration,
           Rating = m.Rating,
           MoviePictureUrl = m.MoviePictureUrl,
           Shows = m.Shows.Select(s => new ShowDTO
           {
               ShowId = s.ShowId,
               ShowDateTime = s.ShowDateTime.ToString("hh:mm tt"),
               Note = s.Note
           }).ToList()
       })
       .ToListAsync();
            return Ok(movies);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            var movie = await cinespherContext.Movies
                .Include(m => m.Shows)
                .Where(m => m.MovieId == id)
                .Select(m => new MovieDTO
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Description = m.Description,
                    Genre = m.Genre,
                    Duration = m.Duration,
                    Rating = m.Rating,
                    MoviePictureUrl = m.MoviePictureUrl,
                    Shows = m.Shows.Select(s => new ShowDTO
                    {
                        ShowId = s.ShowId,
                        ShowDateTime = s.ShowDateTime.ToString("hh:mm tt"),
                        Note = s.Note
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return Ok(movie); // Ensure to return an Ok response with the movie
        }


        [HttpPost]
        public async Task<ActionResult<MovieDTO>> CreateMovie([FromBody] CreateMovieDTO createMovieDTO)
        {
            var movie = new Movie
            {
                Title = createMovieDTO.Title,
                Description = createMovieDTO.Description,
                Genre = createMovieDTO.Genre,
                Duration = createMovieDTO.Duration,
                Rating = createMovieDTO.Rating,

               
            };
            cinespherContext.Movies.Add(movie);
            await cinespherContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieId }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieDTO updateMovieDTO)
        {
            var movie = await cinespherContext.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            // Map updated fields from DTO
            await cinespherContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await cinespherContext.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            cinespherContext.Movies.Remove(movie);
            await cinespherContext.SaveChangesAsync();

            return NoContent();
        }
    }

}

