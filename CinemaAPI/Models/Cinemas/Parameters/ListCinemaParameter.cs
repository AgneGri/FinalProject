using CinemaApi.Exceptions;

namespace CinemaApi.Models.Cinemas.Parameters
{
	public class ListCinemaParameter
	{
		public ListCinemaParameter(int limit)
		{
			if (limit <= 0)
			{
				throw new DataValidationException("Invalid limit value. It must be greater than 0.");
			}

			Limit = limit;
		}

		public int Limit { get; }
	}
}