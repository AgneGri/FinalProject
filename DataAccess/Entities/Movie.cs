using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Movie : BaseEntity
	{
		[Required]
		[MaxLength(70)]
		public string TitleInLT { get; set; } = null!;

		[Required]
		[MaxLength(70)]
		public string TitleInOriginalLanguage { get; set; } = null!;

		[Required]
		[MaxLength(1500)]
		public string Description { get; set; } = null!;

		[Required]
		public int ReleaseYear { get; set; }

		[Required]
		[MaxLength(70)]
		public string Genre { get; set; }

		[Required]
		public int RatingLabelValueId { get; set; }

		[Required]
		public int LanguageId { get; set; }

		[Required]
		public decimal RatingsInStars { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[Required]
		public int SubtitleLanguageId { get; set; }
	}
}