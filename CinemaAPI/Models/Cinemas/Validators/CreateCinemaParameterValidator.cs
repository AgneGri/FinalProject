using CinemaApi.Exceptions;
using CinemaApi.Models.Cinemas.DTOs;

namespace CinemaApi.Models.Cinemas.Validators
{
	public class CreateCinemaParameterValidator
	{
		public void ValidateParameters(CreateCinemaDto cinema)
		{
			if (cinema == null)
			{
				throw new ArgumentNullException(nameof(cinema));
			}

			if (string.IsNullOrWhiteSpace(cinema.Name))
			{
				throw new DataValidationException("The cinema name is required.");
			}

			if (string.IsNullOrWhiteSpace(cinema.City))
			{
				throw new DataValidationException("The cinema city is required.");
			}

			if (string.IsNullOrWhiteSpace(cinema.Auditorium))
			{
				throw new DataValidationException("The cinema auditorium is required.");
			}
		}
	}
}