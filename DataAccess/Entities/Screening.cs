using System.ComponentModel.DataAnnotations;
namespace DataAccess.Entities
{
	public class Screening : BaseEntity
	{
		[Required]
		public int CinemaId { get; set; }
		public Cinema Cinema { get; set; }

		[Required]
		public int MovieId { get; set; }
		public Movie Movie { get; set; }

		[Required]
		public DateTime ShowDate { get; set; }

		[Required] 
		public DateTime ShowTime { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}