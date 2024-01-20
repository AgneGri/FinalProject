namespace CinemaApi.Models.Movies.DTOs
{
	public class UpdateMovieDto
	{
		public int Id { get; set; }
		public string TitleInLT { get; set; }
		public string TitleInOriginalLanguage { get; set; }
		public string Description { get; set; }
		public int ReleaseYear { get; set; }
		public string Genre { get; set; }
		public string RatingLabelValue { get; set; }
		public string Language { get; set; }
		public string SubtitleLanguage { get; set; }
		public decimal RatingsInStars { get; set; }
	}
}