using CinemaApi.Models.Cinemas.DTOs;

namespace CinemaApi.Models.Cinemas.Parameters
{
	public class UpdateCinemaParameter
	{
		public UpdateCinemaParameter(int id, UpdateCinemaDto cinema)
		{
			Id = id;
			Cinema = cinema;
		}

		public int Id { get; }
		public UpdateCinemaDto Cinema { get; }
	}
}