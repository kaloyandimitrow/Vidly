using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class MoviesController : Controller
	{
		// GET: Movies/Random
		public ActionResult Random()
		{
			var movie = new Movie { Name = "Shrek" };

			var customers = new List<Customer>
			{
				new Customer { Name = "Customer 1"},
				new Customer { Name = "Customer 2"}
			};

			var vm = new RandomMovieViewModel
			{
				Movie = movie,
				Customers = customers
			};

			return View(vm);
		}

		//  GET: Movies/Edit/{id}
		public ActionResult Edit(int id)
		{
			return Content("Edited");
		}

		//GET: Movies
		[Route("movies")]
		public ActionResult Movies()
		{
			var movies = new List<Movie>
			{
				new Movie {Name = "Shrek" },
				new Movie {Name = "Wall-e" }
			};

			var vm = new MoviesViewModel
			{
				Movies = movies
			};

			return View(vm);
		}

		//GET: Movies/ByReleaseDate/{year}/{month}
		[Route("movies/released/{year:regex(\\d{4}):range(1900,2019)}/{month:regex(\\d{2}):range(1,12)}")]
		public ActionResult ByReleaseYear(int year, int month)
		{
			return Content(year + " | " + month);
		}
    }
}