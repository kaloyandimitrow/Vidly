﻿using System;
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
			var vm = new NewCustomerViewModel
			{
				MembershipTypes = membershipTypes
			};
			return View(vm);
		}

		[HttpPost]
		public ActionResult Create(Customer customer)
		{
			_context.Customers.Add(customer);
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
	}
}