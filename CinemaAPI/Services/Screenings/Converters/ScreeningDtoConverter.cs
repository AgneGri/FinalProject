using CinemaApi.Models.Screenings.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Services.Screenings.Converters
{
	public class ScreeningDtoConverter
	{
		public ScreeningDto Convert(Screening screening)
		{
			var dto = new ScreeningDto
			{
				Id = screening.Id,
				ShowDate = screening.ShowDate,
				ShowTime = screening.ShowTime,
				CreatedAt = screening.CreatedAt
			};

			if (screening.Cinema != null)
			{
				dto.City = screening.Cinema.City;
				dto.Name = screening.Cinema.Name;
				dto.Auditorium = screening.Cinema.Auditorium;
			}

			if (screening.Movie != null)
			{
				dto.TitleInLT = screening.Movie.TitleInLT;
				dto.TitleInOriginalLanguage = screening.Movie.TitleInOriginalLanguage;

				if (screening.Movie.Description != null)
				{
					dto.Description = screening.Movie.Description.Replace("\n", " ");
				}
				dto.ReleaseYear = screening.Movie.ReleaseYear;
				dto.Genre = screening.Movie.Genre;
				dto.RatingLabelValue = Enum.GetName(typeof(RatingType), screening.Movie.RatingLabelValueId);
				dto.Language = Enum.GetName(typeof(LanguageType), screening.Movie.LanguageId);
				dto.SubtitleLanguage = Enum.GetName(typeof(LanguageType), screening.Movie.SubtitleLanguageId);
				dto.RatingsInStars = screening.Movie.RatingsInStars;
			}

			return dto;
		}
	}
}