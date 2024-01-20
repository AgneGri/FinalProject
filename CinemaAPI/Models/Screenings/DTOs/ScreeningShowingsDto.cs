using CinemaApi.Models.Screenings.Converters;
using System.Text.Json.Serialization;

namespace CinemaApi.Models.Screenings.DTOs
{
	public class ScreeningShowingsDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Auditorium { get; set; }
		public string TitleInLT { get; set; }
		public string TitleInOriginalLanguage { get; set; }

		[JsonConverter(typeof(DateConverter))]
		public DateTime ShowDate { get; set; }

		[JsonConverter(typeof(TimeConverter))]
		public DateTime ShowTime { get; set; }
	}
}