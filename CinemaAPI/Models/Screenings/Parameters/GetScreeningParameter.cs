using CinemaApi.Exceptions;

namespace CinemaApi.Models.Screenings.Parameters
{
	public class GetScreeningParameter
	{
		public GetScreeningParameter(int id)
		{
			if (id < 1)
			{
				throw new DataValidationException("Invalid record Id.");
			}

			Id = id;
		}

		public int Id { get; }
	}
}