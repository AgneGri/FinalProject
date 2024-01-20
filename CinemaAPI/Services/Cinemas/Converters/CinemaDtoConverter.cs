using CinemaApi.Models.Cinemas.DTOs;
using DataAccess.Entities;

namespace CinemaApi.Services.Cinemas.Converters
{
	public class CinemaDtoConverter
	{
		public CinemaDto Convert(Cinema cinema)
		{
			return new CinemaDto()
			{
				Id = cinema.Id,
				Name = cinema.Name,
				City = cinema.City,
				Auditorium = cinema.Auditorium
			};
		}
	}
}