using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class RatingLabelValue : BaseEntity
	{
		[Required]
		[MaxLength(5)]
		public RatingType RatingType { get; set; }
	}
}