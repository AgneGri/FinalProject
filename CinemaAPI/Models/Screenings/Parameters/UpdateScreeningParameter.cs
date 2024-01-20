using CinemaApi.Models.Screenings.DTOs;

namespace CinemaApi.Models.Screenings.Parameters
{
	public class UpdateScreeningParameter
	{
		public UpdateScreeningParameter(int id, UpdateScreeningDto screening)
		{
			Id = id;
			Screening = screening;
		}

		public int Id { get; }
		public UpdateScreeningDto Screening { get; }
	}
}