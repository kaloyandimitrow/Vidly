using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class MoviesController : Controller
	{
		private ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		//  GET: Movies/Details/{id}
		public ActionResult Details(int id)
		{
			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

			if (movie == null)
				return HttpNotFound();

			return View(movie);
		}

		//GET: Movies
		[Route("movies")]
		public ActionResult Movies()
		{
			var vm = new MoviesViewModel
			{
				Movies = _context.Movies.Include(m => m.Genre).ToList()
			};

			return View(vm);
		}
    }
}