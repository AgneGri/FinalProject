using CinemaApi.Models.Screenings.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Services.Screenings.Converters
{
	public class ScreeningConverter
	{
		public Screening Create(CreateScreeningDto screeningDto)
		{
			return new Screening
			{
				CinemaId = screeningDto.CinemaId,
				MovieId = screeningDto.MovieId,
				ShowDate = screeningDto.ShowDate,
				ShowTime = screeningDto.ShowTime,
				CreatedAt = DateTime.UtcNow
			};
		}

		public Screening Update(UpdateScreeningDto screeningDto, Screening screeningEntity)
		{
			screeningEntity.CinemaId = screeningDto.CinemaId;
			screeningEntity.MovieId = screeningDto.MovieId;
			screeningEntity.ShowDate = screeningDto.ShowDate;
			screeningEntity.ShowTime = screeningDto.ShowTime;

			return screeningEntity;
		}
	}
}