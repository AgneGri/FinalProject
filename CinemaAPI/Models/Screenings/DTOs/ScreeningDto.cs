using CinemaApi.Models.Screenings.Converters;
using System.Text.Json.Serialization;

namespace CinemaApi.Models.Screenings.DTOs
{
	public class ScreeningDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Auditorium { get; set; } 
		public string TitleInLT { get; set; }
		public string TitleInOriginalLanguage { get; set; }
		public string Description { get; set; }
		public int ReleaseYear { get; set; }
		public string Genre { get; set; }
		public string RatingLabelValue { get; set; }
		public string Language { get; set; }
		public string SubtitleLanguage { get; set; }
		public decimal RatingsInStars { get; set; }

		[JsonConverter(typeof(DateConverter))]
		public DateTime ShowDate { get; set; }

		[JsonConverter(typeof(TimeConverter))]
		public DateTime ShowTime { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}