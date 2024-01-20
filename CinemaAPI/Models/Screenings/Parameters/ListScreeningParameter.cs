using CinemaApi.Exceptions;

namespace CinemaApi.Models.Screenings.Parameters
{
	public class ListScreeningParameter
	{
		public ListScreeningParameter(int limit)
		{
			if (limit <= 0)
			{
				throw new DataValidationException("Invalid limit value. It must be greater than 0.");
			}

			Limit = limit;
		}

		public ListScreeningParameter(
			string? city = null,
			DateTime? showDate = null,
			string? auditorium = null,
			string? genre = null)
		{
			City = city;
			ShowDate = showDate;
			Auditorium = auditorium;
			Genre = genre;
		}

		public string? City { get; }
		public DateTime? ShowDate { get; }
		public string? Auditorium { get; }
		public string? Genre { get; set; }
		public int? Limit { get; }
	}
}