using CinemaApi.Exceptions;
using CinemaApi.Models.Movies.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Models.Movies.Validators
{
	public class CreateMovieParameterValidator
	{
		public void ValidateParameters(CreateMovieDto movie)
		{
			if (movie == null)
			{
				throw new ArgumentNullException(nameof(movie));
			}

			if (string.IsNullOrWhiteSpace(movie.TitleInLT))
			{
				throw new DataValidationException("The movie title in Lithuanian is required.");
			}

			if (string.IsNullOrWhiteSpace(movie.TitleInOriginalLanguage))
			{
				throw new DataValidationException("The movie title in original language is required.");
			}

			if (string.IsNullOrWhiteSpace(movie.Description))
			{
				throw new DataValidationException("The movie description is required.");
			}

			if (string.IsNullOrWhiteSpace(movie.Genre))
			{
				throw new DataValidationException("The movie genre is required.");
			}

			if (movie.ReleaseYear < 0)
			{
				throw new DataValidationException("The entered release year is invalid.");
			}

			ValidateEnum<LanguageType>(movie.Language, nameof(movie.Language));
			ValidateEnum<LanguageType>(movie.SubtitleLanguage, nameof(movie.SubtitleLanguage));
			ValidateEnum<RatingType>(movie.RatingLabelValue, nameof(movie.RatingLabelValue));
		}

		private void ValidateEnum<TEnum>(string enumValue, string propertyName) where TEnum : struct
		{
			if (string.IsNullOrWhiteSpace(enumValue) || !Enum.IsDefined(typeof(TEnum), enumValue))
			{
				throw new DataValidationException($"Incorrect input format or missing value for {propertyName}.");
			}
		}
	}
}