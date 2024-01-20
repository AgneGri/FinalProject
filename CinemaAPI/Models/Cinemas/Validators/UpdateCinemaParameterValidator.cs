using CinemaApi.Exceptions;
using CinemaApi.Models.Cinemas.DTOs;

namespace CinemaApi.Models.Cinemas.Validators
{
	public class UpdateCinemaParameterValidator
	{
		public void ValidateParameters(int id, UpdateCinemaDto cinema)
		{
			if (id < 1)
			{
				throw new DataValidationException("Invalid cinema Id.");
			}

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