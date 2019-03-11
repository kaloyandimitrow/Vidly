using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public Genre Genre { get; set; }
		public byte GenreId { get; set; }
		public DateTime ReleaseDate { get; set; }
		public DateTime AddedDate { get; set; }

		[Range(1,20)]
		public int NumberInStock { get; set; }
	}
}