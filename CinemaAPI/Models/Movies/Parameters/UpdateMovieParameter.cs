using CinemaApi.Models.Movies.DTOs;

namespace CinemaApi.Models.Movies.Parameters
{
	public class UpdateMovieParameter
	{
		public UpdateMovieParameter(int id, UpdateMovieDto movie)
		{
			Id = id;
			Movie = movie;
		}

		public int Id { get; }
		public UpdateMovieDto Movie { get; }
	}
}