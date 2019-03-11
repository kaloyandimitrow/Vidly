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
    public class MoviesController : ApiController
    {
		private ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		//GET api/customers
		public IEnumerable<MovieDTO> GetMovies()
		{
			return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDTO>);
		}

		//GET api/customers/{id}
		public IHttpActionResult GetMovie(int id)
		{
			var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

			if (movie == null)
				return NotFound();

			return Ok(Mapper.Map<Movie, MovieDTO>(movie));
		}

		//POST api/customers
		[HttpPost]
		public IHttpActionResult CreateMovie(MovieDTO movie)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var domainModelmovie = Mapper.Map<MovieDTO, Movie>(movie);
			_context.Movies.Add(domainModelmovie);
			_context.SaveChanges();

			movie.Id = domainModelmovie.Id;

			return Created(new Uri(Request.RequestUri + "\\" + movie.Id), movie);
		}

		//PUT api/customers/{id}
		[HttpPut]
		public void UpdateMovie(int id, MovieDTO movieDTO)
		{
			if (!ModelState.IsValid)
				throw new HttpResponseException(HttpStatusCode.BadRequest);

			var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);

			if (movieInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			Mapper.Map(movieDTO, movieInDB);

			_context.SaveChanges();
		}

		//DELETE api/customer/{id}
		[HttpDelete]
		public void DeleteMovie(int id)
		{
			var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);

			if (movieInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			_context.Movies.Remove(movieInDB);
			_context.SaveChanges();
		}
	}
}
