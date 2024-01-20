using CinemaApi.Exceptions;
using CinemaApi.Models.Screenings.DTOs;

namespace CinemaApi.Models.Screenings.Validators
{
	public class UpdateScreeningParameterValidator
	{
		public void ValidateParameters(int id, UpdateScreeningDto screening)
		{
			if (id < 1)
			{
				throw new DataValidationException("Invalid screening record Id.");
			}

			if (screening == null)
			{
				throw new ArgumentNullException(nameof(screening));
			}

			if (screening.CinemaId <= 0)
			{
				throw new DataValidationException("The CinemaId is required.");
			}

			if (screening.MovieId <= 0)
			{
				throw new DataValidationException("The MovieId is required.");
			}

			if (screening.ShowDate.Date < DateTime.UtcNow.Date)
			{
				throw new DataValidationException("The ShowDate cannot be in the past.");
			}

			if (screening.ShowTime.Date != screening.ShowDate.Date)
			{
				throw new DataValidationException("The ShowTime and ShowDate must be the same.");
			}
		}
	}
}