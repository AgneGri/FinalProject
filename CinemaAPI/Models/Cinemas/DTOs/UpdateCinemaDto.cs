namespace CinemaApi.Models.Cinemas.DTOs
{
	public class UpdateCinemaDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Auditorium { get; set; }
	}
}