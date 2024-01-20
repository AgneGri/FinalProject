using CinemaApi.Exceptions;

namespace CinemaApi.Models.Cinemas.Parameters
{
	public class DeleteCinemaParameter
	{
		public DeleteCinemaParameter(int id)
		{
			if (id < 1)
			{
				throw new DataValidationException("Invalid cinema Id.");
			}

			Id = id;
		}

		public int Id { get; }
	}
}