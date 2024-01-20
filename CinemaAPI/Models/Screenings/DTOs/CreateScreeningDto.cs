namespace CinemaApi.Models.Screenings.DTOs
{
	public class CreateScreeningDto
	{
		public int CinemaId { get; set; }
		public int MovieId { get; set; }
		public DateTime ShowDate { get; set; }
		public DateTime ShowTime { get; set; }
	}
}