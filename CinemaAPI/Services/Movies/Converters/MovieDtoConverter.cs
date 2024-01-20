using CinemaApi.Models.Movies.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Services.Movies.Converters
{
	public class MovieDtoConverter
	{
		public virtual MovieDto Convert(Movie movie)
		{
			return new MovieDto()
			{
				Id = movie.Id,
				TitleInLT = movie.TitleInLT,
				TitleInOriginalLanguage = movie.TitleInOriginalLanguage,
				Description = movie.Description,
				ReleaseYear = movie.ReleaseYear,
				Genre = movie.Genre,
				RatingLabelValue = Enum.GetName(typeof(RatingType), movie.RatingLabelValueId),
				Language = Enum.GetName(typeof(LanguageType), movie.LanguageId),
				SubtitleLanguage = Enum.GetName(typeof(LanguageType), movie.SubtitleLanguageId),
				RatingsInStars = movie.RatingsInStars
			};
		}
	}
}