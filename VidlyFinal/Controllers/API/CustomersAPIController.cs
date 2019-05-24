using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using VidlyExercice1.Dto;
using VidlyExercice1.ViewModels;

namespace VidlyExercice1.Controllers.API
{
    public class CustomersApiController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersApiController()
        {
            _context = new ApplicationDbContext();
            //_context = new Vidly2Context();
        }

        #region using DTO + IHttpActionResult
        // GET /API/CustomersApi
        //[Route("api/customers")]
        public IHttpActionResult GetCustomers()
        {
            // eager load customers with their membership type
            return Ok(_context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>));
        }

        // GET /API/CustomersApi/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }


        // POST /API/CustomersApi
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customer);
        }

        // PUT /API/CustomersApi/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            // check if the customer in arg correspond to the model concerned Customer
            if (!ModelState.IsValid)
                return BadRequest();

            // Try to get this customer from context by Id
            var customerDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // check if the customer is null
            if (customerDb == null)
                return NotFound();

            // update context customer with customer in arg coming from the form
            //Mapper.Map(customerDto, customerDb);
            customerDb.Name = customerDto.Name;
            customerDb.Birthdate = customerDto.Birthdate;
            customerDb.MembershipTypeId = customerDto.MembershipTypeId;
            customerDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;

            customerDto.Id = customerDb.Id;

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }

        // DELETE /API/CustomersApi/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerDB);
            _context.SaveChanges();

            return Ok(customerDB.Id);
        }
        #endregion


        #region using DTO
        // GET /API/Customers
        //[Route("api/customers")]
        //public IEnumerable<CustomerDto> GetCustomers()
        //{
        //    return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>); ;
        //}


        // GET /API/Customers/1
        //public CustomerDto GetCustomer(int id)
        //{
        //    var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customer == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    return Mapper.Map<Customer, CustomerDto>(customer);
        //}


        // POST /API/Customers
        //[HttpPost]
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();

        //    customerDto.Id = customer.Id;

        //    return customerDto;
        //}

        // PUT /API/Customers/1
        //[HttpPut]
        //public CustomerDto UpdateCustomer(int id, CustomerDto customerDto)
        //{
        //    // check if the customer in arg correspond to the model concerned Customer
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    // Try to get this customer from context by Id
        //    var customerDb = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    // check if the customer is null
        //    if (customerDb == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    // update context customer with customer in arg coming from the form
        //    //Mapper.Map(customerDto, customerDb);
        //    customerDb.Name = customerDto.Name;
        //    customerDb.Birthdate = customerDto.Birthdate;
        //    customerDb.MembershipTypeId = customerDto.MembershipTypeId;
        //    customerDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;

        //    customerDto.Id = customerDb.Id;

        //    _context.SaveChanges();

        //    return customerDto;
        //}


        // DELETE /API/Customers/1
        //[HttpDelete]
        //public bool DeleteCustomer(int id)
        //{
        //    var customerDB = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customerDB == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    _context.Customers.Remove(customerDB);
        //    _context.SaveChanges();

        //    return true;
        //}
        #endregion


        #region No DTO API
        //// GET /API/Customers
        ////[Route("api/customers")]
        //public IEnumerable<Customer> GetCustomers()
        //{
        //    return _context.Customers.ToList(); ;
        //}


        //// GET /API/Customers/1
        //public Customer GetCustomer(int id)
        //{
        //    var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customer == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    return customer;
        //}


        // POST /API/Customers
        //[HttpPost]
        //public Customer CreateCustomer(Customer customer)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();

        //    return customer;
        //}

        // PUT /API/Customers/1
        //[HttpPut]
        //public Customer UpdateCustomer(int id, Customer customer)
        //{
        //    // check if the customer in arg correspond to the model concerned Customer
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    // Try to get this customer from context by Id
        //    var customerDB = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    // check if the customer is nul
        //    if (customerDB == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    // update context customer with customer in arg coming from the form
        //    customerDB.Name = customer.Name;
        //    customerDB.Birthdate = customer.Birthdate;
        //    customerDB.MembershipTypeId = customer.MembershipTypeId;
        //    customerDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

        //    _context.SaveChanges();

        //    return customerDB;
        //}


        //// DELETE /API/Customers/1
        //[HttpDelete]
        //public bool DeleteCustomer(int id)
        //{
        //    var customerDB = _context.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customerDB == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    _context.Customers.Remove(customerDB);
        //    _context.SaveChanges();

        //    return true;
        //}
        #endregion



    }
}
