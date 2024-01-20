using CinemaApi.Models;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Services.Cinemas.Converters;
using DataAccess;
using DataAccess.Entities;

namespace CinemaApi.Services.Cinemas.Services
{
	public class ListCinemaService : IService<ListCinemaParameter, List<CinemaDto>>
	{
		private readonly IRepository<Cinema> _cinemasRepository;
		private readonly CinemaDtoConverter _cinemaDtoConverter;

		public ListCinemaService(
			IRepository<Cinema> cinemasRepository, 
			CinemaDtoConverter cinemaDtoConverter)
		{
			_cinemasRepository = cinemasRepository;
			_cinemaDtoConverter = cinemaDtoConverter;
		}

		public async Task<Result<List<CinemaDto>>> CallAsync(ListCinemaParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var cinemas = await _cinemasRepository.ListAsync();

			if (parameter.Limit > 0)
			{
				cinemas = cinemas.Take(parameter.Limit).ToList();
			}

			var cinemaDtos = cinemas.Select(_cinemaDtoConverter.Convert).ToList();

			return new Result<List<CinemaDto>>(200, cinemaDtos);
		}
	}
}