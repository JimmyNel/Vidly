using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using VidlyExercice1.Dto;
using VidlyExercice1.ViewModels;

namespace VidlyExercice1.Controllers.API
{
    public class NewRentalAPIController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalAPIController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult GetRentals()
        {
            // eager load customers with their membership type
            return Ok(_context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .Include(r => r.Movie.Genre)
                .Include(r => r.Customer.MembershipType)
                .ToList());
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetRental(int id)
        {
            var rental = _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .Include(r => r.Movie.Genre)
                .Include(r => r.Customer.MembershipType)
                .SingleOrDefault(r => r.Id == id);

            if (rental == null)
                return NotFound();

            return Ok(rental);
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult CreateNewRentals([FromBody] NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // On utilise Single car il ne s'agit pas d'une API public. Par ailleurs, le customer sera selectionné à partir d'une liste de customers existants
            // Si un utilisateur malicieux tente de s'introduire avec un id customer inexistant, il aura en retour une erreur non explicite
            // SingleOrDefault a coupler avec if customer == null  return Bad Request("detail message")
            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(m => newRental.MoviesIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable > 0)
                {
                    movie.NumberAvailable--;
                    var rental = new Rental()
                    {
                        Customer = customer,
                        DateRented = DateTime.Now,
                        Movie = movie
                    };
                    _context.Rentals.Add(rental);
                }
                else
                    return BadRequest("Movie not available for rental");
            }

            _context.SaveChanges();

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var rentalDb = _context.Rentals
                .Include(r => r.Movie)
                .SingleOrDefault(r => r.Id == id);

            if (rentalDb == null)
                return NotFound();

            if (rentalDb.DateReturned == null)
            {
                rentalDb.DateReturned = DateTime.Now;
                rentalDb.Movie.NumberAvailable++;

                _context.SaveChanges();
                return Created(new Uri(Request.RequestUri + "/" + rentalDb.Id), rentalDb);
            }
            
            return BadRequest("Movie already returned");
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteRental(int id)
        {
            var rental = _context.Rentals.SingleOrDefault(r => r.Id == id);

            if (rental == null)
                return NotFound();

            _context.Rentals.Remove(rental);
            _context.SaveChanges();

            return Ok(rental.Id);
        }
    }
}