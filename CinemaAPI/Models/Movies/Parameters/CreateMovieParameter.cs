using CinemaApi.Models.Movies.DTOs;

namespace CinemaApi.Models.Movies.Parameters
{
	public class CreateMovieParameter
	{
		public CreateMovieParameter(CreateMovieDto movie)
		{
			Movie = movie;
		}

		public CreateMovieDto Movie { get; }
	}
}