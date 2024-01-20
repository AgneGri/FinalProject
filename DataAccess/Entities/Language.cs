using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	
	public class Language : BaseEntity
	{
		[Required]
		[MaxLength(35)]
		public LanguageType LanguageType { get; set; }
	}
}