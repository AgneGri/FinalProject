using CinemaApi.Exceptions;

namespace CinemaApi.Models.Movies.Parameters
{
	public class ListMovieParameter
	{
		public ListMovieParameter(int limit)
		{
			if (limit <= 0)
			{
				throw new DataValidationException("Invalid limit value. It must be greater than 0.");
			}

			Limit = limit;
		}

		public ListMovieParameter(
			string? titleInLt = null,
			string? titleInOriginalLanguage = null,
			int? releaseYear = null,
			string? genre = null)
		{
			TitleInLt = titleInLt;
			TitleInOriginalLanguage = titleInOriginalLanguage;
			ReleaseYear = releaseYear;
			Genre = genre;
		}

		public string? TitleInLt { get; }
		public string? TitleInOriginalLanguage { get; }
		public int? ReleaseYear { get; }
		public string? Genre { get; }
		public int? Limit { get; }
	}
}