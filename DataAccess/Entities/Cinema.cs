using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Cinema : BaseEntity
	{
		[Required]
		[MaxLength(50)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(50)]
		public string City { get; set; } = null!;

		[Required]
		[MaxLength(50)]
		public string Auditorium { get; set; } = null!;

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}