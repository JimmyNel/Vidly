using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using VidlyExercice1.Dto;
using VidlyExercice1.ViewModels;

namespace VidlyExercice1.Controllers.API
{
    public class MoviesApiController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesApiController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/moviesApi
        [HttpGet]
        public IHttpActionResult GetMovies(string query = null)
        {
            // eager load customers with their membership type
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery
                                    .Where(m => m.NumberAvailable > 0)
                                    .Where(m => m.Name.Contains(query));

            var moviesDtos = moviesQuery
                                    .ToList()
                                    .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDtos);
        }

        // GET api/moviesApi/1
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/moviesApi
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.NumberAvailable = movieDto.NumberInStock;
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            // retourne l'objet créé
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        // PUT api/moviesApi/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieDb == null)
                return NotFound();

            movieDb.Name = movieDto.Name;
            movieDb.MovieGenreId = movieDto.MovieGenreId;
            movieDb.ReleaseDate = movieDto.ReleaseDate;
            movieDb.NumberInStock = movieDto.NumberInStock;

            movieDto.Id = movieDb.Id;

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        // DELETE api/moviesApi/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return BadRequest();

            _context.Movies.Remove(movie);
            _context.SaveChanges();
          
            return Ok(movie.Id);
        }
    }
}
