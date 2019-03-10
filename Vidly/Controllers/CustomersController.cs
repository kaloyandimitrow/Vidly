using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
		[Route("customers")]
        public ActionResult Customers()
        {
			var vm = new CustomersViewModel
			{
				Customers = GenerateCustomers()
			};
            return View(vm);
        }

		[Route("customers/details/{id}")]
		public ActionResult Details(int id)
		{
			var customer = GenerateCustomers().Where(c => c.Id == id);

			if (customer.Count() == 0)
			{
				return HttpNotFound();
			}
			else
			{
				return View(customer.First());
			}
		}

		private List<Customer> GenerateCustomers()
		{
			var customers = new List<Customer>
			{
				new Customer { Id = 1, Name = "John Smith"},
				new Customer { Id = 2, Name = "Mary Williams"}
			};

			return customers;
		}
	}
}