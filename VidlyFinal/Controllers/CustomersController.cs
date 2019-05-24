using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using VidlyExercice1.ViewModels;

namespace VidlyExercice1.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // Context is a disposable object => need to be disposed
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Customers
        [HttpGet]
        public ActionResult Customers()
        {
            // Eager Loading with Include - default = Lazy
            #region without Jquery datatable rendering
            //var viewModel = _context.Customers.Include(c => c.MembershipType);       // differed execution without an extension method ToList()
            //return View(viewModel);
            #endregion

            return View();
        }

        // GET: Customers/New Form
        [HttpGet]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPut]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(m => m.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
        };

            return View("CustomerForm", viewModel);
        }

        // POST form
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent Cross Site Request Forgery (CSRF) Attacks by generating and comparing Token automatically
        public ActionResult Save(Customer customer)
        {
            // check if the form fields are valid according to the Entity Model (type, annotation constraints...)
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var custFromDB = _context.Customers.Single(c => c.Id == customer.Id);

                custFromDB.Name = customer.Name;
                custFromDB.Birthdate = customer.Birthdate;
                custFromDB.MembershipTypeId = customer.MembershipTypeId;
                custFromDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                // Security issue if all field must not be updated = someone could update anyway
                // TryUpdateModel(custFromDB);
                // alternative more secure but string[] is hard coded
                // TryUpdateModel(custFromDB, "", new string[] {"Name", "Birthdate"});

                // Use AutoMapper to do it faster
                // Mapper.Map(customer, custFromDB)

            }
            _context.SaveChanges();

            return RedirectToAction("Customers", "Customers");
        }
        
        // GET : Customers/Details/{id}
        [Route("Customers/Details/{id}")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            // Error handling for out of index ids
            if (customer == null)
                return HttpNotFound();

            return View("Details", customer);
        }
    }
}