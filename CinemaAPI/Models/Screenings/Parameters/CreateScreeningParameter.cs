using CinemaApi.Models.Screenings.DTOs;

namespace CinemaApi.Models.Screenings.Parameters
{
	public class CreateScreeningParameter
	{
		public CreateScreeningParameter(CreateScreeningDto screening)
		{
			Screening = screening;
		}

		public CreateScreeningDto Screening { get; }
	}
}