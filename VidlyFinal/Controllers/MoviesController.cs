using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using VidlyExercice1.ViewModels;

namespace VidlyExercice1.Controllers
{
    public class MoviesController : Controller
    {
        // DBContext to access the DB + initialize it in the constructor
        //private Vidly2Context _context;
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
            //_context = new Vidly2Context();
        }

        // Context is a disposable object => need to b disposed
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        [Route("Movies")]
        [HttpGet]
        public ActionResult MoviesList()
        {
            // Eager Loading with Include - default = Lazy
            #region without Jquery datatable rendering
            //var movies = _context.Movies.Include(m => m.Genre);
            //return View(movies);
            #endregion

            // Add condition for rendering the index page depending on user's role
            // User property of our controller gives us access to the current user
            if(!User.IsInRole(RoleName.CanManageMovies))
                return View("ReadOnlyMovieList");

            return View("MoviesList");
        }

        // GET : Movies/Details/{id}
        [Route("Movies/Details/{id}")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            // error handling for out of index ids
            if (movie == null)
                return HttpNotFound();

            return View("Details", movie);
        }

        // Add Authorize attribute to the MovieForm to avoid direct access through URL for unauthorized users
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpGet]
        // Get Movie/New Form
        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = _context.MovieGenres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        // Get Movie/Edit Form
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.MovieGenres.ToList()
                
            };

            return View("MovieForm", viewModel);
        }

        // Post form
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        [ValidateAntiForgeryToken]  // Prevent Cross Site Request Forgery (CSRF) Attacks by generating and comparing Token automatically
        public ActionResult Save(Movie movie)
        {
            // check if the form fields are valid according to the Entity Model (type, annotation constraints...)
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.MovieGenres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            // post action
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            // put action
            else
            {
                var movieDB = _context.Movies.Single(m => m.Id == movie.Id);
                
                movieDB.Name = movie.Name;
                movieDB.ReleaseDate = movie.ReleaseDate;
                movieDB.MovieGenreId = movie.MovieGenreId;
                movieDB.NumberInStock = movie.NumberInStock;
                
            }

            _context.SaveChanges();

            return RedirectToAction("MoviesList", "Movies");
        }

    }
}