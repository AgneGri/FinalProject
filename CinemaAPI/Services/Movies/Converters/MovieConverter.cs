using CinemaApi.Models.Movies.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Services.Movies.Converters
{
	public class MovieConverter
	{
		public Movie Create(CreateMovieDto movieDto)
		{
			return new Movie
			{
				TitleInLT = movieDto.TitleInLT,
				TitleInOriginalLanguage = movieDto.TitleInOriginalLanguage,
				Description = movieDto.Description,
				ReleaseYear = movieDto.ReleaseYear,
				Genre = movieDto.Genre,
				RatingLabelValueId = (int)Enum.Parse(typeof(RatingType), movieDto.RatingLabelValue),
				LanguageId = (int)Enum.Parse(typeof(LanguageType), movieDto.Language),
				SubtitleLanguageId = (int)Enum.Parse(typeof(LanguageType), movieDto.SubtitleLanguage),
				RatingsInStars = movieDto.RatingsInStars,
				CreatedAt = DateTime.Now
			};
		}

		public Movie Update(UpdateMovieDto movieDto, Movie movieEntity)
		{
			movieEntity.TitleInLT = movieDto.TitleInLT;
			movieEntity.TitleInOriginalLanguage = movieDto.TitleInOriginalLanguage;
			movieEntity.Description = movieDto.Description;
			movieEntity.ReleaseYear = movieDto.ReleaseYear;
			movieEntity.Genre = movieDto.Genre;
			movieEntity.RatingLabelValueId = (int)Enum.Parse(typeof(RatingType), movieDto.RatingLabelValue);
			movieEntity.LanguageId = (int)Enum.Parse(typeof(LanguageType), movieDto.Language);
			movieEntity.SubtitleLanguageId = (int)Enum.Parse(typeof(LanguageType), movieDto.SubtitleLanguage);
			movieEntity.RatingsInStars = movieDto.RatingsInStars;

			return movieEntity;
		}
	}
}