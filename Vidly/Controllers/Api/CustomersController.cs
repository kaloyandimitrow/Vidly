using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
		private ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();					
		}

		//GET api/customers
		public IEnumerable<CustomerDTO> GetCustomers()
		{
			return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDTO>);
		}

		//GET api/customers/{id}
		public IHttpActionResult GetCustomer(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return NotFound();

			return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
		}

		//POST api/customers
		[HttpPost]
		public IHttpActionResult CreateCustomer(CustomerDTO customer)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var domainModelCustomer = Mapper.Map<CustomerDTO, Customer>(customer);
			_context.Customers.Add(domainModelCustomer);
			_context.SaveChanges();

			customer.Id = domainModelCustomer.Id;

			return Created(new Uri(Request.RequestUri + "\\" + customer.Id), customer);
		}

		//PUT api/customers/{id}
		[HttpPut]
		public void UpdateCustomer(int id, CustomerDTO customerDto)
		{
			if (!ModelState.IsValid)
				throw new HttpResponseException(HttpStatusCode.BadRequest);

			var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);

			if (customerInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			Mapper.Map(customerDto, customerInDB);

			_context.SaveChanges();
		}

		//DELETE api/customer/{id}
		[HttpDelete]
		public void DeleteCustomer(int id)
		{
			var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);

			if (customerInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			_context.Customers.Remove(customerInDB);
			_context.SaveChanges();
		}
	}
}
