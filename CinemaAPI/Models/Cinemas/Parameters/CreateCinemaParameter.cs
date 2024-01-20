using CinemaApi.Models.Cinemas.DTOs;

namespace CinemaApi.Models.Cinemas.Parameters
{
	public class CreateCinemaParameter
	{
		public CreateCinemaParameter(CreateCinemaDto cinema)
		{
			Cinema = cinema;
		}

		public CreateCinemaDto Cinema { get; }
	}
}