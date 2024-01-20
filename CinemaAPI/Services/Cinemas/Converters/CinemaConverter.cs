using CinemaApi.Models.Cinemas.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Services.Cinemas.Converters
{
	public class CinemaConverter
	{
		public Cinema Create(CreateCinemaDto cinemaDto)
		{
			return new Cinema
			{
				Name = cinemaDto.Name,
				City = cinemaDto.City,
				Auditorium = cinemaDto.Auditorium,
				CreatedAt = DateTime.UtcNow
			};
		}

		public Cinema Update(UpdateCinemaDto cinemaDto, Cinema cinemaEntity)
		{
			cinemaEntity.Name = cinemaDto.Name;
			cinemaEntity.City = cinemaDto.City;
			cinemaEntity.Auditorium = cinemaDto.Auditorium;
			
			return cinemaEntity;
		}
	}
}