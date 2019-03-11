using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
		private ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public ActionResult New()
		{
			var membershipTypes = _context.MembershipTypes.ToList();
			var vm = new CustomerFormViewModel
			{
				Customer = new Customer(),
				MembershipTypes = membershipTypes
			};
			return View("CustomerForm", vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Customer customer)
		{
			if (!ModelState.IsValid)
			{
				var vm = new CustomerFormViewModel
				{
					Customer = customer,
					MembershipTypes = _context.MembershipTypes.ToList()
				};

				return View("CustomerForm", vm);
			}

			if (customer.Id == 0)
			{
				_context.Customers.Add(customer);
			}
			else
			{
				var customerInDB = _context.Customers.Single(c => c.Id == customer.Id);

				customerInDB.Name = customer.Name;
				customerInDB.Birthday = customer.Birthday;
				customerInDB.MembershipTypeId = customer.MembershipTypeId;
				customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
			}

			_context.SaveChanges();
			return RedirectToAction("Customers", "Customers");
		}

		// GET: Customers
		[Route("customers")]
        public ActionResult Customers()
        {
			var vm = new CustomersViewModel
			{
				Customers = _context.Customers.Include(c => c.MembershipType).ToList()
			};
            return View(vm);
        }

		[Route("customers/details/{id}")]
		public ActionResult Details(int id)
		{
			var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			return View(customer);
		}

		public ActionResult Edit(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
			if (customer == null)
				return HttpNotFound();

			var vm = new CustomerFormViewModel
			{
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};

			return View("CustomerForm", vm);
		}
	}
}