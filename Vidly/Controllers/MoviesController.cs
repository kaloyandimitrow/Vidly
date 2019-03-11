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

		public ActionResult New()
		{
			var genres = _context.Genres.ToList();
			var vm = new MoviesFormViewModel
			{
				Genres = genres
			};
			return View("MovieForm", vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Movie movie)
		{
			if (movie.Id == 0)
			{
				movie.AddedDate = DateTime.Now;
				movie.Genre = _context.Genres.Single(g => g.Id == movie.GenreId);
				_context.Movies.Add(movie);
			}
			else
			{
				var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);

				movieInDB.Name = movie.Name;
				movieInDB.ReleaseDate = movie.ReleaseDate;
				movieInDB.NumberInStock = movie.NumberInStock;
				movieInDB.GenreId = movie.GenreId;
			}

			_context.SaveChanges();
			return RedirectToAction("Movies", "Movies");
		}

		//  GET: Movies/Details/{id}
		public ActionResult Details(int id)
		{
			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

			if (movie == null)
				return HttpNotFound();

			var vm = new MoviesFormViewModel
			{
				Movie = movie,
				Genres = _context.Genres.ToList()
			};

			return View("MovieForm",vm);
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