using CinemaApi.Models.Screenings.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Services.Screenings.Converters
{
	public class ScreeningShowingsDtoConverter
	{
		public ScreeningShowingsDto Convert(Screening screening)
		{
			var dto = new ScreeningShowingsDto
			{
				Id = screening.Id,
				ShowDate = screening.ShowDate,
				ShowTime = screening.ShowTime,
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
			}

			return dto;
		}
	}
}